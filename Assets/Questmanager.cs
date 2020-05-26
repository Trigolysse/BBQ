using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;
using Photon.Pun;


public class Questmanager : MonoBehaviourPunCallbacks
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /*if (PhotonNetwork.IsMasterClient)
        {
            players = GameObject.FindGameObjectsWithTag("Player");
            foreach(GameObject player in players)
            {
                player.GetComponent<Quest>().Goal = Goal;
                player.GetComponent<Quest>().Reward = Reward;
            }

        }*/
    }
}
