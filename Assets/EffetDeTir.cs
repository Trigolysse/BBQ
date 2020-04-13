using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffetDeTir : MonoBehaviour
{
    public GameObject Boomeffet;
    
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject tireffect= Instantiate(Boomeffet, transform) as GameObject;
            Destroy(tireffect,1f);
        }
    }
}
