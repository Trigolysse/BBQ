using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using Random = System.Random;
using UnityEngine.AI;

public class Combatmanager : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public int respawntime;
    public Vector3 cimetiere;
    private bool dead;
    private float timer;
    private NavMeshAgent Agent;
    private Vector3 Initialposition;

    // Start is called before the first frame update
    void Start()
    {
        Agent = GetComponent<NavMeshAgent>();
        currentHealth = maxHealth;
        dead = false;
        Initialposition = transform.position;
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth == 0)
            Death();
        if(dead)
        {
            timer -= Time.deltaTime;
        }
        if(timer <= 0)
        {
            respawn();
        }
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
        Agent.Warp(cimetiere);
        dead = true;

    }
    public void respawn()
    {
        dead = false;
        Agent.Warp(Initialposition);
        timer = respawntime;
        currentHealth = maxHealth;
    }
}
