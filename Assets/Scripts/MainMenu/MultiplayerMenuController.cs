using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiplayerMenuController : MonoBehaviourPunCallbacks
{

    [SerializeField]
    int mainMenuIndex;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void NavigateToMainMenu()
    {
        PhotonNetwork.LoadLevel(mainMenuIndex);
    }

}