using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoreEngineCanvas : MonoBehaviour
{

    [SerializeField]
    private Text engineText;

    [SerializeField]
    private Text gameClock;

    [SerializeField]
    private Text gameStatus;
    CoreEngine engine;
 

    // Start is called before the first frame update
    void Start()
    {
    }

    void Update()
    {
        // Destroy itself if the target(Player) is null, It's a fail safe when Photon is destroying Instances of a Player over the network
        if (engine == null)
        {
            Destroy(this.gameObject);
            return;
        }

        if (engineText != null)
        {
            if(engine.gameState == GameState.PAUSED)
            {
                engineText.text = $"<color=#ffae1a>Game paused {engine.loadingClock.ToString("n2")}s </color>";
            }
            // Reflect Loading Clock
            else if (engine.gameState == GameState.LOADING)
                engineText.text = $"<color=white>Game starting in </color><color=#ffa500><b>{PrettyTime(engine.loadingClock)}s</b></color>";
            else
                engineText.text = "";
        }

        if(gameClock != null && engine.gameState == GameState.RUNNING)
        {
            gameClock.text = $"<color=white>Time left: </color><color=#ffa500><b>{PrettyTime(engine.runningClock)}s</b></color>";
        }

        if(gameStatus != null)
        {
            gameStatus.text = $"<color=white>Status: </color><b>{PrettyStatus(engine.gameState)}</b>";
        }
        
    }

    public void SetEngine(CoreEngine engine)
    {
        if (engine == null)
        {
            Debug.LogError("<color=red><a>Missing</a></Color> ChatManager target for PlayerUI.SetMessage.", this);
            return;
        }

        this.engine = engine;
    }

    private string PrettyTime(float seconds)
    {
        int min = Mathf.FloorToInt(seconds / 60);
        int sec = Mathf.FloorToInt(seconds % 60);
        return min.ToString("00") + ":" + sec.ToString("00");
    }

    private string PrettyStatus(GameState state)
    {
       switch(state)
        {
            case GameState.LOADING:
                return "<color=blue>loading</color>";
            case GameState.PAUSED:
                return "<color=#ffa500>paused</color>";
            case GameState.RUNNING:
                return "<color=#7CFC00>live</color>";
            case GameState.STOPPED:
                return "<color=red>stopped</color>";
            default:
                return $"<color=white>{state}</color>";
        }
    }
}
