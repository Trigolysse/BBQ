using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviourPunCallbacks
{
   public int health;
    private RaycastHit hit;

    // Start is called before the first frame update
    void Start()
    {
        health = 100;
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
        health -= damage;
        Debug.Log(health);
    }
}
