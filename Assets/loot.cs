using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class loot : MonoBehaviour
{
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
            PhotonNetwork.Destroy(this.gameObject);
    }
}
