using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviourPunCallbacks
{
    public HealthBar healthBar;
    private RaycastHit hit;
    public int currentHealth;

    private void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = 100;
        if (!photonView.IsMine)
            healthBar.gameObject.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine)
        {
            healthBar.SetHealth(currentHealth);
            if(currentHealth <= 0)
            {
                gameObject.transform.position = new Vector3(100, 20, 100);
                //Destroy(gameObject);
            }
        }

    }


}
