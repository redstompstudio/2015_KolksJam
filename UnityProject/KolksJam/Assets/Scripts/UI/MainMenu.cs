using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public CanvasGroup credits;

    CanvasGroup mainMenu;

    bool showMainMenu = true;
    bool showCredits = false;

    void Start()
    {
        mainMenu = gameObject.GetComponent<CanvasGroup>();
    }

    void Update()
    {
        if (showMainMenu && mainMenu.alpha < 1)
            mainMenu.alpha += .7f * Time.deltaTime;

        if (!showMainMenu && mainMenu.alpha > 0)
            mainMenu.alpha -= .7f * Time.deltaTime;

        if (showCredits && credits.alpha < 1)
            credits.alpha += .7f * Time.deltaTime;

        if (!showCredits && credits.alpha > 0)
            credits.alpha -= .7f * Time.deltaTime;
    }

    public void OnClickPlay()
    {

    }

    public void OnClickExit()
    {

    }

    public void OnClickCredits()
    {
        showMainMenu = false;
        showCredits = true;
    }

    public void OnClickCloseCredits()
    {
        showMainMenu = true;
        showCredits = false;
    }
}
