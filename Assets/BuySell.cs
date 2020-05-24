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
    // Start is called before the first frame update
    void Start()
    {
        player=gameObject.GetComponent<Player>();
        BuySelll.SetActive(false);
        droit = true;
        canvassell.SetActive(false);
        
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
        if (droit && Input.GetKeyUp(KeyCode.F))
        {
            BuySelll.SetActive(false);
            droit = false;
        }
        if (droit && Input.GetKeyUp(KeyCode.O))
        {
            BuySelll.SetActive(false);
            canvassell.SetActive(true);
            
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
}
