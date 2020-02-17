﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Deplacement : MonoBehaviour
{
    private NavMeshAgent Agentenemy;

    public Transform target;

    public float Attackdistance;

    private float Distance;

    private Vector3 InitialPosition;

    public float ReactionTime;

    public float Damage;

    public float Attackspeed;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        Agentenemy = GetComponent<NavMeshAgent>();
        InitialPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Distance<Attackdistance)
        {
            Agentenemy.SetDestination(target.position);
        }
        else
        {
            Agentenemy.SetDestination(InitialPosition);
        } */
        
        Distance = Vector3.Distance(target.position, transform.position);
        Agentenemy.SetDestination(target.position);
    }
}
