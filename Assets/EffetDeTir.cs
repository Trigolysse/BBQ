using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffetDeTir : MonoBehaviour
{
    public GameObject Boomeffet;
    // Start is called before the first frame update
    

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject tireffect= Instantiate(Boomeffet, transform);
            Destroy(tireffect,1f);
        }
    }
}
