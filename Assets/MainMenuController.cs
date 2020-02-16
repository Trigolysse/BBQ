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
    Button multiplayerButton;

    [SerializeField]
    int multiplayerMenuIndex;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

 
    public void NavigateToMultiplayerMainMenu()
    {
        PhotonNetwork.LoadLevel(multiplayerMenuIndex);
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("You are now connected to " + PhotonNetwork.CloudRegion + " server!");
        multiplayerButton.interactable = true;
    }
}
