using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffetDeTir : MonoBehaviour
{
    public GameObject Boomeffet;
    private bool tir;
    public GameObject AK;
    private int munition;
    private bool reload;
    


    private void Start()
    {
        tir = false;
    }

    void Update()
    {
        reload = AK.GetComponent<WeaponHandler>().recharge;
        if (Input.GetMouseButtonDown(0) && !reload)
        {
            tir = true;
            GameObject tireffect= Instantiate(Boomeffet, transform) as GameObject;
            Destroy(tireffect,1f);
        }
        else
        {
            if (tir && !Input.GetMouseButtonUp(0) && !reload)
            {
                munition = AK.GetComponent<WeaponHandler>().ammunition;
                if (munition>0)
                {
                   tir = true;
                    GameObject tireffect= Instantiate(Boomeffet, transform) as GameObject;
                    Destroy(tireffect,1f); 
                }
                else
                {
                    tir = false;
                }
                
            }
            else
            {
                tir = false;
            }
        }
    }
}
