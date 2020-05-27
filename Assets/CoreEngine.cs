using Photon.Pun;
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
    public float wincond;
    public float redscore;
    public float bluescore;
    public GameObject Redwin;
    public GameObject BlueWin;

    // The Loading Clock
    public float loadingClock;

    // The Running Clock
    public float runningClock;

    #endregion

    #region MonoBehaviour Callbacks

    void Start()
    {
        redscore = 0;
        bluescore = 0;
        wincond = 15;
        BlueWin.SetActive(false);
        Redwin.SetActive(false);
        gameState = GameState.PLAYGROUND;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameState == GameState.LOADING)
        {
            BlueWin.SetActive(false);
            Redwin.SetActive(false);
            //Finished loading
            if (loadingClock <= 0f)
            {
                redscore = 0;
                bluescore = 0;
                wincond = 15;
                gameState = GameState.RUNNING;
                teleportPlayersToSpawn();
                // Set the Game clock to 20 minutes
                runningClock = 1800f;
            }
            else
                loadingClock -= Time.deltaTime;
        }

        if (gameState == GameState.RUNNING)
        {
            //Finished loading
            if (runningClock <= 0f)
            {
                if (bluescore>redscore)
                {
                    BlueWin.SetActive(true);
                    
                }
                else
                {
                    Redwin.SetActive(true);
                }
                gameState = GameState.STOPPED;
            }
            else
            {
                runningClock -= Time.deltaTime;
                if (bluescore>=wincond)
                {
                    BlueWin.SetActive(true);
                    gameState = GameState.STOPPED;
                }

                if (redscore>=wincond)
                {
                    Redwin.SetActive(true);
                    gameState = GameState.STOPPED;
                }
            }
                
            
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

