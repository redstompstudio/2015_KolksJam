using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class PlayerScript : MonoBehaviour
{
	private RigidbodyFirstPersonController controller;
	private const string interactableTagName = "Interactable";

	[Header("PLAYER VARS")]
	public Transform playerHeadRef;
	[SerializeField]
	private bool isAlive;

	[Header("INTERACTION VARS")]
	public float interactionDistance = 1.0f;
	public KeyCode interactionKey = KeyCode.Mouse0;
	
	[Header("FLASHLIGHT VARS")]
	private bool canUseFlashlight;
	public Light flashLight;
	public float flashlightChargeTime;

	[Header("STRESS VARS")]
	public bool isStressed;
	[Range(0, 2)]
	public float stressLevel = 0.0f;
	public AudioSource stressSound;

	[Header("FOOTSTEP VARS")]
	public bool playFootstepSounds;
	public AudioSource footStepSound;


    public Camera Camera
    {
        get
        {
            return controller.cam;
        }
    }

	private void Awake()
	{
		controller = GetComponent<RigidbodyFirstPersonController>();
		controller.enabled = false;
	}

	public void OnStartGame()
	{
		controller.enabled = true;
		isAlive = true;
	}

	private void Update()
	{
		if(!isAlive)
			return;

		HandleFlashlight();
		HandleInteraction();
		HandleStress();
		FootStepSounds();

		if(Input.GetKeyDown(KeyCode.I))
			BlinkFlashLight(10);
	}

	public void Kill()
	{
		isAlive = false;
		controller.enabled = false;
        footStepSound.Stop();

		StartCoroutine(RestartScene());
	}

	public IEnumerator RestartScene()
	{
		yield return new WaitForSeconds(4.0f);
		Application.LoadLevel("GameLoop");
	}

	private void HandleFlashlight()
	{
		if(flashlightChargeTime > 0.0f)
		{
			flashlightChargeTime -= Time.deltaTime;
			canUseFlashlight = false;
		}
		else
			canUseFlashlight = true;

		flashLight.enabled = canUseFlashlight;

		if(Input.GetKeyDown(KeyCode.F))
			flashLight.enabled = !flashLight.enabled;
	}

	public void CutFlashlight(float pLenght)
	{
		flashlightChargeTime += pLenght;
	}

	public void BlinkFlashLight(int pTimes)
	{
		StartCoroutine(BlinkCoroutine(pTimes));
	}

	private IEnumerator BlinkCoroutine(int pTimes)
	{
		while(pTimes > 0)
		{
			CutFlashlight(0.15f);
			pTimes--;
			yield return new WaitForSeconds(0.15f + Random.Range(0.05f, 0.4f));
		}
	}

	private void HandleInteraction()
	{
		if(Input.GetKeyDown(interactionKey))
		{
			RaycastHit hit;
			
			if(Physics.Raycast(playerHeadRef.position, playerHeadRef.forward, out hit, interactionDistance))
			{
				if(hit.transform.CompareTag(interactableTagName))
					hit.transform.SendMessage("ExecuteAction", SendMessageOptions.DontRequireReceiver);
			}
		}
	}

	private void HandleStress()
	{
		float blondeDistance = Vector3.Distance(transform.position, SceneManager.Instance.enemy.transform.position);

		if(blondeDistance < 10.0f)
		{
			if(blondeDistance < 5.0f)
				stressLevel = 2.0f;
			else
				stressLevel = 1.0f;
		}
		else
			stressLevel = 0.0f;

		isStressed = stressLevel > 0.0f;
		StressSound();
	}

	private void StressSound()
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

			if(stressSound.isPlaying)
			{
				stressSound.pitch = stressLevel / 1.5f;
			}
		}
	}

	private void FootStepSounds()
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



    /// <summary>
    /// DEBUG temporary UI
    /// </summary>
//    void OnGUI()
//    {
//        if (!isAlive)
//        {
//            float fWidth = Screen.width / 3;
//            float fHeight = Screen.height / 3;
//
//            if (GUI.Button(new Rect(
//                fWidth - (fWidth / 3),
//                fHeight - (fHeight / 3),
//                fWidth, fHeight
//                ), "You are seeing dead wet woman!\nClick to restart"))
//            {
//                
//            }
//        }
//    }
}
