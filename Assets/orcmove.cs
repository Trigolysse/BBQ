using UnityEngine;
using Random = System.Random;
using UnityEngine.AI;


public class orcmove : MonoBehaviour
{
    #region Private Fields

    private Animator Anim;
    private GameObject Player;
    private NavMeshAgent Agent;
    private Vector3 InitialPosition;
    private float attackDelay;
    private bool walk = true;
    private bool attack = false;
    private bool run = false;

    #endregion

    #region Public Fields

    [Tooltip("Distance from a player at which the Orc will start to inflict damage")]
    [SerializeField]
    private float attackDistance = 10;

    [Tooltip("Distance from a player at which the Orc will stop its normal behavior and engage the player in combat")]
    [SerializeField]
    private float aggroDistance = 20;

    [Tooltip("?? Ulysse write something here")]
    [SerializeField]
    private int patrolDistance = 10;

    [Tooltip("Frequency of a unit's basic attacks")]
    [SerializeField]
    private float attackSpeed;
    
    [Tooltip("Inflicting Damage of the Orc")]
    [SerializeField]
    private int damage = 20;

    #endregion

    #region Mono Callbacks

    private void Awake()
    {
        Agent = GetComponent<NavMeshAgent>();
        Anim = Agent.GetComponent<Animator>();
        Player = GameObject.FindGameObjectWithTag("Player");
        attackDelay = 0;
    }

    void Start()
    {   
        InitialPosition = Agent.transform.position;
        Walk();   
        Anim.SetBool("run", false);
        Anim.SetBool("walk", true);
        Anim.SetBool("attack", false);      
    }

    void Update()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player"); // Bruh
        float distance = Mathf.Infinity;
        foreach (var play in players)
        {
            float newdist = Vector3.Distance(Agent.transform.position, play.transform.position);
            if (newdist <= distance)
            {
                Player = play;
                distance = newdist;
            }

        }

        if (attackDelay > 0 )
        {
            attackDelay -= Time.deltaTime;
        }
        
        if (Vector3.Distance(Agent.transform.position, Player.transform.position) <= aggroDistance)
        {
            if (Vector3.Distance(Agent.transform.position, Player.transform.position) <= attackDistance)
            {
                attack = true;
                walk = false;
                run = false;
                Agent.ResetPath();
                Vector3 lookAtPos = Player.transform.position;
                lookAtPos.y = transform.position.y;
                transform.LookAt(lookAtPos);
                if (attackDelay <= 0)
                {
                    attackDelay = attackSpeed;
                    
                    Attack();
                }               
            }
            else
            {
                attack = false;
                walk = false;
                run = true;
                Agent.ResetPath();
                Run();
            }
        }
        else
        {
            attack = false;
            walk = true;
            run = false;
            Walk();
        }
        Anim.SetBool("run", run);
        Anim.SetBool("walk", walk);
        Anim.SetBool("attack", attack);
    }

    #endregion

    #region Private Methods

    private void Walk()
    {
        if (Agent.remainingDistance <= Agent.stoppingDistance && !Agent.pathPending)
        {
            //Debug.Log("Walk");
            Random aleatoire = new Random();

            int xo = aleatoire.Next(1, patrolDistance);
            int zo = aleatoire.Next(1, patrolDistance);

            int nb2 = aleatoire.Next(0, 2);
            int nb1 = aleatoire.Next(0, 2);

            if (nb1 == 0)
            {
                nb1 = -1;
            }

            if (nb2 == 0)
            {
                nb2 = -1;
            }

            Vector3 destination;
            destination.x = InitialPosition.x + xo * nb1;
            destination.z = InitialPosition.z + zo * nb2;
            destination.y = InitialPosition.y;

            Vector3 lookAtPos = destination;
            lookAtPos.y = transform.position.y;

            transform.LookAt(lookAtPos);

            NavMeshHit navhit;
            NavMesh.SamplePosition(destination, out navhit, 20f, NavMesh.AllAreas);
            Agent.SetDestination(navhit.position);
        }

    }

    private void Run()
    {
        //Debug.Log("Run");
        Vector3 lookAtPos = Player.transform.position;
        lookAtPos.y = transform.position.y;
        transform.LookAt(lookAtPos);      
        NavMeshHit navhit;
        NavMesh.SamplePosition(Player.transform.position, out navhit, 20f, NavMesh.AllAreas);
        Agent.SetDestination(navhit.position);
    }

    private void Attack()
    {       
        //Debug.Log("Attack");
        Player.GetComponent<Player>().ApplyDamage( "Orc" , this.damage, WeaponName.ORC) ;       
    }

    #endregion

}

