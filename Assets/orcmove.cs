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
    public float Attackdistance = 10;
    public float Aggrodistance = 20;
    public int Patroldistance = 10;
    public float attackspeed;
    private float attackdelay;
    private Animator Anim;
    





    // Start is called before the first frame update
    void Start()
    {
        
        Agent = GetComponent<NavMeshAgent>();
        Anim = Agent.GetComponent<Animator>();
        InitialPosition = Agent.transform.position;
        Walk();
        Player = GameObject.FindGameObjectWithTag("Player");
        Anim.SetBool("run", false);
        Anim.SetBool("walk", true);
        Anim.SetBool("attack", false);
        attackdelay = 0;
    }

    // Update is called once per frame
    void Update()
    {

        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
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


        if (attackdelay > 0 )
        {
            attackdelay -= Time.deltaTime;
        }
        
        if (Vector3.Distance(Agent.transform.position, Player.transform.position) <= Aggrodistance)
        {
            if (Vector3.Distance(Agent.transform.position, Player.transform.position) <= Attackdistance)
            {
                attack = true;
                walk = false;
                run = false;
                Agent.ResetPath();
                Vector3 lookAtPos = Player.transform.position;
                lookAtPos.y = transform.position.y;
                transform.LookAt(lookAtPos);
                if (attackdelay <= 0)
                {
                    attackdelay = attackspeed;
                    
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



    void Walk()
    {
        if (Agent.remainingDistance <= Agent.stoppingDistance && !Agent.pathPending)
        {
            Debug.Log("Walk");
            Random aleatoire = new Random();


            int xo = aleatoire.Next(1, Patroldistance);
            int zo = aleatoire.Next(1, Patroldistance);


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

    void Run()
    {
        Vector3 lookAtPos = Player.transform.position;
        lookAtPos.y = transform.position.y;

        transform.LookAt(lookAtPos);
        Debug.Log("Run");
        NavMeshHit navhit;
        NavMesh.SamplePosition(Player.transform.position, out navhit, 20f, NavMesh.AllAreas);
        Agent.SetDestination(navhit.position);
    }

    void Attack()
    {
        
        Debug.Log("Attak");
        Player.GetComponent<Combatmanager>().TakeDamage(20);
        
    }

}

