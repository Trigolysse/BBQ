using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    [SerializeField]
    private int movementSpeed;

    public bool recharge;
    private float temps;
    private float tempsamo;

    public GameObject Epee;

    public Text Amo;
    public Text TotalAmo;
    private Animator AttakSword;
    private Animator PunchAttak;

    #endregion

    #region Public Fields

    public WeaponAim weaponAim;
    public WeaponFireType fireType;
    public WeaponBulletType bulletType;
    public int damage;
    public float fireRate;
    public int magazineCapacity;
    public int Initialmmunition;
    public int ammunition;
    public GameObject bullet;
    public int amo;
    
    public GameObject attackPoint;
    public Vector2[] recoil;
    public GameObject Punch;
    private Animator anim;
    public GameObject Ernesto;
    public Camera lacamera;

    [HideInInspector]
    [Tooltip("The Player's Current Ammunition")]
    public int Ammunition;

    [HideInInspector]
    [Tooltip("The Player's Current Magazine")]
    public int Magazine;

    #endregion

    void balanceAmmunution()
    {
        int shift = (Magazine > magazineCapacity ? magazineCapacity - Ammunition : Magazine);
        this.Ammunition += shift;
        this.Magazine -= shift;
    }

    #region Mono Callbacks

    void Awake()
    {
        if(Initialmmunition / magazineCapacity > 1)
            Ammunition = magazineCapacity;
        else
            Ammunition = Initialmmunition % magazineCapacity;

        Magazine = Initialmmunition - Ammunition;
        temps = 0;
        tempsamo = 0;
        recharge = false;
        AttakSword = Epee.GetComponent<Animator>();
        PunchAttak = Punch.GetComponent<Animator>();
        anim = Ernesto.GetComponent<Animator>();
        anim.SetBool("Dead",false);
        anim.SetBool("Run",false);
        PunchAttak.SetBool("Parade",false);

        
        ammunition = 32;
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

    private void Update()
    {
        if (!Epee.active && !Punch.active)
        {
            if (!recharge)
            {
                if (Input.GetKeyDown(KeyCode.R))
                {
                    lacamera.GetComponent<AudioSource>().Play();
                    recharge = true;
                    temps = 0;
                }
            }
            else
            {
                temps += Time.deltaTime;
                recharge = true;
                if (temps>1.40f)
                {
                    balanceAmmunution();
                    recharge = false;
                }
            }
        }
    }


    #region Public Methods

    public void DecreaseAmmunition() => Ammunition -= 1;

    public void ShootAnimation()
    {
        if (!Epee.active && !Punch.active)
        {
            if (ammunition<=0 || recharge)
            {
                bullet.SetActive(false);
                return;
            }
            else
            {
                GetComponent<AudioSource>().Play();
                bullet.SetActive(true);
                animator.SetTrigger(AnimationTags.FIRE_TRIGGER);
                Amo.text = ammunition.ToString();
            }
        }
        else if (!Punch.active)
        {
            AttakSword.SetBool("Shoot",true);
        }
        else
        {
            PunchAttak.SetBool("Punch",true);
            PunchAttak.SetBool("Parade",false);
            
        }
    }

    public void StopShootAnimation()
    {
        if (Epee.active)
        {
             AttakSword.SetBool("Shoot",false);
        }
    }

    public void StopPunchAnimation()
    {
        if (Punch.active)
        {
            PunchAttak.SetBool("Punch",false);
        }
        
    }

    public void WalkAnimation()
    {
        anim.SetBool("Run",true);
    }

    public void StopWalkAnimation()
    {
        anim.SetBool("Run",false);
    }

    public void InspectAnimation()
    {
        animator.SetTrigger(AnimationTags.INSPECT_TRIGGER);
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
