using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public enum Loot
{
    Purple, Yellow, Orange, Wood, Rocks, Metal
}

public class SimpleInv : MonoBehaviourPunCallbacks
{
    private bool ouvert;
    public GameObject inventaire;
   
    public Text[] lamoula;
    public GameObject AK;
    public GameObject Sword;
    public Image SwordImage;
    public Image AKImage;
    public GameObject BuySell;
    private bool droit;
    public GameObject canvassell;
    private Player player;

    public int[] count = new int[6];
    // Start is called before the first frame update
    void Start()
    {
        
        {
            
        }
        player=gameObject.GetComponent<Player>();
        
        inventaire.SetActive(false);
        BuySell.SetActive(false);
        for(int i = 0;i <6;i++)
        {
            lamoula[i].text = "0";
            count[i] = 0;
        }

        ouvert = false;
        droit = true;
        canvassell.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!photonView.IsMine)
        {
            return;
        }
      

        if (AK.active)
        {
            AKImage.color=Color.white;
        }
        else
        {
            AKImage.color = Color.black;
        }
        if (Sword.active)
        {
            SwordImage.color=Color.white;
        }
        else
        {
            SwordImage.color = Color.black;
        }

        
            
        
        if (Input.GetKeyUp(KeyCode.I))
        {
            inventaire.SetActive(!ouvert);
            ouvert = !ouvert;
            if (ouvert)
            {
                
                player.isOutOfFocus = true;
                
            }
            else
            {
                player.isOutOfFocus = false;
            }


        }
    }
    public void Add(Loot item, int amount)
    {
        
        count[(int)item] += amount;

        lamoula[(int) item].text = count[(int) item].ToString();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag=="PNJ")
        {
            if (photonView.IsMine)
            {
                BuySell.SetActive(true);
            }
            
        }
    }

    public void OnTriggerStay(Collider other)
    {
        if (droit && Input.GetKeyUp(KeyCode.F))
        {
            BuySell.SetActive(false);
            droit = false;
        }
        if (droit && Input.GetKeyUp(KeyCode.O))
        {
            BuySell.SetActive(false);
            canvassell.SetActive(true);
            
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag=="PNJ")
        {
            
                BuySell.SetActive(false);
                canvassell.SetActive(false);
        }

        droit = true;
    }
}
