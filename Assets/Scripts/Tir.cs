﻿using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Tir : MonoBehaviourPunCallbacks
{

    #region Public Fields

    public Camera mainCam;
    public GameObject EmptyPrefab;
    public GameObject Bloodeffect;
    public GameObject Metaleffect;
    public float radius = 1f;
    public GameObject Sword;
    public GameObject attackPoint;
    public float SwordAttackRange;
    public LayerMask enemyLayers;
    public LayerMask monsterLayers;


    #endregion

    #region Private Fields

    private RaycastHit hit;
    private float nextTimeToFire;
    private float cooldownTime;
    private WeaponManager weaponManager;
    private MouseLook mouseLook;
    private int munition;
    public GameObject AK;
    private bool reload;
    private Animator anim;
    
    

    #endregion

    #region Mono Callbacks
    
    
    void Awake()
    {
        anim = AK.GetComponent<Animator>();
        weaponManager = GetComponent<WeaponManager>();
        mouseLook = GetComponentInChildren<MouseLook>();
    }

    void Update()
    {
        if (photonView.IsMine)
        {

            WeaponShoot();
            reload = AK.GetComponent<WeaponHandler>().recharge;
            if (!Sword.active && reload)
            {
            
                anim.SetBool("Reload", true);
            }
            else
            {
                anim.SetBool("Reload",false);
            }
            
        }
            
            
    }

    #endregion

    public GameObject killFeedObj;
    int i = 0;

    void WeaponShoot()
    {
        WeaponHandler weapon = weaponManager.GetCurrentSelectedWeapon();

        if (Input.GetMouseButtonUp(0))
        {
            i = 0;
        }

        if (Input.GetMouseButton(0) && Time.time >= nextTimeToFire)
        {
            if (i > 29)
                return;
            mouseLook.ApplyRecoil(weapon.recoil[i].normalized.x, weapon.recoil[i].normalized.y);
            nextTimeToFire = Time.time + 1f / weapon.fireRate; //weaponManager.GetCurrentSelectedWeapon().fireRate
            weapon.ShootAnimation();
            Shoot();
            i++;
        }
        else
        {
            weapon.StopShootAnimation();
        }
        if(Input.GetMouseButton(1))
        {
            if (weapon.bulletType == WeaponBulletType.NONE)
            {
                weapon.BlockAnimation();
            }
        } else
        if (weapon.bulletType == WeaponBulletType.NONE)
        {
            weapon.UnBlockAnimation();
        }
    }
    public static Vector3 RandomInsideCone(float radius)
    {
        //(sqrt(1 - z^2) * cosϕ, sqrt(1 - z^2) * sinϕ, z)
        float radradius = radius * Mathf.PI / 360;
        float z = Random.Range(Mathf.Cos(radradius), 1);
        float t = Random.Range(0, Mathf.PI * 2);
        return new Vector3(Mathf.Sqrt(1 - z * z) * Mathf.Cos(t), Mathf.Sqrt(1 - z * z) * Mathf.Sin(t), z);
    }

    void Shoot()
    {
        munition = AK.GetComponent<WeaponHandler>().ammunition;
        reload = AK.GetComponent<WeaponHandler>().recharge;
        
        
        
        if (!Sword.active && munition>0 && !reload)
        { 
                         RaycastHit hit;

        Vector3 direction = mainCam.transform.TransformDirection(RandomInsideCone(radius).normalized);
        
        if (Physics.Raycast(new Ray(mainCam.transform.position, direction), out hit))
        {
            Debug.Log("Did hit " + hit.collider.name);
            Debug.DrawLine(mainCam.transform.position, hit.point);
            if (hit.transform.GetComponent<Rigidbody>() != null)
            {
                hit.transform.GetComponent<Rigidbody>().AddForce(transform.forward * 200);
            }

            if (hit.transform.CompareTag("Planet"))
            {
                GameObject Go =
                    Instantiate(EmptyPrefab, hit.point,
                        Quaternion.FromToRotation(Vector3.forward, hit.normal)) as GameObject;
                Destroy(Go, 8f);
            }
            if (hit.transform.CompareTag("Metal"))
            {
                GameObject Gucci =
                    Instantiate(Metaleffect, hit.point,
                        Quaternion.FromToRotation(Vector3.forward, hit.normal)) as GameObject;
                Destroy(Gucci, 2f);
            }
            if (hit.transform.CompareTag("Player"))
            {
                if (hit.transform.GetComponent<PhotonView>().IsMine) return;
                GameObject Blood =
                    Instantiate(Bloodeffect, hit.point,
                        Quaternion.FromToRotation(Vector3.forward, hit.normal)) as GameObject;
                Destroy(Blood, 2f);
            }
            if (hit.transform.CompareTag("Enemy"))
            {
                GameObject Blood =
                    Instantiate(Bloodeffect, hit.point,
                        Quaternion.FromToRotation(Vector3.forward, hit.normal)) as GameObject;
                Destroy(Blood, 2f);
                hit.transform.gameObject.GetComponent<Combatmanager>().TakeDamage(weaponManager.GetCurrentSelectedWeapon().damage);
            }

            if (hit.transform.CompareTag(Tags.PLAYER_TAG))
            {
                Debug.Log("Hit player");
                hit.transform.gameObject.GetComponent<PhotonView>().RPC("ApplyDamage", RpcTarget.All, photonView.Owner.NickName, weaponManager.GetCurrentSelectedWeapon().damage, WeaponName.AK47);
            }
            else
            {
                //Debug.DrawRay(mainCam.transform.position, hit.point, Color.red, 2f);
            }
        }
        else
        {
            //Did not it...
            //Debug.DrawRay(mainCam.transform.position, hit.point, Color.white, 2f);
            Debug.Log("Did not it");
        }
        }

        if (Sword.active)
        {
            
             
                        
                        Collider[] hitenemy=Physics.OverlapSphere(attackPoint.transform.position, SwordAttackRange, enemyLayers);
                        foreach (Collider enemy in hitenemy)
                        {
                            Debug.Log("We hit " + enemy.name);
                        }
                        
                        Collider[] hitmonster=Physics.OverlapSphere(attackPoint.transform.position, SwordAttackRange, monsterLayers);
                        foreach (Collider enemy in hitmonster)
                        {
                            Debug.Log("We hit " + enemy.name);
                        }

                        Collider[] hitenemy2 = Physics.OverlapBox(attackPoint.transform.position,
                            attackPoint.transform.position, Quaternion.identity, monsterLayers);
                        foreach (Collider enemy in hitenemy2)
                        {
                            
                        }
        }
       
        
      

    }
}
