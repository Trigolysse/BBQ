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
public enum Bigitem
{
    Key, grenade, Armor, AK, Sword, Metal, Wood, Rocks
}



public class SimpleInv : MonoBehaviourPunCallbacks
{
    private bool ouvert;
    public GameObject inventaire;
    private int[] Lootprice = { 15, 15, 15, 30, 50, 75 };
    public bool[] Bigitem;
    public Text[] lamoula;
    public Text[] lamoula2;
    public Text money2;
    public Text moneytext;
    public Image[] allimage;
    private Player player;
    public int money;
    private Tir tir;
    private PlayerMovement playermovement;
    private PlayerSetup playersetup;
    public GameObject Lookroot;


    public int[] count = new int[6];
    // Start is called before the first frame update
    void Start()
    {
        money = 1000;
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
        Debug.Log(count[5]);    
        if (!photonView.IsMine)
        {
            return;
        }
        for(int i = 0; i < 6; i++)
        {
            lamoula2[i].text = lamoula[i].text;
        }
        money2.text = moneytext.text;


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
        switch(Bigmarra)
        {

            case (Bigitem)0:
                {
                    if(money >= 1000 && !Bigitem[0])
                    {
                        money -= 1000;
                        Bigitem[0] = true;
                        allimage[0].color = Color.white;
                        ima.color = Color.black;
                    }
                    break;
                }
            case (Bigitem)1:
                {
                    if(count[5] >= 3 && !Bigitem[1]) 
                    {
                        count[5] -= 3;
                        lamoula[5].text = count[5].ToString();
                        Bigitem[1] = true;
                        allimage[1].color = Color.white;
                        ima.color = Color.black;

                    }
                    break;
                }
            case (Bigitem)2:
                {
                    if(count[4] >= 5 && !Bigitem[2])
                    {
                        count[4] -= 5;
                        lamoula[4].text = count[4].ToString();
                        Bigitem[2] = true;
                        allimage[2].color = Color.white;
                        ima.color = Color.black;

                    }
                    break;
                }
            case (Bigitem)3:
                {
                    if(count[5] >= 5 && count[4] >= 10 && count[3] >= 20 && !Bigitem[3])
                    {
                        count[5] -= 5;
                        lamoula[5].text = count[5].ToString();
                        count[4] -= 10;
                        lamoula[4].text = count[4].ToString();
                        count[3] -= 20;
                        lamoula[3].text = count[3].ToString();
                        Bigitem[3] = true;
                        allimage[3].color = Color.white;
                        ima.color = Color.black;
                    }
                    break;
                }
            case (Bigitem)4:
                {
                    if(count[4] >= 8 && count[3] >= 8 && !Bigitem[4])
                    {
                        count[4] -= 8;
                        lamoula[4].text = count[4].ToString();
                        count[3] -= 8;
                        lamoula[3].text = count[3].ToString();
                        Bigitem[4] = true;
                        allimage[4].color = Color.white;
                        ima.color = Color.black;
                    }
                    break;
                }
            case (Bigitem)5:
                {
                    Debug.Log("oui");
                    if(money >= 150)
                    {
                        money -= 150;
                        count[5] += 1;
                        lamoula[5].text = count[5].ToString();
                    }
                    break;
                }
            case (Bigitem)6:
                {
                    if (money >= 60)
                    {
                        money -= 60;
                        count[3] += 1;
                        lamoula[3].text = count[3].ToString();
                    }
                    break;
                }
            default:
                {
                    if (money >= 100)
                    {
                        money -= 100;
                        count[4] += 1;
                        lamoula[4].text = count[4].ToString();
                    }
                    break;
                }
        }
    }



   
}
