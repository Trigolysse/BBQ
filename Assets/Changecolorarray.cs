using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Changecolorarray : MonoBehaviour
{
  
    private Renderer corps;
    public GameObject Player;
    public Material[] lemario;

    public GameObject o;
    // Start is called before the first frame update
    void Start()
    {
        corps= o.GetComponent<Renderer>();
        lemario = corps.GetComponents<Material>();
        if (Player.GetComponent<Player>().team==Teams.RED)
        {
            lemario[0].color=Color.red;
        }
        else
        {
            lemario[0].color = Color.blue;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
