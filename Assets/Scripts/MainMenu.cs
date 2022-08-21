using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public string GameSceneName;

    public void GameButton()
    {
        SceneManager.LoadScene(GameSceneName);
    }

    public void ExitButton()
    {
        Application.Quit();
    }
}
