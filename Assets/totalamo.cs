using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class totalamo : MonoBehaviour
{
    public GameObject AK;
    private int amo;
    public Text totalAmo;
    

    // Update is called once per frame
    void Update()
    {
        amo = AK.GetComponent<WeaponHandler>().amo;
        totalAmo.text = amo.ToString();
    }
}
