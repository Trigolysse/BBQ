using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    
    #region Private Fields


    [Tooltip("UI Text to display Player's Name")]
    [SerializeField]
    private Text playerNameText;

    [Tooltip("UI Text to display the number of Alive players")]
    [SerializeField]
    private Text aliveCounter;


    [Tooltip("UI Slider to display Player's Ammunition")]
    [SerializeField]
    private Text weaponAmmunitionCounter;

    [Tooltip("UI Slider to display Player's Magazine")]
    [SerializeField]
    private Text weaponMagazineCounter;

    [Tooltip("UI Slider to display Player's Health")]
    [SerializeField]
    private HealthBar playerHealthBar;

    [Tooltip("The TeamatePrefab")]
    [SerializeField]
    public GameObject TeamatePrefab;

    public Player target;

    public WeaponManager weaponManager;
    int i = 0;
    #endregion


    #region MonoBehaviour Callbacks

    void Awake()
    {
        this.transform.SetParent(GameObject.Find("Canvas").GetComponent<Transform>(), false);
    }

    private void Start()
    {
        if (!target.photonView.IsMine) return;
        // Reflect the Team
        if (TeamatePrefab != null)
        {
            GameObject[] allPlayers = GameObject.FindGameObjectsWithTag("Player");

            foreach (GameObject player in allPlayers)
            {
                i++;
                GameObject _uiGo = Instantiate(TeamatePrefab, this.transform);
                _uiGo.SendMessage("SetTarget", new TeamateObject(player.GetComponent<Player>(), i), SendMessageOptions.RequireReceiver);
            }
        }
    }



    // Update is called once per frame
    void Update()
    {
      

        // Destroy itself if the target(Player) is null, It's a fail safe when Photon is destroying Instances of a Player over the network
        if (target == null)
        {
            Destroy(this.gameObject);
            return;
        }

        // Reflect the Player Health
        if (playerHealthBar != null)
        {
            playerHealthBar.value = target.Health;
        }

        // Reflect the Player's Weapon Ammunition
        if (weaponAmmunitionCounter != null)
        {
            int Ammunution = target.weaponManager.GetCurrentSelectedWeapon().Ammunition;
            weaponAmmunitionCounter.text = Ammunution.ToString();
            if (Ammunution < 5)
            {
                weaponAmmunitionCounter.color = Color.red;
            }
            else
                weaponAmmunitionCounter.color = Color.white;
        }
       
        // Reflect the Player Magazine
        if (weaponMagazineCounter != null)
        {
            int Magazine = target.weaponManager.GetCurrentSelectedWeapon().Magazine;
            weaponMagazineCounter.text = Magazine.ToString();
        }

        // Display number of alive players
        if (aliveCounter != null)
        {
            aliveCounter.text = PhotonNetwork.PlayerList.Length.ToString();
        }
    }

    #endregion


    #region Public Methods

    public void SetTarget(Player _target)
    {
        if (_target == null)
        {
            Debug.LogError("<color=red><a>Missing</a></Color> PlayMakerManager target for PlayerUI.SetTarget.", this);
            return;
        }
        // Cache references for efficiency
        target = _target;
    }

    #endregion

}
