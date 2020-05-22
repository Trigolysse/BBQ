using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class HealthBarOther : MonoBehaviourPunCallbacks
{
    private Camera Main;
    private GameObject[] playerList;
    

    private void Start()
    {
        playerList = GameObject.FindGameObjectsWithTag("Player");
    }
    private void Update()
    {
        foreach (GameObject player in playerList)
        {
            if (player.PhotonView.IsMine)
            {
                Main = player.GetComponent<Camera>().MainCamera;
                transform.LookAt(transform.position + Main.forward);
            }
        }
    }
}
