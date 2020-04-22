using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillFeed : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.onPlayerKilledCallback += OnKill;

    }

    public void OnKill(string killer, string victim)
    {
        Debug.Log(killer + " killed " + victim);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
