using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;
using Photon.Pun;


public class Questmanager : MonoBehaviour
{
    private GameObject[] players;
    private Random rand = new Random();
    public int Goal;
    public int Reward;

    // Start is called before the first frame update
    void Start()
    {
        if (PhotonNetwork.IsMasterClient)
            Changequest();

    }

    // Update is called once per frame
    void Update()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            players = GameObject.FindGameObjectsWithTag("Player");
            foreach(GameObject player in players)
            {
                player.GetComponent<Quest>().Goal = Goal;
                player.GetComponent<Quest>().Reward = Reward;
            }

        }


    }
    public void Changequest()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            Goal = rand.Next(10);
            Reward = rand.Next(4);
        }
    }
}
