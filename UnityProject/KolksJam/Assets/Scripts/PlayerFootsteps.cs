using UnityEngine;
using System.Collections;

public class PlayerFootsteps : MonoBehaviour 
{
	private Rigidbody cachedRigidbody;

	void Awake()
	{
		cachedRigidbody = GetComponent<Rigidbody>();
	}

	public void Update()
	{

	}
}
