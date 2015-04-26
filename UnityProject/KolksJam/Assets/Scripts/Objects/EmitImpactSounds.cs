using UnityEngine;
using System.Collections;

public class EmitImpactSounds : MonoBehaviour 
{
	private Rigidbody cachedRigidbody;
	public AudioSource impactSFX;

	void Awake()
	{
		cachedRigidbody = GetComponent<Rigidbody>();
	}

	void OnCollisionEnter(Collision pColInfo)
	{
		Debug.Log(pColInfo.relativeVelocity.sqrMagnitude);

		if(pColInfo.relativeVelocity.sqrMagnitude > 5.0f)
		{
			impactSFX.Play();
		}
	}
}
