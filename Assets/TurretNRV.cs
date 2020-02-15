using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UIElements;

public class TurretNRV : MonoBehaviour
{
    private Transform player;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }


    // Update is called once per frame
    void Update()
    {
        float distance = Vector2.Distance(player.position, transform.position);
        if (distance < 40)
        {
            Vector2 v2 = player.position - transform.position;
            v2.Normalize();
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(v2), 1f * Time.deltaTime);
        }
    }
}
