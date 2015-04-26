using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject[] UIElements;

    public CanvasGroup Credits;
    public CanvasGroup TutorialMessage;

    public Text ValvesCount;

    public Image AIM;

    CanvasGroup mainMenu;

    bool showMainMenu = true;
    bool showCredits = false;
    bool showTutorialMessage = false;

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

        if (showTutorialMessage && TutorialMessage.alpha < 1)
            TutorialMessage.alpha += .7f * Time.deltaTime;

        if (!showTutorialMessage && TutorialMessage.alpha > 0)
            TutorialMessage.alpha -= .7f * Time.deltaTime;

    }

    public void OnClickPlay()
    {
        TutorialMessage.gameObject.SetActive(true);
        SceneManager.Instance.player.OnStartGame();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        showMainMenu = false;
        showTutorialMessage = true;
        AIM.color = new Color(1f, 1f, 1f, 1f);
        DisableUI();
        StartCoroutine("DisableTutorial");
    }

    IEnumerator DisableTutorial()
    {
        yield return new WaitForSeconds(5f);
        showTutorialMessage = false;
        yield return new WaitForSeconds(10f);
        gameObject.SetActive(false);
    }

    void DisableUI()
    {
        foreach (GameObject g in UIElements)
            g.SetActive(false);
    }

    public void OnClickExit()
    {
        Debug.Log("Sair");
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

    public void OnClickFullScreen()
    {
        Debug.Log("FullScreen");
        Screen.fullScreen = !Screen.fullScreen;
    }
}
