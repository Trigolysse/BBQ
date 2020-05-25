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
    public int patrolDistance;

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
                voyage = true;
                
               
                Pathing();
                
                
            }
            Anim.SetBool("VOYAGE", voyage);
        
        

    }

    void  OnTriggerEnter ( Collider other  ){
      

    }

    void  OnTriggerStay ( Collider other  )
    {
        time += Time.deltaTime;
        
        if(other.CompareTag("Player"))
        {
            playerSighted=true;
            if (time>1)
            {
                time = 0;
                Agent.ResetPath();
            }
            else
            {
                Debug.Log("TROUVER");
                Vector3 lookAtPos = other.transform.position;
    
                lookAtPos.y= transform.position.y;
                transform.LookAt(lookAtPos);
                if (Vector3.Distance(transform.position,other.transform.position)>=MinDist)
                {
                    transform.position += transform.forward * MoveSpeed * Time.deltaTime;
                    if (Vector3.Distance(transform.position, player.transform.position) <= MaxDist)
                    {
                        EnemyShooting = true;
                        Anim.SetBool("MINDISTANCE", true);
                        Anim.SetBool("VU", false);
                        dragon.Play();
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