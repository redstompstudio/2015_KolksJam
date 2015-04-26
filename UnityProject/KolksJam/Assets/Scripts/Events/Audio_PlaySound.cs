using UnityEngine;
using System.Collections;

public class Audio_PlaySound : MonoBehaviour 
{
	public AudioSource sfx;

	public void PlaySound()
	{
		if(sfx != null)
		{
			sfx.Play();
		}
	}
}
