using System;
using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class EnemyAttack : MonoBehaviour {

static bool  EnemyShooting;
private Transform Player;
private GameObject player;
public float MoveSpeed= 4f;
public float MaxDist= 10f;
public float MinDist=5f;
public GameObject Ennemi;
bool  playerSighted;
private NavMeshAgent Agent;

void  Awake (){
    playerSighted=false;
    EnemyShooting=false;
}

private void Start()
{
    player = GameObject.FindGameObjectWithTag("Player");
    Player = player.transform;
    Agent = GetComponent<NavMeshAgent>();
    
}

void  Update (){
    if (playerSighted==true)
    {
        PlayerFound();

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
    }

}

void  OnTriggerExit ( Collider other  ){
    if(other.transform==Player)
    {
        playerSighted=false;
        EnemyShooting=false;
    }

}

void  PlayerFound ()
{
    Debug.Log("TROUVER");
    Vector3 lookAtPos = Player.position;
    
    lookAtPos.x= transform.position.x;
    transform.LookAt(lookAtPos);
    if (Vector3.Distance(transform.position,Player.position)>=MinDist)
    {
        EnemyShooting=true;
        

    }


}
}