using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Canvas))]
public class EscapeMenu : MonoBehaviour
{

    public bool isShowing;

    // Start is called before the first frame update
    void Start()
    {
        isShowing = false;
        GetComponent<Canvas>().enabled = isShowing;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isShowing = !isShowing;
            GetComponent<Canvas>().enabled = isShowing;
        }
    }
}
