using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public CanvasGroup Credits;

    public Text ValvesCount;

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

        if (showCredits && Credits.alpha < 1)
            Credits.alpha += .7f * Time.deltaTime;

        if (!showCredits && Credits.alpha > 0)
            Credits.alpha -= .7f * Time.deltaTime;

        ValvesCount.text = SceneManager.Instance.GetValveCount() + "x";
    }

    public void OnClickPlay()
    {
        SceneManager.Instance.player.OnStartGame();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        showMainMenu = false;
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
