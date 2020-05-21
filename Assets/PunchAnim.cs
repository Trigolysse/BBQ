using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchAnim : MonoBehaviour
{
    public GameObject Punch;

    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = Punch.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            anim.SetBool("Parade",false);
            anim.SetBool("Punch",true);
        }
        else
        {
            if (Input.GetMouseButton(1))
            {
                anim.SetBool("Punch",false);
                anim.SetBool("Parade",true);
            }
        }
        
    }
}
