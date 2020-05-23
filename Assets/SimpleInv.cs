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

    public int[] count = new int[6];
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0;i <6;i++)
        {
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
        Debug.Log(item);
        Debug.Log(count[(int)item]);
    }
}
