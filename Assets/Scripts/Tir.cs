using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
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
    public GameObject PunchDroit;
    public GameObject Attakpoint;
    public float punchrange;
    public LayerMask PlayerLayer;
    public LayerMask MonsterLayer;
    public GameObject Punch;
    public GameObject PunchImpact;
    public GameObject PunchImpact2;


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

    [Tooltip("The Player Script Reference")]
    private Player player;

    #endregion

    #region Mono Callbacks

    void Awake()
    {
        player = GetComponent<Player>();
        anim = AK.GetComponent<Animator>();
        weaponManager = GetComponent<WeaponManager>();
        mouseLook = GetComponentInChildren<MouseLook>();
    }


    void Update()
    {
        if (photonView.IsMine && !player.isOutOfFocus)
        {
            WeaponShoot();
            reload = AK.GetComponent<WeaponHandler>().recharge;
            if (!Sword.active && reload &&!Punch.active)
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
            mouseLook.ApplyRecoil(weapon.recoil[i].normalized.x *2 , weapon.recoil[i].normalized.y * 2);
            nextTimeToFire = Time.time + 1f / weapon.fireRate; //weaponManager.GetCurrentSelectedWeapon().fireRate
            weapon.ShootAnimation();
            Shoot();
            i++;
        }
        else
        {
            weapon.StopShootAnimation();
            weapon.StopPunchAnimation();
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
        WeaponHandler weapon = weaponManager.GetCurrentSelectedWeapon();

        munition = weapon.Ammunition;
        reload = weapon.recharge;

        if (AK.active && munition>0 && !reload)
        {
            RaycastHit hit;
            Vector3 direction = mainCam.transform.TransformDirection(RandomInsideCone(radius).normalized);

            if (Physics.Raycast(new Ray(mainCam.transform.position, direction), out hit))
            {
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
                    if (gameObject.GetComponent<Player>().team==hit.transform.gameObject.GetComponent<Player>().team)
                    {
                        return;
                    }
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
                    if (gameObject.GetComponent<Player>().team==hit.transform.gameObject.GetComponent<Player>().team)
                    {
                        return;
                    }
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
            weapon.DecreaseAmmunition();
        }

        if (Punch.active)
        {

            /*Collider[] hitplayer = Physics.OverlapSphere(Attakpoint.transform.position, punchrange, PlayerLayer);
            foreach (Collider player in hitplayer)
            {
                Debug.Log("jt ai nique ta grand mere " + player.name);
            } */

            RaycastHit hit2;

            Vector3 direction2 = mainCam.transform.TransformDirection(RandomInsideCone(radius).normalized);

            if (Physics.Raycast(new Ray(mainCam.transform.position, direction2), out hit2))
            {
                Debug.DrawLine(mainCam.transform.position, hit2.point);
                if (hit2.transform.GetComponent<Rigidbody>() != null && Vector3.Distance(transform.position,hit2.transform.position)<punchrange)
                {
                    hit2.transform.GetComponent<Rigidbody>().AddForce(transform.forward * 200);
                }
                if (hit2.transform.CompareTag("Metal")&& Vector3.Distance(transform.position,hit2.transform.position)<punchrange)
                {
                    GameObject Gucci =
                        Instantiate(PunchImpact2, hit2.point,
                            Quaternion.FromToRotation(Vector3.forward, hit2.normal)) as GameObject;
                    Destroy(Gucci, 2f);
                }

                if (hit2.transform.CompareTag("Player")&& Vector3.Distance(transform.position,hit2.transform.position)<punchrange)
                {
                    if (hit2.transform.GetComponent<PhotonView>().IsMine) return;
                    GameObject Blood =
                        Instantiate(PunchImpact, hit2.point,
                            Quaternion.FromToRotation(Vector3.forward, hit2.normal)) as GameObject;
                    Destroy(Blood, 2f);
                }

                if (hit2.transform.CompareTag("Enemy")&& Vector3.Distance(transform.position,hit2.transform.position)<punchrange)
                {
                    GameObject Blood =
                        Instantiate(PunchImpact, hit2.point,
                            Quaternion.FromToRotation(Vector3.forward, hit2.normal)) as GameObject;
                    Destroy(Blood, 2f);
                    hit2.transform.gameObject.GetComponent<Combatmanager>()
                        .TakeDamage(weaponManager.GetCurrentSelectedWeapon().damage);
                }

                if (hit2.transform.CompareTag(Tags.PLAYER_TAG)&& Vector3.Distance(transform.position,hit2.transform.position)<punchrange)
                {
                    Debug.Log("Hit player");
                    hit2.transform.gameObject.GetComponent<PhotonView>().RPC("ApplyDamage", RpcTarget.All,
                        photonView.Owner.NickName, weaponManager.GetCurrentSelectedWeapon().damage, WeaponName.PUNCH);
                }
                else
                {
                    //Debug.DrawRay(mainCam.transform.position, hit.point, Color.red, 2f);
                }
            }
            else
            {
                //Did not it...
            }
        }

        if (Sword.active)
        {
             RaycastHit hit3;

                        Vector3 direction3 = mainCam.transform.TransformDirection(RandomInsideCone(radius).normalized);

                        if (Physics.Raycast(new Ray(mainCam.transform.position, direction3), out hit3))
                        {
                            Debug.Log("Did hit " + hit3.collider.name);
                            Debug.DrawLine(mainCam.transform.position, hit3.point);
                            if (hit3.transform.GetComponent<Rigidbody>() != null && Vector3.Distance(transform.position,hit3.transform.position)<punchrange)
                            {
                                hit3.transform.GetComponent<Rigidbody>().AddForce(transform.forward * 200);
                            }
                            if (hit3.transform.CompareTag("Metal")&& Vector3.Distance(transform.position,hit3.transform.position)<punchrange)
                            {
                                GameObject Gucci =
                                    Instantiate(PunchImpact2, hit3.point,
                                        Quaternion.FromToRotation(Vector3.forward, hit3.normal)) as GameObject;
                                Destroy(Gucci, 2f);
                            }

                            if (hit3.transform.CompareTag("Player")&& Vector3.Distance(transform.position,hit3.transform.position)<punchrange)
                            {
                                if (hit3.transform.GetComponent<PhotonView>().IsMine) return;
                                GameObject Blood =
                                    Instantiate(PunchImpact, hit3.point,
                                        Quaternion.FromToRotation(Vector3.forward, hit3.normal)) as GameObject;
                                Destroy(Blood, 2f);
                            }

                            if (hit3.transform.CompareTag("Enemy")&& Vector3.Distance(transform.position,hit3.transform.position)<punchrange)
                            {
                                GameObject Blood =
                                    Instantiate(PunchImpact, hit3.point,
                                        Quaternion.FromToRotation(Vector3.forward, hit3.normal)) as GameObject;
                                Destroy(Blood, 2f);
                                hit3.transform.gameObject.GetComponent<Combatmanager>()
                                    .TakeDamage(weaponManager.GetCurrentSelectedWeapon().damage);
                            }

                            if (hit3.transform.CompareTag(Tags.PLAYER_TAG)&& Vector3.Distance(transform.position,hit3.transform.position)<punchrange)
                            {
                                Debug.Log("Hit player");
                                hit3.transform.gameObject.GetComponent<PhotonView>().RPC("ApplyDamage", RpcTarget.All,
                                    photonView.Owner.NickName, weaponManager.GetCurrentSelectedWeapon().damage, WeaponName.PUNCH);
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

    }
}
