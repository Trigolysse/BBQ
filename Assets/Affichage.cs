using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Affichage : MonoBehaviour
{
    private bool Mm;
    // Start is called before the first frame update
    void Start()
    {
        Mm = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (Mm)
            {
                GameObject.Find("minimapcamera").GetComponent<Camera>().enabled = true;
                Mm = false;
            }
            else
            {
                GameObject.Find("minimapcamera").GetComponent<Camera>().enabled = false;
                Mm = true;
            }

        }
    }
}
