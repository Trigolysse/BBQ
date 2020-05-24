using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviourPunCallbacks
{
    private int timeblood = 0;
    private bool blood = false;
    private int frames;
    private int pasthealth;
    private RaycastHit hit;
    private Animator anim;
    public GameObject Ernesto;
    public int Health;
    public Canvas DeadCanvas;

    public GameObject BloodSight;
    public GameObject healthBar;
    //public Canvas playerUI;

    [Tooltip("The Player's UI GameObject Prefab")]
    [SerializeField]
    public GameObject PlayerUiPrefab;

    [SerializeField]
    private PlayerMovement playerMovement;

 
    public bool isDead = false;
    public bool isOutOfFocus = false;

    private void Awake()
    {
        Health = 100;
        pasthealth = Health;
        frames = 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        healthBar.GetComponent<MonsterHEALTHBAR>().SetMaxHealth(Health);
        DeadCanvas.enabled = false;
        anim = Ernesto.GetComponent<Animator>();
  
        if (!photonView.IsMine)
            return;

        //GameObject.Find("Death Canvas").GetComponent<Canvas>().enabled = false;

        if (PlayerUiPrefab != null)
        {
            Debug.Log("PlayerUiPrefab");
            GameObject _uiGo = Instantiate(PlayerUiPrefab);
            _uiGo.SendMessage("SetTarget", this, SendMessageOptions.RequireReceiver);
        }
        else
        {
            Debug.LogWarning("<Color=Red><a>Missing</a></Color> PlayerUiPrefab reference on player Prefab.", this);
        }

        
        if (!photonView.IsMine)
        {
            //playerUI.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!photonView.IsMine)
            return;
        if (isDead)
        {
            anim.SetBool("Dead",true);
            DeadCanvas.enabled = true;
            this.isOutOfFocus = true;
        }
        else
        {
            DeadCanvas.enabled = false;
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
        isDead = false;
        GameObject.Find("Death Canvas").GetComponent<Canvas>().enabled = false;
    }

   
    private void Die(string _sourceName, WeaponName weaponName)
    {

        isDead = true;
        GameManager.Instance.onPlayerKilledCallback.Invoke(_sourceName, photonView.Owner.NickName, weaponName);

        if (photonView.IsMine)
        {
            GameObject.Find("Death Canvas").GetComponent<Canvas>().enabled = true;
        }
   
        //Destroy(gameObject);
    }

}
