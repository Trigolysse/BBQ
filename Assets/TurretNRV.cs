using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UIElements;

public class TurretNRV : MonoBehaviour
{
    private Transform player;
    [SerializeField]
    private float maxFocusRange = 40f;
    [SerializeField]
    private float strength = 20;

    private void Start()
    {
        maxFocusRange = 40f;
        strength = 20;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        float distance = Vector2.Distance(player.position, transform.position);
        if (distance < maxFocusRange)
        {
            Quaternion targetRotation = Quaternion.LookRotation(player.position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Mathf.Min(strength * Time.deltaTime, 1));
        }
    }
}
