using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combatmanager : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public int respawntime;

    // Start is called before the first frame update
    void Start()
    {
        
        currentHealth = maxHealth;
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth == 0)
            Death();
    }


    public void TakeDamage(int damage)
    {
        if (currentHealth - damage < 0)
            currentHealth = 0;
        else
            currentHealth -= damage;
        

    }


    public void Heal(int heal)
    {
        if (currentHealth + heal > maxHealth)
            currentHealth = maxHealth;
        else
            currentHealth += heal;
    }

    public void Death()
    { 
    }
}
