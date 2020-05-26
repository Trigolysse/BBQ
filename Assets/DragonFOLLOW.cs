using System;
using UnityEngine;
using System.Collections;
using UnityEngine.AI;
using Random = System.Random;

public class DragonFOLLOW : MonoBehaviour {

    static bool  EnemyShooting;
    private int frames;
    private GameObject player;
    public float MoveSpeed= 30f;
    public float MaxDist= 20f;
    public float MinDist=2f;
    public GameObject Ennemi;
    bool  playerSighted;
    private NavMeshAgent Agent;
    private Animator Anim;
    public GameObject pointdecontrole1;
    public GameObject pointdecontrole2;
    public GameObject pointdecontrole3;
    public GameObject pointdecontrole4;
    private bool voyage;
    private Vector3 InitialPosition;
    private Vector3 destinatione;
    public AudioSource dragon;
    private bool first;
    private float time;
    private GameObject Player;
    private float attackDelay = 0;




    #region Public Fields

    [Tooltip("Distance from a player at which the Orc will start to inflict damage")]
    [SerializeField]
    private float attackDistance = 10;

    [Tooltip("Distance from a player at which the Orc will stop its normal behavior and engage the player in combat")]
    [SerializeField]
    private float aggroDistance = 20;

    [Tooltip("?? Ulysse write something here")]
    [SerializeField]
    private int patrolDistance = 20;

    [Tooltip("Frequency of a unit's basic attacks")]
    [SerializeField]
    private float attackSpeed;
    
    [Tooltip("Inflicting Damage of the Orc")]
    [SerializeField]
    private int damage = 20;

    #endregion
    
    
    
    
    

    void  Awake ()
    {
        playerSighted=false;
        EnemyShooting=false;
        first = false;
        time = 0;
    }

    private void Start()


    {
        voyage = false;
        Agent = GetComponent<NavMeshAgent>();
        InitialPosition = Agent.transform.position;
        destinatione = Agent.transform.position;
        Anim = GetComponent<Animator>();
        //player = GameObject.FindGameObjectWithTag("Player");
        //Player = player.transform;
        Anim.SetBool("VU", false);
        Anim.SetBool("MINDISTANCE", false);
        Anim.SetBool("VOYAGE", false);
        


    }

    void  Update (){
        
            if (playerSighted==true)
            {
                voyage = false;
            }
            else
            {
                dragon.Stop();
                voyage = true;
                
               
                Pathing();
                
                
            }
            Anim.SetBool("VOYAGE", voyage);
        
        

    }

    void  OnTriggerEnter ( Collider other  ){
      

    }

    void  OnTriggerStay ( Collider other  )
    {
    
        
        
        
        
        if(other.CompareTag("Player"))
        {
            
            time += Time.deltaTime;
        
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
            //playerpos = Player.transform.position;

            if (attackDelay > 0 )
            {
                attackDelay -= Time.deltaTime;
            }




            playerSighted=true;
            if (time>1)
            {
                time = 0;
                Agent.ResetPath();
            }
            else
            {
                Debug.Log("TROUVER");
                Vector3 lookAtPos = Player.transform.position;
    
                lookAtPos.y= transform.position.y;
                transform.LookAt(lookAtPos);
                if (Vector3.Distance(transform.position,Player.transform.position)>=MinDist)
                {
                    transform.position += transform.forward * MoveSpeed * Time.deltaTime;
                    if (Vector3.Distance(transform.position, Player.transform.position) <= MaxDist)
                    {
                        EnemyShooting = true;
                        Anim.SetBool("MINDISTANCE", true);
                        Anim.SetBool("VU", false);
                        if (attackDelay <= 0)
                        {
                            attackDelay = attackSpeed;
                            dragon.Play();
                            Player.GetComponent<Player>().ApplyDamage( "Orc" , this.damage, WeaponName.DRAKE) ; 
                        }
                        
                    }
                    else
                    {
                        if (Vector3.Distance(transform.position, Player.transform.position) <= MaxDist)
                        {
                            EnemyShooting = true;
                            Anim.SetBool("MINDISTANCE", true);
                            Anim.SetBool("VU", false);
                            if (attackDelay <= 0)
                            {
                                attackDelay = attackSpeed;
                                dragon.Play();
                                Player.GetComponent<Player>().ApplyDamage( "Orc" , this.damage, WeaponName.DRAKE) ; 
                            }
                        
                        }
                        else
                        {
                            Anim.SetBool("MINDISTANCE", false);
                            Anim.SetBool("VU", true);
                            dragon.Stop();
                        }
                        
                    }

                }
            
            }
        }

    }

    void  OnTriggerExit ( Collider other  ){
        if(other.CompareTag("Player"))
        {
            
            playerSighted=false;
            Anim.SetBool("VU", false);
            EnemyShooting=false;
            Debug.Log("OU ES TU?");
            
        }

    }

    void  PlayerFound ()
    {
        Debug.Log("TROUVER");
        Vector3 lookAtPos = player.transform.position;
    
        lookAtPos.y= transform.position.y;
        transform.LookAt(lookAtPos);
        if (Vector3.Distance(transform.position,player.transform.position)>=MinDist)
        {
            transform.position += transform.forward * MoveSpeed * Time.deltaTime;
            if (Vector3.Distance(transform.position, player.transform.position) <= MaxDist)
            {
                EnemyShooting = true;
                Anim.SetBool("MINDISTANCE", true);
                dragon.Play();
            }
            else
            {
                Anim.SetBool("MINDISTANCE", false);
                dragon.Stop();
            }

        }


    }

    void Pathing()
    {
        voyage = true;
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

    
}