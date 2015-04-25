using UnityEngine;
using System.Collections;

public class ValveObject : MonoBehaviour, IActionReceiver 
{
    bool isClosed = false;

    public void CloseValve()
    {
        if (!isClosed)
        {
            Debug.Log("CloseValve");
            LeanTween.rotateAroundLocal(gameObject, Vector3.up, 360f, 2.5f).setOnComplete(SetValveClosed);
        }
    }

    public void ExecuteAction()
    {
        CloseValve();
    }

    void SetValveClosed()
    {
        isClosed = true;
    }
}
