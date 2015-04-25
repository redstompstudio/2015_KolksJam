using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UISplash : MonoBehaviour
{
    Image ImageRef;

    void Start()
    {
        RectTransform ImageRectTransform = gameObject.GetComponent<RectTransform>();
        ImageRef = gameObject.GetComponent<Image>();

        ImageRef.color = new Color(1f, 1f, 1f, 0f);
        ImageRectTransform.sizeDelta = new Vector2(Screen.width, Screen.height);

        StartCoroutine("AnimSplash");

    }

    IEnumerator AnimSplash()
    {
        yield return new WaitForSeconds(1f);
        LeanTween.value(gameObject, UpdateAlpha, 0f, 1f, 1.5f);
        yield return new WaitForSeconds(3.5f);
        LeanTween.value(gameObject, UpdateAlpha, 1f, 0f, 1.5f);
        yield return new WaitForSeconds(2.5f);
        Application.LoadLevel("Player_Movement");
    }

    void UpdateAlpha(float f)
    {
        ImageRef.color = new Color(1f, 1f, 1f, f);
    }

    
}
