﻿using System;
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
public enum Bigitem
{
    Key, grenade, Armor, AK, Sword
}



public class SimpleInv : MonoBehaviourPunCallbacks
{
    private bool ouvert;
    public GameObject inventaire;
    private int[] Lootprice = { 10, 10, 10, 20, 30, 50 };
    private int[] Bigitemprice = { 50, 50, 50, 200, 80 };
    public bool[] Bigitem;
    public Text[] lamoula;
    public Text moneytext;
    public Image[] allimage;
    public bool Sword;
    public bool AK;
    public bool Armor;
    public bool Key;
    public bool grenade;
    public int money;
    private Player player;
    private Tir tir;
    private PlayerMovement playermovement;
    private PlayerSetup playersetup;
    public GameObject Lookroot;


    public int[] count = new int[6];
    // Start is called before the first frame update
    void Start()
    {
        foreach(Image im in allimage)
        {
            im.color = Color.black;
        }

        for(int i = 0;i <6;i++)
        {
            lamoula[i].text = "0";
            count[i] = 0;
        }

        ouvert = false;
        player = gameObject.GetComponent<Player>();
        tir = gameObject.GetComponent<Tir>();
        playermovement = gameObject.GetComponent<PlayerMovement>();
        playersetup = gameObject.GetComponent<PlayerSetup>();
        

    }

    // Update is called once per frame
    void Update()
    {
        if (!photonView.IsMine)
        {
            return;
        }



        moneytext.text = money + " $";




        if (Input.GetKeyUp(KeyCode.I))
        {
            Debug.Log("oui");
            inventaire.SetActive(!ouvert);
            ouvert = !ouvert;
            if (ouvert)
            {


                GetComponent<CharacterController>().enabled = false;
                player.isOutOfFocus = true;

                Cursor.lockState = CursorLockMode.None;

                Cursor.visible = true;


            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                GetComponent<CharacterController>().enabled = true;
                player.isOutOfFocus = false;
                Cursor.visible = false;
            }


        }
    }
    public void Add(Loot item, int amount)
    {
        
        count[(int)item] += amount;

        lamoula[(int) item].text = count[(int) item].ToString();
    }

    public void Sell(Loot item)
    {
        Debug.Log(item);
        if (count[(int)item]>0)
        {
            money += Lootprice[(int)item];
            count[(int)item] -= 1;
            lamoula[(int)item].text = count[(int)item].ToString();
        }

    }
    public void Buy(Bigitem Bigmarra, Image ima)
    {
        if (!(Bigitem[(int)Bigmarra]) && money > Bigitemprice[(int)Bigmarra])
        {
            Bigitem[(int)Bigmarra] = true;
            money -= Bigitemprice[(int)Bigmarra];
            allimage[(int)Bigmarra].color = Color.white;
            ima.color = Color.black;
        }
    }



   
}
