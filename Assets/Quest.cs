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
    // Start is called before the first frame update
    void Start()
    {
        gamemanager = GameObject.FindWithTag("Gamemanager");
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(Goal);
        Debug.Log(Reward);
    }


    public void Complete()
    {

    }
}
