﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KillFeed : MonoBehaviour
{
    [SerializeField]
    GameObject killFeedItemPrefab;

    void Start()
    {
        GameManager.Instance.onPlayerKilledCallback += OnKill;
    }

    public void OnKill(string killer, string victim)
    {
        GameObject go = Instantiate(killFeedItemPrefab, this.transform) as GameObject;
        go.GetComponent<KillFeedItem>().Setup(killer, victim);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

