using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Combatmanager : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public int respawntime;
    public Vector3 cimetiere;
    private bool isDead;
    private float timer;
    private NavMeshAgent Agent;
    private Vector3 Initialposition;
    public GameObject healthBar;

    #region Mono Callbacks

    private void Awake()
    {
        Agent = GetComponent<NavMeshAgent>();
    }

    void Start()
    {   
        currentHealth = maxHealth;
        isDead = false;
        Initialposition = transform.position;
        healthBar.GetComponent<MonsterHEALTHBAR>().SetMaxHealth(currentHealth);
    }

    void Update()
    {
        if (currentHealth == 0)
            Death();
        if(isDead)
        {
            timer -= Time.deltaTime;
        }
        if(timer <= 0)
        {
            Respawn();
        }
    }

    #endregion


    public void TakeDamage(int damage)
    {
        if (currentHealth - damage < 0)
        {
            currentHealth = 0;
            healthBar.GetComponent<MonsterHEALTHBAR>().SetHealth(currentHealth);
        }
        else
        {
            currentHealth -= damage;
            healthBar.GetComponent<MonsterHEALTHBAR>().SetHealth(currentHealth);
        }
    }


    public void Heal(int heal)
    {
        if (currentHealth + heal > maxHealth)
        {
            currentHealth = maxHealth;
            healthBar.GetComponent<MonsterHEALTHBAR>().SetHealth(currentHealth);
        }

        else
        {
            currentHealth += heal;
            healthBar.GetComponent<MonsterHEALTHBAR>().SetHealth(currentHealth);
        }
            
    }

    public void Death()
    {
        Agent.Warp(cimetiere);
        isDead = true;

    }
    public void Respawn()
    {
        isDead = false;
        Agent.Warp(Initialposition);
        timer = respawntime;
        currentHealth = maxHealth;
        healthBar.GetComponent<MonsterHEALTHBAR>().SetHealth(currentHealth);
    }
}
