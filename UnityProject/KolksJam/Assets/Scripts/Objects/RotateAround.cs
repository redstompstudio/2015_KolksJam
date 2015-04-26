using UnityEngine;
using System.Collections;

public class RotateAround : MonoBehaviour 
{
	public float rotationSpeed = 10.0f;
	public Vector3 axis = Vector3.up;

	private Transform cachedTransform;

	void Awake()
	{
		cachedTransform = transform;
	}

	void Update()
	{
		cachedTransform.RotateAround(axis, rotationSpeed * Time.deltaTime);
	}
}
