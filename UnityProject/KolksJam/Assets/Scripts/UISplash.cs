using UnityEngine;
using System.Collections;

public class UISplash : MonoBehaviour
{
    void Start()
    {
        RectTransform ImageRectTransform = gameObject.GetComponent<RectTransform>();
        SpriteRenderer Image = gameObject.GetComponent<SpriteRenderer>();


        ImageRectTransform.sizeDelta = new Vector2(Screen.width, Screen.height);
    }
}
