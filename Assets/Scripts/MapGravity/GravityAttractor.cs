using UnityEngine;
using System.Collections;

public class GravityAttractor : MonoBehaviour {
	
	public float gravity = -9.8f;
	
	
	public void Attract(Rigidbody body) {
		Vector3 gravityUp = (body.position - transform.position).normalized;
		Vector3 localUp = body.transform.up;
		
		// Force qui attire un corps vers le bas
		body.AddForce(gravityUp * gravity);
		// Alligne les corps avec le centre de la planète
		body.MoveRotation(Quaternion.FromToRotation(localUp, gravityUp) * body.rotation);
	}

	public void AttractCharacterController(CharacterController characterController)
	{
		Vector3 gravityUp = (characterController.transform.position - transform.position).normalized;
		Vector3 localUp = characterController.transform.up;

		// Force qui attire un corps vers le bas
		characterController.Move(gravityUp * gravity);
		// Alligne les corps avec le centre de la planète
		//characterController.rotation = Quaternion.FromToRotation(localUp, gravityUp) * characterController.transform.rotation;
	}
}
