using UnityEngine;
using System.Collections;

public class FinalCutscene : MonoBehaviour 
{
	public Camera cutsceneCamera;
	public GameObject crosshairRef;

	public AudioSource deathSFX;

	public GameObject morenaDaCutscene;

	public Transform valveCamRef;
	public Light valveSpotlight;
	public AudioSource tamtaaaaam;

	public void StartCutscene()
	{
		Camera.main.gameObject.SetActive(false);
		cutsceneCamera.enabled = true;
		crosshairRef.SetActive(false);

		deathSFX.Play();
		morenaDaCutscene.SetActive(true);

		StartCoroutine(EmposoredByValve());
	}

	void OnGUI()
	{
		if(GUILayout.Button("Start Cutscene"))
		{
			StartCutscene();
		}
	}

	IEnumerator EmposoredByValve()
	{
		yield return new WaitForSeconds(4.0f);
		valveSpotlight.enabled = true;

		cutsceneCamera.transform.position = valveCamRef.position;
		cutsceneCamera.transform.rotation = valveCamRef.rotation;

		tamtaaaaam.Play();
	}
}
	