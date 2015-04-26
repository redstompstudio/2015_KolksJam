using UnityEngine;
using System.Collections;

public class ValveObject : MonoBehaviour, IActionReceiver 
{
    bool isClosed = false;
	bool isClosing = false;
	public float dropsSFXRate;
	public AudioSource dropSFX;
	public AudioSource closingSFX;

    void Start()
    {
        gameObject.AddComponent<BoxCollider>();
        Rigidbody r = gameObject.AddComponent<Rigidbody>();
        r.useGravity = false;
        r.isKinematic = true;

		StartCoroutine(PlayDropsSFX());
    }

	public void ExecuteAction()
	{
		CloseValve();
	}

	public IEnumerator PlayDropsSFX()
	{
		while(!isClosed)
		{
			dropSFX.Play();
			yield return new WaitForSeconds(dropsSFXRate);
		}
	}

    public void CloseValve()
    {
        if (!isClosed && !isClosing)
        {
			isClosing = true;
			if(closingSFX)
				closingSFX.Play();

            Debug.Log("CloseValve");
            LeanTween.rotateAroundLocal(gameObject, Vector3.up, 360f, 4.0f).setOnComplete(SetValveClosed);
        }
    }

    void SetValveClosed()
    {
        isClosed = true;

		if(dropSFX)
			dropSFX.Stop();
    }
}
