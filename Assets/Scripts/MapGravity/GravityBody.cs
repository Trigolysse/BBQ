using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Rigidbody))]
public class GravityBody : MonoBehaviour {
	
    GravityAttractor planet;
    Rigidbody rigidbody;
    private CharacterController characterController;

    void Awake () {
        planet = GameObject.FindGameObjectWithTag("Planet").GetComponent<GravityAttractor>();
        rigidbody = GetComponent<Rigidbody> ();
        characterController = GetComponent<CharacterController>();

        rigidbody.useGravity = false;
        rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
    }
	
    void FixedUpdate () {
        //planet.Attract(rigidbody);
    }
}