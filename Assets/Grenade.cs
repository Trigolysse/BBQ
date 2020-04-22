using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviourPunCallbacks
{
    public float delay = 3f;
    public GameObject explosionEffect;
    public GameObject grenadeparent;
    public float radius;
    public float force = 700f;
    public float throwForce = 30f;

    private bool hasExploded = false;
    private Rigidbody rg;
    float countdown;
    // Start is called before the first frame update
    void Start()
    {
        countdown = delay;
        rg = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {   
        countdown -= Time.deltaTime;
        if (countdown <= 0f && !hasExploded)
        {
            Explode();
            hasExploded = true;
        }
       
    }

    public void Throw(Camera mainCamera)
    {
        GetComponent<Rigidbody>().velocity = mainCamera.transform.forward * throwForce;
        transform.LookAt(transform.position + GetComponent<Rigidbody>().velocity);
    }

    void Explode()
    {
        //Show effect
        Instantiate(explosionEffect, transform.position, transform.rotation);

        // Get nearby objects 
        Collider[] colliderstoDestroy =Physics.OverlapSphere(transform.position, radius);
        foreach (Collider NearbyObject in colliderstoDestroy)
        {
            
            //Add damamge
            Destructible dest = NearbyObject.GetComponent<Destructible>();
            if (dest != null)
            {
                dest.Destroy();
            }
        }
        //Add force
        Collider[] colliderstoMove =Physics.OverlapSphere(transform.position, radius);
        foreach (Collider NearbyObject in colliderstoMove)
        {
            Rigidbody rb = NearbyObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(force, transform.position, radius);
            }
            if(NearbyObject.gameObject.CompareTag(Tags.PLAYER_TAG))
            {
                NearbyObject.gameObject.GetComponent<PhotonView>().RPC("ApplyDamage", RpcTarget.All, "Grenade", 100, NearbyObject.gameObject.name);
            }
        }



        //Remove Grenade 
        Destroy(grenadeparent);
    }

    
}
