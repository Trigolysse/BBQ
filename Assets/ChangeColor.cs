using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColor : MonoBehaviour
{
    private Renderer corps;
    public GameObject Player;

    public GameObject o;
    // Start is called before the first frame update
    void Start()
    {
        corps= o.GetComponent<Renderer>();
        if (Player.GetComponent<Player>().team==Teams.RED)
        {
            corps.material.color=Color.red;
        }
        else
        {
            corps.material.color = Color.blue;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Player.GetComponent<Player>().team==Teams.RED)
        {
            corps.material.color=Color.red;
        }
        else
        {
            corps.material.color = Color.blue;
        }
    
    }
}
