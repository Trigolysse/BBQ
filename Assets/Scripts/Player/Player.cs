using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviourPunCallbacks
{
    public HealthBar healthBar;
    private RaycastHit hit;
    public int currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = 100;
    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine)
        {
           
        }

    }


}
