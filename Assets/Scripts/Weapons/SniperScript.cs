using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperScript : MonoBehaviour
{

    public GameObject playerCam;
    public GameObject SniperScope;


    private void Start()
    {
        SniperScope.SetActive(false);
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            playerCam.GetComponent<Camera>().fieldOfView = 16.6f;
            SniperScope.SetActive(true);
        }
        if (Input.GetMouseButtonUp(1))
        {
            playerCam.GetComponent<Camera>().fieldOfView = 68;
            SniperScope.SetActive(false);
        }

    }
}