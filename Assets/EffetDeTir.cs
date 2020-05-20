using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffetDeTir : MonoBehaviour
{
    public GameObject Boomeffet;
    private bool tir;


    private void Start()
    {
        tir = false;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            tir = true;
            GameObject tireffect= Instantiate(Boomeffet, transform) as GameObject;
            Destroy(tireffect,1f);
        }
        else
        {
            if (tir && !Input.GetMouseButtonUp(0))
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
    }
}
