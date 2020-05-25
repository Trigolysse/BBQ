using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreEngine : MonoBehaviourPunCallbacks
{

    [SerializeField]
    private GameObject enginePrefab;


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

