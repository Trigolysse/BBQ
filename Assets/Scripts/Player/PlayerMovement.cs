using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviourPunCallbacks, IPunObservable
{
    #region Private Fields

    private Rigidbody rigidbody;
    private Vector3 moveDirection;
    private float gravity = 20f;
    private float verticalVelocity;
    private Vector3 TargetPosition;

    #endregion

    #region Public Fields

    public float speed = 10f;
    public float jump_Force = 10f;

    #endregion

    #region Mono Callbacks
    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (photonView.IsMine)
        {
            MoveThePlayer();
        }  
        else
        {
            return;
        }
    }

    #endregion

    #region Private Methods

    void MoveThePlayer()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Vector3 forward = rigidbody.transform.forward;
        Vector3 tempVect = new Vector3(h, 0, v);
        
        tempVect = tempVect.normalized * speed;
        rigidbody.MovePosition(transform.position + tempVect * Time.deltaTime);
    } 


    void PlayerJump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            verticalVelocity = jump_Force;
        }
    }

    #endregion

    #region IPunObservable Implementation

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(transform.position);
        }
        else
        {
            transform.position = (Vector3)stream.ReceiveNext();
        }
    }

    #endregion
}
