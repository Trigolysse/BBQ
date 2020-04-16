using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PORTESHOP : MonoBehaviour
{
    public float angle=150;
    public float angle2=150;
    public GameObject portedroite;
    
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
                    portedroite.transform.Rotate(0,0,-angle);
                    Debug.Log("OUVERT");
                    ouvert = false;
                }
                else
                {
                    
                    portedroite.transform.Rotate(0,0,angle2);
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
