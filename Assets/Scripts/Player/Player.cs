using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviourPunCallbacks, IPunObservable
{
    public int Health;
    private RaycastHit hit;

    // Start is called before the first frame update
    void Start()
    {
        Health = 100;
        GetComponent<WeaponManager>().GetCurrentSelectedWeapon();
    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine)
        {

        }

    }

    public void ApplyDamage(int damage)
    {
        Health -= damage;
        Debug.Log(Health);
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            // We own this player: send the others our data
            stream.SendNext(Health);
        }
        else
        {
            // Network player, receive data
            this.Health = (int)stream.ReceiveNext();
        }
    }
}
