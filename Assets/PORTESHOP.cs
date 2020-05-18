using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PORTESHOP : MonoBehaviour
{
    
    public GameObject porteouverte;
    public GameObject porteferme;
    
    private bool droit = false;
    private bool ouvert = false;
    

    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        
            if (Input.GetKeyDown(KeyCode.E) && droit)
            {
                porteferme.SetActive(true);
                porteouverte.SetActive(false);

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
