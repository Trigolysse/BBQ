using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Broadcast : MonoBehaviour
{
    string message;
    [SerializeField]
    private Text text;
    public float delay = 3f;
    float countdown;


    // Start is called before the first frame update
    void Start()
    {
        countdown = delay;
    }

    // Update is called once per frame
    void Update()
    {
        countdown -= Time.deltaTime;
        if (countdown <= 0f)
        {
            GameObject.Destroy(this.gameObject);
        }

        // Destroy itself if the target(Player) is null, It's a fail safe when Photon is destroying Instances of a Player over the network
        if (message == null)
        {
            Destroy(this.gameObject);
            return;
        }

        // Reflect the Broadcast text message
        if (text != null)
        {
            text.text = message;
        }
    }

    public void SetMessage(String _message)
    {
        if (_message == null)
        {
            Debug.LogError("<color=red><a>Missing</a></Color> ChatManager target for PlayerUI.SetMessage.", this);
            return;
        }
        // Cache references for efficiency
        message = _message;
    }
}
