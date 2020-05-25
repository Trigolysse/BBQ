using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreEngine : MonoBehaviourPunCallbacks
{

    [SerializeField]
    private GameObject enginePrefab;

    private GameObject[] ListPlayer;
    private bool begin=false;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [PunRPC]
    public void StartGameEngine(int startTime)
    {
        if (enginePrefab != null)
        {
            EngineUI engine = new EngineUI((float) startTime, GameState.LOADING, true);
            GameObject _uiGo = Instantiate(enginePrefab);
            _uiGo.SendMessage("SetEngine", engine, SendMessageOptions.RequireReceiver);
        }
    }

    public void teleportPlayersToSpawn()
    {
        if (0<1 && !begin)
        {
            begin = true;
            ListPlayer =GameObject.FindGameObjectsWithTag("Player");
            foreach (GameObject player in ListPlayer)
            {
                if (player.GetComponent<Player>().team==Teams.RED)
                {
                    player.transform.position= new Vector3(-191.096f,20,-68.05299f);
                }
                else
                {
                    player.transform.position= new Vector3(406.4642f,6,536.1269f);
                }
            }

        }
    }
}

public class EngineUI
{
    public float startTime;
    public bool timer = false;
    public GameState gameState;

    public EngineUI(float startTime, GameState gameState, bool timer = false)
    {
        this.timer = timer;
        this.startTime = startTime;
        this.gameState = gameState;
    }
}

public enum GameState
{
    LOADING,
    RUNNING,
    PAUSED,
    STOPPED
}

