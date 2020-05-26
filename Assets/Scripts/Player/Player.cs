using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviourPunCallbacks, IPunObservable
{
    public GameObject respawnGobj;
    public Text respawntext;
    public float respawntime;
    public bool teleport;
    private int timeblood = 0;
    private bool blood = false;
    private int frames;
    private int pasthealth;
    private RaycastHit hit;
    private Animator anim;
    public GameObject Ernesto;

    public Canvas DeadCanvas;
    public GameObject BloodSight;
    public GameObject healthBar;
    private bool gooddead = false;

    public GameObject talk;

    private GameObject redbbq;

    private GameObject bluebbq;

    private GameObject engin;
    //public Canvas playerUI;

    /** DO NOT TOUCH IF YOU ARE NOT QUALIFIED 
    Spoiler Alert: You are not qualified */

    #region Attributs

    protected static string dependencyInjector = "9ecd8da311d71beb026e20851965f502";

    public Teams team;

    [Tooltip("The Player's UI GameObject Prefab")]
    public int Health;

    [Tooltip("The Player's UI GameObject Prefab")]
    [SerializeField]
    public GameObject PlayerUiPrefab;

    public WeaponManager weaponManager;

    [SerializeField]
    private PlayerMovement playerMovement;

    #endregion

    public bool isDead = false;
    public bool isOutOfFocus = false;

    private void Awake()
    {
        Health = 100;
        pasthealth = Health; /* ???? nice coding u nerd */
        frames = 0;
        weaponManager = GetComponent<WeaponManager>();
        bluebbq = GameObject.FindWithTag("BLUEBBQ");
        redbbq = GameObject.FindWithTag("REDBBQ");
        engin = GameObject.Find("CoreEngine");
    }

    void Start()
    {
        if (!photonView.IsMine)
            return;
        teleport = false;

        // Inject Player reference
        if (PlayerUiPrefab != null)
        {
            GameObject _uiGo = Instantiate(PlayerUiPrefab);
            _uiGo.SendMessage("SetTarget", this, SendMessageOptions.RequireReceiver);
        }
        else
        {
            Debug.LogWarning("<Color=Red><a>Missing</a></Color> PlayerUiPrefab reference on player Prefab. " + dependencyInjector, this);
        }

        /* */
        healthBar.GetComponent<MonsterHEALTHBAR>().SetHealth(Health);
        DeadCanvas.enabled = false;
        anim = Ernesto.GetComponent<Animator>();
        respawntime = 5;
    }
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(team);
        }
        else
        {
            this.team = (Teams)stream.ReceiveNext();
        }
    }

    void Update()
    {
        if (!photonView.IsMine)
            return;
        if (team==Teams.RED)
        {
            if (Vector3.Distance(bluebbq.transform.position,transform.position)<2)
            {
                engin.GetComponent<CoreEngine>().redscore += Time.deltaTime;
            }
        }
        else
        {
            if (Vector3.Distance(redbbq.transform.position,transform.position)<2)
            {
                engin.GetComponent<CoreEngine>().bluescore += Time.deltaTime;
            }
        }
        if (teleport)
        {
            teleport = false;
            gameObject.GetComponent<CharacterController>().enabled = false;
            if (team==Teams.RED)
            {
                Debug.Log("red");
                transform.position = new Vector3(-191.096f, 20, -68.05299f);
                gameObject.GetComponent<CharacterController>().enabled = true;
            }
            else
            {
                Debug.Log("blue");
                transform.position = new Vector3(406.4642f, 6, 536.1269f);
                gameObject.GetComponent<CharacterController>().enabled = true;
            }
        }
        if (isDead)
        {
            respawntext.text = ((int) (respawntime / 1)).ToString();
            if (!gooddead)
            {
                //anim.SetBool("Dead",true);
                gooddead = true;
                Teleport();
            }
            else
            {
                respawntime -= Time.deltaTime;
                if (respawntime<0)
                {
                    respawntime = 5;
                    Respawn();
                }
            }
        }
       
        if (blood)
        {
            timeblood++;
        }

        if (timeblood==32)
        {
            blood = false;
            timeblood = 0;
            BloodSight.SetActive(false);
        }
        frames++;
        if (frames%4==0)
        {
            if (pasthealth>Health)
            {
                pasthealth = Health;
                BloodSight.SetActive(true);
                blood = true;
                timeblood = 0;

            }
            else
            {
                pasthealth = Health;
            }
        }
        
    }

    [PunRPC]
    public void ApplyDamage(string _sourceName, int damage, WeaponName weaponName)
    {
        if (isDead)
        {
            GetComponent<AudioSource>().Play();
            return;
        }
        

        Debug.Log("ApplyDamage");

        Health -= damage;
        if (Health<0)
        {
            Health = 0;
        }
        healthBar.GetComponent<MonsterHEALTHBAR>().SetHealth(Health);
        if (Health <= 0)
        {
            Die(_sourceName, weaponName);
        }
    }

    private void Respawn()
    {
        if (!photonView.IsMine)
            return;
        Health = 100;
        isDead = false;
        teleport = true;
        respawnGobj.SetActive(false);
    }

   
    private void Die(string _sourceName, WeaponName weaponName)
    {
        respawnGobj.SetActive(true);

        isDead = true;
        GameManager.Instance.onPlayerKilledCallback.Invoke(_sourceName, photonView.Owner.NickName, weaponName);

        if (photonView.IsMine)
        {
            respawntime = 5;
        }
   
        //Destroy(gameObject);
    }

    public void OnTalk()
    {
        talk.SetActive(true);
    }
    public void NoTalk()
    {
        talk.SetActive(false);
    }

    public void Teleport()
    {
       
            Debug.Log("red");
            gameObject.GetComponent<CharacterController>().enabled = false;
            transform.position = new Vector3(-191.096f, -2, -68.05299f);
            gameObject.GetComponent<CharacterController>().enabled = true;
       
    }


}

public enum Teams
{
    RED, BLUE
}
