using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using Random = System.Random;
using UnityEngine.AI;

public class orcmove : MonoBehaviour
{
    private bool walk = true;
    private bool attack = false;
    private bool run = false;
    private NavMeshAgent Agent;
    private Vector3 InitialPosition;
    private GameObject Player;
    public float MinDistance = 10;
    public float radius = 15;
    public float MoveSpeed=10f;
    public float MaxDistance = 30f;





    // Start is called before the first frame update
    void Start()
    {
        Agent = GetComponent<NavMeshAgent>();
        InitialPosition = Agent.transform.position;
        Walk();
        Player = GameObject.FindGameObjectWithTag("Player");

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(walk);
        if (walk)
        {
           Walk(); 
        }
        else
        {
            Debug.Log("TROUVER");
            Vector3 lookAtPos = Player.transform.position;
    
            lookAtPos.y= transform.position.y;
            transform.LookAt(lookAtPos);
            if (Vector3.Distance(transform.position, Player.transform.position) >= MinDistance)
            {
                transform.position += transform.forward * MoveSpeed * Time.deltaTime;
                if (Vector3.Distance(transform.position, Player.transform.position) <= MaxDistance)
                {
                    attack = true;
                    walk = false;
                    run = false;
                }
                else
                {
                    attack = false;
                    walk = false;
                    run = true;


                }
            }

        }




    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag=="Player")
        {
             run = true;
             walk = false;
             attack = false;
        }
        
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag=="Player")
        {
             walk = true;
             attack = false;
             run = false;

             Walk();
        }
        

    }
    private void OnTriggerStay(Collider other)
    {
        if (other.transform == Player)
        {
            if (Vector3.Distance(transform.position,Player.transform.position)>=MinDistance)
            {
                transform.position += transform.forward * MoveSpeed * Time.deltaTime;
                if (Vector3.Distance(transform.position, Player.transform.position) <= MaxDistance)
                {
                    attack = true;
                    walk = false;
                    run = false;
                }
                else
                {
                    attack = false;
                    walk = false;
                    run = true;
                    
                    
                }

            }

        }

    }




    void Walk()
    {
        if (Agent.remainingDistance <= Agent.stoppingDistance && !Agent.pathPending)
        {
            Random aleatoire = new Random();

            
            int xo = aleatoire.Next( 1,40);
            int zo = aleatoire.Next(1,40);
            
            
            int nb2 = aleatoire.Next(0, 2);
            int nb1 = aleatoire.Next(0, 2);
            if (nb1==0)
            {
                nb1 = -1;
            }
            if (nb2==0)
            {
                nb2 = -1;
            }



            Vector3 destination;
            destination.x = InitialPosition.x + xo * nb1;
            destination.z = InitialPosition.z + zo * nb2;
            destination.y = InitialPosition.y;
            

            Vector3 lookAtPos = destination;
            lookAtPos.y= transform.position.y;
            
            transform.LookAt(lookAtPos);

        
            NavMeshHit navhit;
            NavMesh.SamplePosition(destination, out navhit,20f, NavMesh.AllAreas);
            Agent.SetDestination(navhit.position);
        }
        
    }

    void Run()
    {
        Debug.Log("Run");
        NavMeshHit navhit;
        NavMesh.SamplePosition(Player.transform.position, out navhit,20f, NavMesh.AllAreas);
        Agent.SetDestination(navhit.position);
    }

    void Attack()
    {
        Debug.Log("Attak");
        NavMeshHit navhit;
        NavMesh.SamplePosition(Player.transform.position, out navhit,20f, NavMesh.AllAreas);
        Agent.SetDestination(navhit.position);

    }
    
}
