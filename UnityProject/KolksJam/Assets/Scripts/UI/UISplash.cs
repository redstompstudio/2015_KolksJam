using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UISplash : MonoBehaviour
{
    Image ImageRef;
    public CanvasGroup Splash;

    bool fadeIn = false;
    bool fadeOut = false;

    void Start()
    {
        RectTransform ImageRectTransform = gameObject.GetComponent<RectTransform>();
        ImageRef = gameObject.GetComponent<Image>();

        Splash.alpha = 0f;
        ImageRectTransform.sizeDelta = new Vector2(Screen.width, Screen.height);

        StartCoroutine("AnimSplash");

    }

    void Update()
    {
        if (fadeIn && Splash.alpha < 1)
            Splash.alpha += 2f * Time.deltaTime;

        if (fadeOut && Splash.alpha > 0)
            Splash.alpha -= 2f * Time.deltaTime;
    }

    IEnumerator AnimSplash()
    {
        yield return new WaitForSeconds(1f);
        fadeIn = true;
        yield return new WaitForSeconds(3.5f);
        fadeIn = false;
        fadeOut = true;
        yield return new WaitForSeconds(1.5f);
        StarGame();
    }

    void StarGame()
    {
        Application.LoadLevel(1);
    }
}
