using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreEngine : MonoBehaviourPunCallbacks
{
    #region Fields

    [SerializeField]
    private GameObject enginePrefab;

    // Cuurent Game State
    public GameState gameState;

    // The Loading Clock
    public float loadingClock;

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
            }
            else
                loadingClock -= Time.deltaTime;
        }

        if (gameState == GameState.RUNNING)
        {

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
                player.transform.position = new Vector3(-191.096f, 20, -68.05299f);
            }
            else
            {
                // Teleport BLUE Team
                player.transform.position = new Vector3(406.4642f, 6, 536.1269f);
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

