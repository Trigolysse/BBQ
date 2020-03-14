using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class localPlayController : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void naviguateBack()
    {
        PhotonNetwork.LoadLevel(0);
    }
}
