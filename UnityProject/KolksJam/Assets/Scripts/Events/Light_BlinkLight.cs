using UnityEngine;
using System.Collections;

public class Light_BlinkLight : MonoBehaviour 
{
	public int blinkTimes;
	public Light light;



	public void BlinkLight()
	{
		StartCoroutine(Blink());
	}

	IEnumerator Blink()
	{
		int count = 0;

		while(count < blinkTimes)
		{
			count++;
			light.enabled = false;
			yield return new WaitForSeconds(Random.Range(0.1f, 0.35f));
			light.enabled = true;
			yield return new WaitForSeconds(Random.Range(0.1f, 0.35f));
		}
	}
}
