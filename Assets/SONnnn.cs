using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SONnnn : MonoBehaviour
{
    public AudioSource son;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        son.Play();
        
    }

    private void OnTriggerExit(Collider other)
    {
        son.Stop();
        Debug.Log("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");
    }
}
