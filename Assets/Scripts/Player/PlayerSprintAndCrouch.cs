﻿using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerSprintAndCrouch : MonoBehaviourPunCallbacks, IPunObservable
{
    #region Private Fields

    private PlayerMovement playerMovement;
    private Transform look_Root;
    private float stand_Height = 1.6f;
    private float crouch_Height = 1f;
    public bool isCrouching;
    private float sprint_Value = 100f;
    private Animator anim;
    public GameObject Ernesto;
    private CharacterController characterController;

    #endregion

    #region Public Fields

    public float sprint_Speed;
    public float move_Speed;
    public float crouch_Speed = 2f;
    public float sprint_Treshold = 10f;

    #endregion

    void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        characterController = GetComponent<CharacterController>();
    }

    void Start()
    {
        move_Speed = playerMovement.speed;
        sprint_Speed = playerMovement.speed * 2;
        anim = Ernesto.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine)
        {
            Sprint();
            Crouch();
        }
        else
        {
            if (isCrouching)
            {
                // if remote player is crouching then set him to crouch postition
                characterController.height = crouch_Height;
            }
            else
            {
                // if remote player is not crouching then set him to standing up postition
                characterController.height = stand_Height;
            }
        }

    }

    void Sprint()
    {
        // if we have stamina we can sprint
        if (sprint_Value > 0f)
        {
            
            if (Input.GetKeyDown(KeyCode.LeftControl) && !isCrouching)
            {
                playerMovement.speed = sprint_Speed;
                anim.SetBool("Running",true);
            }
        }
        
        if (Input.GetKeyUp(KeyCode.LeftControl) && !isCrouching)
        {
            anim.SetBool("Running",false);
            playerMovement.speed = move_Speed;
        }
        if (Input.GetKey(KeyCode.LeftControl) && !isCrouching)
        {
            sprint_Value -= sprint_Treshold * Time.deltaTime;

            if (sprint_Value <= 0f)
            {
                sprint_Value = 0f;
                // reset the speed and sound
                playerMovement.speed = move_Speed;
            }
        }
        else
        {
            if (sprint_Value != 100f)
            {
                sprint_Value += (sprint_Treshold / 2f) * Time.deltaTime;
                if (sprint_Value > 100f)
                {
                    sprint_Value = 100f;
                }
            }
        }
    }

    void Crouch()
    {
     
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            // if we are crouching - stand up
            if (isCrouching)
            {
                characterController.height = stand_Height;
                playerMovement.speed = move_Speed;
                isCrouching = false;
            }
            else
            {
                // if we are not crouching - crouch
                characterController.height = crouch_Height;
                playerMovement.speed = crouch_Speed;
                isCrouching = true;
            }
        }
    }

    #region IPunObservable Implementation

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(isCrouching);
        }
        else
        {
            this.isCrouching = (bool)stream.ReceiveNext();
        }
    }

    #endregion
}