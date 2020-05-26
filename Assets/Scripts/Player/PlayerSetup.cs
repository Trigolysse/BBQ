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
            sceneCamera = Camera.main;
            if (sceneCamera != null)
            {
                //sceneCamera.gameObject.SetActive(false);
            }
            //Camera.main.gameObject.SetActive(false);
            if (UnityEngine.Random.Range(0, 2) == 0)
                photonView.RPC("AssignTeam", RpcTarget.All, Teams.BLUE);
            else
                photonView.RPC("AssignTeam", RpcTarget.All, Teams.RED);

            //photonView.RPC("SetName", RpcTarget.All, Teams.RED);

            
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

    [PunRPC]
    private void AssignTeam(Teams team)
    {
        if(photonView.IsMine)
        {
            player.team = team;
        }
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
