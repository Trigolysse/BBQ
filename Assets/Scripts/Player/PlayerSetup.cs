using Photon.Pun;
using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSetup : MonoBehaviourPunCallbacks
{
    [SerializeField]
    Behaviour[] componentsToDisable;
    Camera sceneCamera;

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
        }

        RegisterPlayer();
    }
    
    private void OnDisable()
    {
        if (sceneCamera != null)
        {
            sceneCamera.gameObject.SetActive(true);
        }
    }

    void Update()
    {

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

    private void RegisterPlayer()
    {
        // If this player is not you
        if (!photonView.IsMine)
        {
            this.transform.GetChild(1).gameObject.layer = 0;
            ChangeLayersRecursively(this.transform.GetChild(0).gameObject.transform.GetChild(2).gameObject.transform, 0);
        }
        
        //transform.name = photonView.Owner.NickName;
    }

    private void ChangeLayersRecursively(Transform trans, int layer)
    {
        foreach (Transform child in trans)
        {
            child.gameObject.layer = layer;
            ChangeLayersRecursively(child, layer);
        }
    }

    #endregion
}
