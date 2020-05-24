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
    public Text moneytext;
    public Image SwordImage;
    public Image AKImage;
    public Image ArmorImage;
    public Image KeyImage;
    public Image grenadeImage;
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
        Sword = false;
        AK = false;
        Armor = false;
        Key = false;
        grenade = false;
        SwordImage.color = Color.black;
        AKImage.color = Color.black;
        ArmorImage.color = Color.black;
        KeyImage.color = Color.black;
        grenadeImage.color = Color.black;


        inventaire.SetActive(false);
        
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
        inventaire.SetActive(false);

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


   
}
