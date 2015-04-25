using UnityEngine;
using System.Collections;

public class DoorObject : MonoBehaviour, IActionReceiver
{
    public bool isTwoSided = true;

    public float zColliderSize = 3.5f;
    public float zColliderCenter = .25f;

    public bool canOpenDoor = false;
    bool isAnimating = false;

    void Start()
    {
        BoxCollider originalCollider = gameObject.AddComponent<BoxCollider>();
        Rigidbody rigidbody = gameObject.AddComponent<Rigidbody>();

        rigidbody.useGravity = false;
        rigidbody.isKinematic = true;

        if (!isTwoSided)
        {
            BoxCollider secondCollider = gameObject.AddComponent<BoxCollider>();
            secondCollider.size = new Vector3(secondCollider.size.x, zColliderSize / 2, 2f);
            secondCollider.center = new Vector3(secondCollider.center.x, zColliderCenter, secondCollider.center.z);
            secondCollider.isTrigger = true;
        }
        else
        {
            BoxCollider secondCollider = gameObject.AddComponent<BoxCollider>();
            secondCollider.size = new Vector3(secondCollider.size.x, zColliderSize, 2f);
            secondCollider.isTrigger = true;
        }
    }

    void OpenDoor()
    {
        Debug.Log("OpenDoor");
        LeanTween.rotateAroundLocal(gameObject, Vector3.forward, -85f, 1.2f).setOnComplete(CloseDoor);
    }

    IEnumerator ICloseDoor()
    {
        yield return new WaitForSeconds(2.5f);
        Debug.Log("CloseDoor");
        LeanTween.rotateAroundLocal(gameObject, Vector3.forward, 85f, 1.2f).setOnComplete(AllowAnimation);
    }

    void AllowAnimation()
    {
        isAnimating = false;
    }

    void CloseDoor()
    {
        StartCoroutine("ICloseDoor");
    }

    public void ExecuteAction()
    {
        if (canOpenDoor && !isAnimating)
        {
            isAnimating = true;
            OpenDoor();
        }
    }

    void OnTriggerEnter()
    {
        canOpenDoor = true;
    }

    void OnTriggerExit()
    {
        canOpenDoor = false;
    }
}
