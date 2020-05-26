﻿using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CoreEngine : MonoBehaviourPunCallbacks
{
    #region Fields

    [SerializeField]
    private GameObject enginePrefab;

    // Cuurent Game State
    public GameState gameState;

    // The Loading Clock
    public float loadingClock;

    // The Running Clock
    public float runningClock;

    #endregion

    #region MonoBehaviour Callbacks

    void Start()
    {
        gameState = GameState.PLAYGROUND;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameState == GameState.LOADING)
        {
            //Finished loading
            if (loadingClock <= 0f)
            {
                gameState = GameState.RUNNING;
                teleportPlayersToSpawn();
                // Set the Game clock to 20 minutes
                runningClock = 1200f;
            }
            else
                loadingClock -= Time.deltaTime;
        }

        if (gameState == GameState.RUNNING)
        {
            //Finished loading
            if (runningClock <= 0f)
            {
                gameState = GameState.STOPPED;
            }
            else
                runningClock -= Time.deltaTime;
        }
    }

    #endregion

    [PunRPC] /* Called via chat command */
    
    public void StartNewGame(float delayBeforeGameRunning = 60f)
    {
        gameState = GameState.LOADING;

        // Set the Clock Delay [Default to 1 minute]
        this.loadingClock = delayBeforeGameRunning;

        if (enginePrefab != null)
        {
            GameObject _uiGo = Instantiate(enginePrefab);
            _uiGo.SendMessage("SetEngine", this, SendMessageOptions.RequireReceiver);
        }
    }

    public void teleportPlayersToSpawn()
    {
        GameObject[] allPlayers = GameObject.FindGameObjectsWithTag("Player");
       
        foreach (GameObject player in allPlayers)
        {
            if (player.GetComponent<Player>().team == Teams.RED)
            {
                // Teleport RED Team
                player.GetComponent<Player>().teleport = true;
            }
            else
            {
                // Teleport BLUE Team
                player.GetComponent<Player>().teleport = true;
            }
        }
    }
}


public enum GameState
{
    PLAYGROUND,
    LOADING,
    RUNNING,
    PAUSED,
    STOPPED
}

