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
    private bool voyage = false;

    void  Awake ()
    {
        playerSighted=false;
        EnemyShooting=false;
    }

    private void Start()


    {
        Anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        Player = player.transform;
        Anim.SetBool("VU", false);
        Anim.SetBool("MINDISTANCE", false);
    
    
    }

    void  Update (){
        
            if (playerSighted==true)
            {
                voyage = false;
                PlayerFound();

            }
            else
            {
                Voyage();
                if (voyage)
                {
                    Pathing();
                }
                
            }
        
        

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
        if (!voyage)
        {
            voyage = true;
            GameObject destination;
            Random aleatoire = new Random();
            int nb = aleatoire.Next(1, 5);
            if (nb==1)
            {
                destination = pointdecontrole1;
            }
            else if (nb==2)
            {
                destination = pointdecontrole2;
            }
            else if (nb==3)
            {
                destination = pointdecontrole3;
            }
            else
            {
                destination = pointdecontrole4;
            }
            Vector3 lookAtPos = destination.transform.position;
            lookAtPos.y= transform.position.y;
            transform.LookAt(lookAtPos);
            while (voyage)
            {
                transform.position += transform.forward * MoveSpeed * Time.deltaTime;
            }
            
            
        }
    }

    void Voyage()
    {
        if (transform.position==pointdecontrole1.transform.position)
        {
            voyage = false;
        }
        else if (transform.position==pointdecontrole2.transform.position)
        {
            voyage = false;
        }
        else if (transform.position==pointdecontrole3.transform.position)
        {
            voyage = false;
        }
        else if (transform.position==pointdecontrole4.transform.position)
        {
            voyage = false;
        }
        else
        {
            voyage = true;
        }
    }
}