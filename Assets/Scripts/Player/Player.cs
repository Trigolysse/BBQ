using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviourPunCallbacks
{
    private int frames;
    private int pasthealth;
    private RaycastHit hit;
    public int Health;

    public GameObject BloodSight;
    //public Canvas playerUI;

    [Tooltip("The Player's UI GameObject Prefab")]
    [SerializeField]
    public GameObject PlayerUiPrefab;

    [SerializeField]
    private PlayerMovement playerMovement;

 
    public bool isDead = false;

    private void Awake()
    {
        Health = 100;
        pasthealth = Health;
        frames = 0;
    }

    // Start is called before the first frame update
    void Start()
    {
  
        if (!photonView.IsMine)
            return;

        GameObject.Find("Death Canvas").GetComponent<Canvas>().enabled = false;

        if (PlayerUiPrefab != null)
        {
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
        frames++;
        if (frames%4==0)
        {
            if (pasthealth>Health)
            {
                pasthealth = Health;
            }
            else
            {
                pasthealth = Health;
            }
        }
        if (!photonView.IsMine)
            return;
    }

    [PunRPC]
    public void ApplyDamage(string _sourceName, int damage, WeaponName weaponName)
    {
        if (isDead)
            return;

        Debug.Log("ApplyDamage");

        Health -= damage;
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
