using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class loot : MonoBehaviour
{
    public Vector3 deadzone;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        transform.position = deadzone;
    }
}
