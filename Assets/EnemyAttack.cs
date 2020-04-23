using System;
using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class EnemyAttack : MonoBehaviour {

private Transform Player;
private GameObject player;
public float MoveSpeed= 30f;
public float MaxDist= 20f;
public float MinDist=2f;
public GameObject Ennemi;
bool  playerSighted;
private NavMeshAgent Agent;

void  Awake() {
    playerSighted = false;
}

private void Start()
{
    player = GameObject.FindGameObjectWithTag("Player");
    Player = player.transform;
    
    
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
        Debug.Log("VU");
    }

}

void  OnTriggerExit ( Collider other  ){
    if(other.transform==Player)
    {
        playerSighted=false;
       // EnemyShooting=false;
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
            //EnemyShooting=true;   
        }

    }


}
}