using UnityEngine;
using System.Collections;

public class PlayerInteraction : MonoBehaviour
{
	public Transform playerHeadRef;
	public float interactionDistance = 1.0f;

	private const string interactableTagName = "Interactable";

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetKeyDown(KeyCode.E))
		{
			RaycastHit hit;

			if(Physics.Raycast(playerHeadRef.position, playerHeadRef.forward, out hit, interactionDistance))
			{
				if(hit.transform.CompareTag(interactableTagName))
				{
					hit.transform.SendMessage("ExecuteAction", SendMessageOptions.DontRequireReceiver);
				}
			}
		}
	}
}
