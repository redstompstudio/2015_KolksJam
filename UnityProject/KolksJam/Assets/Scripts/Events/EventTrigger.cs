using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EventTrigger : MonoBehaviour 
{
	public bool isOneTimeUse;
	private bool wasActivated;

	public List<string> validTags = new List<string>();

	public GameObject target;
	public string methodName;

	void Start()
	{
	}

	public virtual void OnTriggerEnter(Collider pOther)
	{
		if(wasActivated && isOneTimeUse)
			return;
		else
			wasActivated = true;

		if((validTags.Count < 1 || validTags.Contains(pOther.tag)))
		{
			if(target != null)
				target.SendMessage (methodName, SendMessageOptions.DontRequireReceiver);
		}
	}
}
