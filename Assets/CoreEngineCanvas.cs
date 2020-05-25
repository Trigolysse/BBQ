using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoreEngineCanvas : MonoBehaviour
{

    [SerializeField]
    private Text engineText;

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
                engineText.text = $"<color=white>Game starting in {engine.loadingClock.ToString("n2")}s </color>";

            else if (engine.gameState == GameState.RUNNING)
                engineText.text = $"<color=#ffae1a>Game LIVE</color>";
            else
                engineText.text = "";
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
}
