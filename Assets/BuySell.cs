using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class BuySell : MonoBehaviourPunCallbacks
{
    
    public GameObject BuySelll;
    private bool droit;
    public GameObject canvassell;
    private Player player;
    private Tir tir;
    private PlayerMovement playermovement;
    private PlayerSetup playersetup;
    public GameObject Lookroot;
    public Button[] Sell;
    public Button[] Buy;
    public Image[] Ui;
    public bool[] canbuy;
    // Start is called before the first frame update
    void Start()
    {
        player=gameObject.GetComponent<Player>();
        tir = gameObject.GetComponent<Tir>();
        playermovement = gameObject.GetComponent<PlayerMovement>();
        playersetup = gameObject.GetComponent<PlayerSetup>();
        BuySelll.SetActive(false);
        droit = true;
        canvassell.SetActive(false);
        for(int i = 0; i < 6;i++)
        {
            int a = i;
            Sell[a].onClick.AddListener(() => GetComponent<SimpleInv>().Sell((Loot)a));
        }
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag=="PNJ")
        {
            
            if (photonView.IsMine)
            {
                BuySelll.SetActive(true);
            }
            
        }
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.tag=="PNJ")
        {
            if (droit && Input.GetKeyUp(KeyCode.F))
            {
                BuySelll.SetActive(false);
                droit = false;

            }
            if (droit && Input.GetKeyUp(KeyCode.O))
            {
                BuySelll.SetActive(false);
                canvassell.SetActive(true);
                player.isOutOfFocus = true;

                Cursor.lockState = CursorLockMode.None;

                Cursor.visible = true;
            
            }

        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag=="PNJ")
        {
            
            BuySelll.SetActive(false);
            canvassell.SetActive(false);
        }

        droit = true;
    }
    private void SellShit(int i)
    {
        GetComponent<SimpleInv>().Sell((Loot)i);
    }
}
