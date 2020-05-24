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

    public int[] count = new int[6];
    // Start is called before the first frame update
    void Start()
    {
        
        {
            
        }
        
        inventaire.SetActive(false);
        for(int i = 0;i <6;i++)
        {
            lamoula[i].text = "0";
            count[i] = 0;
        }

        ouvert = false;
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
        }
    }
    public void Add(Loot item, int amount)
    {
        
        count[(int)item] += amount;

        lamoula[(int) item].text = count[(int) item].ToString();
    }
}
