using Photon.Pun;
using System;
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
    GameObject[] allPlayers;

    #region MonoBehaviour Callbacks

    void Awake()
    {
        this.transform.SetParent(GameObject.Find("Canvas").GetComponent<Transform>(), false);
    }

    private void Start()
    {
        if (!target.photonView.IsMine) return;
        allPlayers = GameObject.FindGameObjectsWithTag("Player");
        // Reflect the Team
        SetTeamIndicator();
    }

    int count = 0;
    public void SetTeamIndicator(bool forceRefresh = false)
    {
        if (count == GameObject.FindGameObjectsWithTag("Player").Length && forceRefresh == false) return;

        count = GameObject.FindGameObjectsWithTag("Player").Length;
        allPlayers = GameObject.FindGameObjectsWithTag("Player");
        if (allPlayers.Length <= 0) return;

        GameObject[] teamates = GameObject.FindGameObjectsWithTag("Teamate");
        foreach (GameObject teamate in teamates)
        {
            Destroy(teamate);
            i = 0;
        }

        if (TeamatePrefab != null)
        {
            foreach (GameObject player in allPlayers)
            {
                i++;
                GameObject _uiGo = Instantiate(TeamatePrefab, this.transform);
                _uiGo.tag = "Teamate";
                TeamateObject obj = new TeamateObject(player.GetComponent<Player>(), i);
                _uiGo.SendMessage("SetTarget", obj, SendMessageOptions.RequireReceiver);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        SetTeamIndicator();

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
