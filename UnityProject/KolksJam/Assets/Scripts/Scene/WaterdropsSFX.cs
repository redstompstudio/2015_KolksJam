using UnityEngine;
using System.Collections;

public class WaterdropsSFX : MonoBehaviour 
{
	public AudioSource dropSFX;
	public float repeatRate;

	// Use this for initialization
	void Start () 
	{
		dropSFX.loop = false;

		InvokeRepeating("PlaySound", 0.0f, repeatRate);
	}

	public void PlaySound()
	{
		dropSFX.Play();
	}
}
