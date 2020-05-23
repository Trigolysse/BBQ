using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Loot
{
    Purple, Yellow, Orange, Wood, Rocks, Metal
}

public class SimpleInv : MonoBehaviour
{
    private bool ouvert;
    public GameObject inventaire;
   
    public Text[] lamoula;

    public int[] count = new int[6];
    // Start is called before the first frame update
    void Start()
    {
        
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
