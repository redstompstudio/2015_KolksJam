using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class PlayerScript : MonoBehaviour
{
	public Transform playerHeadRef;
	public float interactionDistance = 1.0f;

	private const string interactableTagName = "Interactable";

	public Light flashLight;

	public bool isStressed;
	public AudioSource stressSound;

	public bool playFootstepSounds;
	public AudioSource footStepSound;

	private RigidbodyFirstPersonController controller;

	void Awake()
	{
		controller = GetComponent<RigidbodyFirstPersonController>();
	}

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

		StressSound();
		FootStepSounds();
	}

	public void StressSound()
	{
		if(stressSound != null)
		{
			if(isStressed && !stressSound.isPlaying)
			{
				stressSound.Play();
			}
			else if(!isStressed && stressSound.isPlaying)
			{
				stressSound.Stop();
			}
		}
	}


	public void FootStepSounds()
	{
		if(!playFootstepSounds || footStepSound == null)
			return;

		if(controller.Grounded && controller.Velocity.sqrMagnitude > 1.0f)
		{
			if(!footStepSound.isPlaying)
				footStepSound.Play();
		}
		else if(!controller.Grounded || controller.Velocity.sqrMagnitude <= 1.0f)
		{
			if(footStepSound.isPlaying)
				footStepSound.Stop();
		}
	}
}
