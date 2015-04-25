using UnityEngine;
using System.Collections;

public class PlayerInteraction : MonoBehaviour
{
	public Transform playerHeadRef;
	public float interactionDistance = 1.0f;

	private const string interactableTagName = "Interactable";

	public Light flashLight;

	// Update is called once per frame
	void Update () 
	{
		if(Input.GetKeyDown(KeyCode.F))
		{
			flashLight.enabled = !flashLight.enabled;
		}

		if(Input.GetKeyDown(KeyCode.E))
		{
			RaycastHit hit;

			if(Physics.Raycast(playerHeadRef.position, playerHeadRef.forward, out hit, interactionDistance))
			{
				if(hit.transform.CompareTag(interactableTagName))
					hit.transform.SendMessage("ExecuteAction", SendMessageOptions.DontRequireReceiver);
			}
		}
	}
}
