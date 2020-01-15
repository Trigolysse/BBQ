using BBQ;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ak47 : MonoBehaviour, Weapon
{
    private Animator animator;
    [SerializeField]
    private int damage;

    public int Damage { get => damage; set => value = damage ; }

   
    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShootAnimation()
    {
        Debug.Log("ShootAnimation");
        animator.SetTrigger(AnimationTags.SHOOT_TRIGGER); 
    }
}
