using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;



public class Quest : MonoBehaviour
{

    public int Goal;
    public int Reward;
    public Canvas[] Goaldis;
    public Canvas[] Rewarddis;
    public Button complete;
    private GameObject gamemanager;
    public Canvas alldisp;
    public bool cancomplete;
    // Start is called before the first frame update
    void Start()
    {
        gamemanager = GameObject.FindWithTag("Gamemanager");
        alldisp.enabled = false;
        for (int i = 0; i < 10; i++)
        {
             Goaldis[i].enabled = false;
        }
        for (int i = 0; i < 4; i++)
        {
             Rewarddis[i].enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(Goal);
        Debug.Log(Reward);
        if(Input.GetKeyUp(KeyCode.P))
        {
            alldisp.enabled = !alldisp.enabled;
        }

        for(int i = 0; i <10; i++)
        {
            if (i == Goal)
                Goaldis[i].enabled = true;
            else
                Goaldis[i].enabled = false;
        }
        for (int i = 0; i < 4; i++)
        {
            if (i == Reward)
                Rewarddis[i].enabled = true;
            else
                Rewarddis[i].enabled = false;
        }
    }


    public void Complete()
    {

    }
    public void OnTriggerStay(Collider other)
    {
        if (other.tag == "PNJ")
        {
            cancomplete = true;
        }

    }
    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "PNJ")
        {
            cancomplete = false;
        }

    }

}
