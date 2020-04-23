using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviourPunCallbacks
{
    private RaycastHit hit;
    public int Health;
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
        if (!photonView.IsMine)
            return;
    }

    [PunRPC]
    public void ApplyDamage(string _sourceName, int damage)
    {
        if (isDead)
            return;

        Debug.Log("ApplyDamage");

        Health -= damage;
        if (Health <= 0)
        {
            Die(_sourceName);
        }
    }

    private void Respawn()
    {
        if (!photonView.IsMine)
            return;
        isDead = false;
        GameObject.Find("Death Canvas").GetComponent<Canvas>().enabled = false;
    }

   
    private void Die(string _sourceName)
    {

        isDead = true;
        GameManager.Instance.onPlayerKilledCallback.Invoke(_sourceName, photonView.Owner.NickName);

        if (photonView.IsMine)
        {
            GameObject.Find("Death Canvas").GetComponent<Canvas>().enabled = true;
        }
   
        //Destroy(gameObject);
    }

}
