using UnityEngine;
using UnityEngine.AI;

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
        isDead = true;

    }
    public void Respawn()
    {
        isDead = false;
        Agent.Warp(Initialposition);
        timer = respawntime;
        currentHealth = maxHealth;
    }
}
