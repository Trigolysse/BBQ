using Photon.Pun;
using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSetup : MonoBehaviourPunCallbacks
{
    [SerializeField]
    Behaviour[] componentsToDisable;
    Camera sceneCamera;
    Player player;
    Quest quest;
    [SerializeField]
    private Canvas teamCanvas;

    private void Awake()
    {
        player = GetComponent<Player>();
        quest = GetComponent<Quest>();
    }

    void Start()
    {
       
        //AssignRemoteLayer();
        // If this player is not you
        if (!photonView.IsMine)
        {
            for (int i = 0; i < componentsToDisable.Length; i++)
            {
                componentsToDisable[i].enabled = false;
            }
        }
        else
        {
            player.isOutOfFocus = true;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            teamCanvas.GetComponent<Canvas>().enabled = true;
            sceneCamera = Camera.main;
        }

        gameObject.name = photonView.Owner.NickName;
        photonView.RPC("RegisterPlayer", RpcTarget.All);
    }
    
    private void OnDisable()
    {
        if (sceneCamera != null)
        {
            sceneCamera.gameObject.SetActive(true);
        }
    }
    bool show = false;

    private void OnGUI()
    {
        if (Input.GetKey(KeyCode.F4))
        {
            GetComponent<PhotonView>().RPC("ChatMessage", RpcTarget.All, "Trololol");
        }
    }

    #region Private Methods

    private void AssignRemoteLayer()
    {
        if (photonView.IsMine)
        {
            gameObject.layer = LayerMask.NameToLayer("LocalPlayer");
        }
        else
        {
            gameObject.layer = LayerMask.NameToLayer("RemotePlayer");
        }
           
    }

    [PunRPC]
    private void SetName()
    {
        // If this player is not you
        if (!photonView.IsMine)
        {
            this.transform.GetChild(1).gameObject.layer = 0;
            //ChangeLayersRecursively(this.transform.GetChild(0).gameObject.transform.GetChild(2).gameObject.transform, 0);
        }
        if (photonView.IsMine)
        {
            transform.name = photonView.Owner.NickName;
        }
    }

    [PunRPC]
    private void RegisterPlayer()
    {
        // If this player is not you
        if (!photonView.IsMine)
        {
            this.transform.GetChild(1).gameObject.layer = 0;
            //ChangeLayersRecursively(this.transform.GetChild(0).gameObject.transform.GetChild(2).gameObject.transform, 0);
        }
        if(photonView.IsMine)
        {
            transform.name = photonView.Owner.NickName;
        }  
    }

    public void SetBlueTeam()
    {
        player.team = Teams.BLUE;
        if(teamCanvas != null)
        {
            teamCanvas.GetComponent<Canvas>().enabled = false;
        }
        player.isOutOfFocus = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        GameObject.FindGameObjectWithTag("PlayerUI").GetComponent<PlayerUI>().SetTeamIndicator(true);
        player.GetComponent<Player>().Teleport();
    }

    public void SetRedTeam()
    {
        player.team = Teams.RED;
        if (teamCanvas != null)
        {
            teamCanvas.GetComponent<Canvas>().enabled = false;
        }
        
        player.isOutOfFocus = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        GameObject.FindGameObjectWithTag("PlayerUI").GetComponent<PlayerUI>().SetTeamIndicator(true);
        player.GetComponent<Player>().Teleport();
    }


    private void ChangeLayersRecursively(Transform trans, int layer)
    {
        
        foreach (Transform child in trans)
        {
            
            child.gameObject.layer = layer;
            ChangeLayersRecursively(child, layer);
        }
        return;
    }

    #endregion
}
