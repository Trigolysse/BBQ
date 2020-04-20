using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combatmanager : MonoBehaviour
{
    public int maxhealth;
    public int respawntime;
    private int health;

    // Start is called before the first frame update
    void Start()
    {
        
        health = maxhealth;
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(health);
        if (health == 0)
            Death();
        
    }

    public void TakeDamage(int damage)
    {
        if (health - damage < 0)
            health = 0;
        else
            health -= damage;

    }


    public void Heal(int heal)
    {
        if (health + heal > maxhealth)
            health = maxhealth;
        else
            health += heal;
    }

    public void Death()
    { 
    }
}
