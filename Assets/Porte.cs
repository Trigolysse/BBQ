using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class Porte : MonoBehaviour
{
    
    public float angle=150;
    public float angle2=150;
    public GameObject portedroite;
    public GameObject portegauche;
    private bool droit = false;
    private bool ouvert = false;
    public int team;
    

    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
   

    private void OnTriggerEnter(Collider other)
    {
        //if (other.transform==Player)
        {
            droit = true;

        }
        
    }
    
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (Input.GetKeyUp(KeyCode.E) && other.gameObject.GetComponent<SimpleInv>().Bigitem[0])
            {
                if (team==0&&other.gameObject.GetComponent<Player>().team==Teams.RED&&!ouvert)
                {
                    ouvert = true;
                    PhotonNetwork.Destroy(portedroite);
                    PhotonNetwork.Destroy(portegauche);

                }
                if (team==1&&other.gameObject.GetComponent<Player>().team==Teams.BLUE&&!ouvert)
                {
                    ouvert = true;
                    PhotonNetwork.Destroy(portedroite);
                    PhotonNetwork.Destroy(portegauche);

                }

                
            }
            

        }
        
    }
        
        
    

}
