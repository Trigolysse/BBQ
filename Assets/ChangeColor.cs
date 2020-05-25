using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColor : MonoBehaviour
{
    private Renderer corps;

    public GameObject o;
    // Start is called before the first frame update
    void Start()
    {
        corps= o.GetComponent<Renderer>();
        corps.material.color=Color.red;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
