using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityScript.Steps;

public class Grenade : MonoBehaviour
{
    public float delay = 3f;
    public GameObject explosionEffect;
    public GameObject grenadeparent;
    public float radius;
    public float force = 70000f;

    private bool hasExploded = false;

    float countdown;
    // Start is called before the first frame update
    void Start()
    {
        countdown = delay;
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
        foreach (Collider nearbyObject in colliderstoMove)
        {
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(force, transform.position, radius);
            }
        }
        //Damage

        //Remove Grenade 
        Destroy(grenadeparent);
    }

    
}
