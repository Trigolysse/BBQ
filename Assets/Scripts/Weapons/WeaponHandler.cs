﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponAim
{
    NONE,
    SELF_AIM,
    AIM
}

public enum WeaponFireType
{
    SINGLE,
    MULTIPLE
}

public enum WeaponBulletType
{
    BULLET,
    ARROW,
    SPEAR,
    NONE
}

public class WeaponHandler : MonoBehaviour
{

    #region Private Fields

    private Animator animator;
    [SerializeField]
    private GameObject muzzleFlash;
    [SerializeField]
    private AudioSource shootSound, reload_Sound;

    #endregion

    #region Public Fields

    public WeaponAim weaponAim;
    public WeaponFireType fireType;
    public WeaponBulletType bulletType;
    public int damage;
    public float fireRate;
    public int magazineCapacity;
    public int ammunition;
    public GameObject attackPoint;
    public Vector2[] recoil;

    #endregion

    #region Mono Callbacks

    void Awake()
    {
        animator = GetComponent<Animator>();
        recoil = new Vector2[] {
            Vector2.zero,
            new Vector2(-2f, 1.39f),
            new Vector2(-3.84f, 1.17f),
            new Vector2(-5.51f, 3.38f),
            new Vector2(-7.01f, 5.08f),
            new Vector2(-8.34f, 5.09f),
            new Vector2(-9.48f, 4.42f),
            new Vector2(-10.44915f, 3.250455f),
            new Vector2(-11.22279f, 1.73545f),
            new Vector2(-11.8046f, 0.04893398f),
            new Vector2(-12.19056f, -1.641158f),
            new Vector2(-12.58713f, -3.166891f),
            new Vector2(-13.32077f, -4.360331f),
            new Vector2(-14.32128f, -5.053545f),
            new Vector2(-15.51103f, -5.090651f),
            new Vector2(-16.81242f, -4.489915f),
            new Vector2(-18.14783f, -3.382552f),
            new Vector2(-19.43966f, -1.899585f),
            new Vector2(-20.61031f, -0.1720295f),
            new Vector2(-21.58213f, 1.669086f),
            new Vector2(-22.27755f, 3.492748f),
            new Vector2(-22.61893f, 5.16793f),
            new Vector2(-22.81778f, 6.563614f),
            new Vector2(-23.37389f, 7.548776f),
            new Vector2(-24.21139f, 7.992399f),
            new Vector2(-25.23734f, 7.512226f),
            new Vector2(-26.35886f, 6.063792f),
            new Vector2(-26.35886f, 6.063792f),
            new Vector2(-27.48302f, 4.117367f),
            new Vector2(-28.51692f, 2.143932f),
            new Vector2(-29.36766f, 0.6144824f)
        };
    }

    #endregion

    #region Public Methods

    public void DecreaseAmmunition() => ammunition -= 1;

    public void ShootAnimation()
    {
        //Debug.Log("ShootAnimation");
        animator.SetTrigger(AnimationTags.SHOOT_TRIGGER);
        DecreaseAmmunition();
    }

    public void BlockAnimation()
    {
        //Debug.Log("BlockAnimation");
        animator.SetBool(AnimationTags.BLOCK_TRIGGER, true);
    }

    public void UnBlockAnimation()
    {
        //Debug.Log("BlockAnimation");
        animator.SetBool(AnimationTags.BLOCK_TRIGGER, false);
    }

    public void Aim(bool canAim)
    {
        animator.SetBool(AnimationTags.AIM_PARAMETER, canAim);
    }

    void TurnOnMuzzleFlash()
    {
        muzzleFlash.SetActive(true);
    }

    void TurnOffMuzzleFlash()
    {
        muzzleFlash.SetActive(false);
    }

    void PlayShootSound()
    {
        shootSound.Play();
    }

    void Play_ReloadSound()
    {
        reload_Sound.Play();
    }

    void Turn_On_AttackPoint()
    {
        attackPoint.SetActive(true);
    }

    void Turn_Off_AttackPoint()
    {
        if (attackPoint.activeInHierarchy)
        {
            attackPoint.SetActive(false);
        }
    }

    #endregion

}
