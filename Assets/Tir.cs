using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class Tir : MonoBehaviour
{
    private Ray ray;

    private RaycastHit hit;

    public GameObject ball;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            Vector2 Centre=new Vector2(Screen.width/2, Screen.height/2);
            ray = Camera.main.ScreenPointToRay(Centre);
            //Debug.Log("Feu");
            
            if (Physics.Raycast(ray,out hit, Camera.main.farClipPlane))
            {
                //Debug.Log("tir dans l anus!!!" + hit.transform.name);
                //hit.rigidbody.AddForceAtPosition(transform.TransformDirection(Vector3.up)*100000,hit.normal);
            }

            if (hit.transform.gameObject.tag == "Planet")
            {
                Debug.Log("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");
                GameObject Go=Instantiate(ball,hit.point,Quaternion.FromToRotation(Vector3.forward, hit.normal)) as GameObject;
                Destroy(Go,10f);
            }
        }

       
    }
}
