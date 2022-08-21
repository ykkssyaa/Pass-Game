using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public string GameSceneName;
    public GameObject helpScreen;
    public GameObject mainScreen;

    public void GameButton()
    {
        SceneManager.LoadScene(GameSceneName);
    }

    public void ExitButton()
    {
        Application.Quit();
    }

    public void HelpButton()
    {

        helpScreen.SetActive(true);
        mainScreen.SetActive(false);
    }

    public void ExitHelpScreen()
    {
        helpScreen.SetActive(false);
        mainScreen.SetActive(true);
    }
}
