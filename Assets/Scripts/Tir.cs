    using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class Tir : MonoBehaviourPunCallbacks
{

    #region Public Fields

    public Camera mainCam;
    public GameObject EmptyPrefab;

    #endregion

    #region Private Fields

    private RaycastHit hit;
    private float nextTimeToFire;

    #endregion


    #region Mono Callbacks

    void Start()
    {
        
    }

    void Update()
    {
        WeaponShoot();
    }

    #endregion

    void WeaponShoot()
    {
        // if we press and hold left mouse click AND
        // if Time is greater than the nextTimeToFire
        if (Input.GetMouseButton(0) && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / 7; //weaponManager.GetCurrentSelectedWeapon().fireRate
            //ShootAnimation();
            photonView.RPC("Shoot", RpcTarget.All);
        }
    }

    [PunRPC]
    void Shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(mainCam.transform.position, mainCam.transform.forward, out hit))
        {
            Debug.Log("Did hit " + hit.collider.name);
            Debug.DrawRay(mainCam.transform.position, mainCam.transform.forward * 100, Color.green, 2f);

            if (hit.transform.GetComponent<Rigidbody>() != null)
            {
                hit.transform.GetComponent<Rigidbody>().AddForce(transform.forward * 200);
            }
            if (hit.transform.CompareTag("Planet"))
            {
                Debug.Log("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");
                GameObject Go = Instantiate(EmptyPrefab,hit.point,Quaternion.FromToRotation(Vector3.forward,hit.normal)) as GameObject;
                Destroy(Go,8f);    
            }
            if (hit.transform.CompareTag(Tags.PLAYER_TAG))
            {
                //hit.transform.GetComponent<HealthScript>().ApplyDamage(weaponManager.GetCurrentSelectedWeapon().damage);
            }
            else
            {
                Debug.DrawRay(mainCam.transform.position, mainCam.transform.forward * 100, Color.red, 2f);
            }
        }
        else
        {
            //Did not it...
            Debug.DrawRay(mainCam.transform.position, mainCam.transform.forward * 100, Color.white, 2f);
            Debug.Log("Did not it");
        }

    }

}
