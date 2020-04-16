using System;
using UnityEngine;
using System.Collections;
using UnityEngine.AI;
using Random = System.Random;

public class DragonFOLLOW : MonoBehaviour {

    static bool  EnemyShooting;
    private int frames;
    private Transform Player;
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

    void  Awake ()
    {
        playerSighted=false;
        EnemyShooting=false;
    }

    private void Start()


    {
        voyage = false;
        Agent = GetComponent<NavMeshAgent>();
        InitialPosition = Agent.transform.position;
        destinatione = Agent.transform.position;
        Anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        Player = player.transform;
        Anim.SetBool("VU", false);
        Anim.SetBool("MINDISTANCE", false);
        Anim.SetBool("VOYAGE", false);
        


    }

    void  Update (){
        
            if (playerSighted==true)
            {
                voyage = false;
                PlayerFound();

            }
            else
            {
                
               
                Pathing();
                
                
            }
            Anim.SetBool("VOYAGE", voyage);
        
        

    }

    void  OnTriggerEnter ( Collider other  ){
        if (other.transform==Player)
        {
            //GetComponent.<AudioSource>.Play();

        }

    }

    void  OnTriggerStay ( Collider other  ){
        if(other.transform==Player)
        {
            playerSighted=true;
            Anim.SetBool("VU", true);
            Debug.Log("VU");
        }

    }

    void  OnTriggerExit ( Collider other  ){
        if(other.transform==Player)
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
        Vector3 lookAtPos = Player.position;
    
        lookAtPos.y= transform.position.y;
        transform.LookAt(lookAtPos);
        if (Vector3.Distance(transform.position,Player.position)>=MinDist)
        {
            transform.position += transform.forward * MoveSpeed * Time.deltaTime;
            if (Vector3.Distance(transform.position, Player.position) <= MaxDist)
            {
                EnemyShooting = true;
                Anim.SetBool("MINDISTANCE", true);
            }
            else
            {
                Anim.SetBool("MINDISTANCE", false);
            }

        }


    }

    void Pathing()
    {
        
        if (transform.position.x==destinatione.x && transform.position.z==destinatione.z)
        {
            Debug.Log("Camembert");
            voyage = true;
            
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

            float x1 = destination.x;
            float y1 = destination.y;
            float z1 = destination.z;
            destinatione.x = x1;
            destinatione.y = y1;
            destinatione.z = z1;
            
            
        }
        else
        {
            
            Debug.Log(transform.position + " " + destinatione);
            voyage = true;
            //Debug.Log("J aime la pain du diamnche");
            Agent.SetDestination(destinatione);
        }
        
        
       
    }

    
}