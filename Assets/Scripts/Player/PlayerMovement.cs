using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviourPunCallbacks
{
    #region Private Fields

    private CharacterController characterController;
    private Vector3 moveDirection;
    private float gravity = 20f;
    private float verticalVelocity;
    private Vector3 TargetPosition;
    private int Life;
    private Player player;
    private WeaponManager weaponManager;
    private Animator anim;
    public GameObject Ernesto;

    public int Life1 => Life;

    #endregion

    #region Public Fields

    public float speed = 5f;
    public float jump_Force = 10f;

    #endregion

    #region Mono Callbacks
    void Awake()
    {
        player = GetComponent<Player>();
        characterController = GetComponent<CharacterController>();
        weaponManager = GetComponent<WeaponManager>();
        anim = Ernesto.GetComponent<Animator>();
    }

    void Update()
    {
        if (photonView.IsMine)
            MoveThePlayer();
        else
        {
            //characterController.transform.position = TargetPosition;//Vector3.Lerp(transform.position, TargetPosition, 0.5f);
        }
        
    }

    #endregion

    #region Private Methods

    void MoveThePlayer()
    {
        if (!player.isOutOfFocus)
            moveDirection = new Vector3(Input.GetAxis(Axis.HORIZONTAL), 0f, Input.GetAxis(Axis.VERTICAL));

        if (moveDirection != Vector3.zero)
        {
            weaponManager.GetCurrentSelectedWeapon().WalkAnimation();
            //anim.SetBool("Run",true);
        }
        else
        {
            weaponManager.GetCurrentSelectedWeapon().StopWalkAnimation();
            //anim.SetBool("Run",false);
        }
            

        moveDirection = transform.TransformDirection(moveDirection);
        moveDirection *= weaponManager.GetCurrentSelectedWeapon().speed * Time.deltaTime;
        ApplyGravity();
        characterController.Move(moveDirection);
    }

    void Walk()
    {

    }

    void ApplyGravity()
    {
        verticalVelocity -= gravity * Time.deltaTime;
        PlayerJump();
        moveDirection.y = verticalVelocity * Time.deltaTime;
    }

    void PlayerJump()
    {
        if (characterController.isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            verticalVelocity = jump_Force;
        }
    }

    #endregion

    #region IPunObservable Implementation

    //public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    //{
    //    if (stream.IsWriting)
    //    {
    //        stream.SendNext(transform.position);
    //    }
    //    else
    //    {
    //        TargetPosition = (Vector3)stream.ReceiveNext();
    //    }
    //}

    #endregion
}