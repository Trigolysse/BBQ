using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Porte : MonoBehaviour
{
    
    public float angle=150;
    public float angle2=150;
    public GameObject portedroite;
    public GameObject portegauche;
    private bool droit = false;
    private bool ouvert = false;
    

    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (droit)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (ouvert)
                {
                    portegauche.transform.Rotate(0,angle,0);
                    portedroite.transform.Rotate(0,-angle,0);
                    Debug.Log("OUVERT");
                    ouvert = false;
                }
                else
                {
                    portegauche.transform.Rotate(0,-angle2,0);
                    portedroite.transform.Rotate(0,angle2,0);
                    Debug.Log("FERME");
                    ouvert = true;

                }
                
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //if (other.transform==Player)
        {
            droit = true;

        }
        
    }
    
    private void OnTriggerStay(Collider other)
    {
        //if (other.transform==Player)
        {
            droit = true;

        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        //if (other.transform==Player)
        {
            droit = false;

        }
        
    }
}
