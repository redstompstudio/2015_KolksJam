using UnityEngine;
using System.Collections;

public class Physics_ApplyForce : MonoBehaviour 
{
	public Rigidbody targetRigidbody;

	public float force;
	public Vector3 direction;
	public bool directionRelativeToTarget;
	public ForceMode forceMode;

	public void ApplyForce()
	{
		if(directionRelativeToTarget)
			targetRigidbody.AddRelativeForce(direction * force, forceMode);
		else
			targetRigidbody.AddForce(direction * force, forceMode);
	}
}
