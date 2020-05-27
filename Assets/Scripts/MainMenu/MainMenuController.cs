using System;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviourPunCallbacks
{

    [SerializeField]
    public Button multiplayerButton;
    public Button quitButton;

    [SerializeField]
    private int multiplayerMenuIndex;
    [SerializeField]
    private int localPlayIndex;
    
    
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void NavigateToMultiplayerMenu()
    {
        PhotonNetwork.LoadLevel(multiplayerMenuIndex);
    }

    public void NavigateToLocalPlay()
    {
        PhotonNetwork.LoadLevel(localPlayIndex);
    }

    public override void OnConnectedToMaster()
    {
        multiplayerButton.interactable = true;
        quitButton.interactable = true;
    }

    public void OnApplicationQuit()
    {
        Application.Quit();
    }
}