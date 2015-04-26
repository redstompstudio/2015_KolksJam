using UnityEngine;
using System.Collections;

public class PlayerHead : MonoBehaviour 
{
	public Transform leanLeftRef;
	public Transform leanRightRef;
	private Vector3 originalPosition;

	// Use this for initialization
	void Start () 
	{
		originalPosition = transform.localPosition;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetKey(KeyCode.Q))	
		{
			if(!Physics.Raycast(transform.position, -transform.right, 0.5f))
				transform.localPosition = Vector3.Lerp(transform.localPosition, leanLeftRef.localPosition, Time.deltaTime * 5.0f);
			else
				transform.localPosition = Vector3.Lerp(transform.localPosition, originalPosition, Time.deltaTime * 5.0f);
		}
		else if(Input.GetKey(KeyCode.E))
		{
			transform.localPosition = Vector3.Lerp(transform.localPosition, leanRightRef.localPosition, Time.deltaTime * 5.0f);

			if(!Physics.Raycast(transform.position, -transform.right, 0.5f))
				transform.localPosition = Vector3.Lerp(transform.localPosition, leanRightRef.localPosition, Time.deltaTime * 5.0f);
			else
				transform.localPosition = Vector3.Lerp(transform.localPosition, originalPosition, Time.deltaTime * 5.0f);
		}
		else
		{
			transform.localPosition = Vector3.Lerp(transform.localPosition, originalPosition, Time.deltaTime * 5.0f);
		}
	}
}
