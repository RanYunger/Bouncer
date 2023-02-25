using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : Menu
{
    // Methods
    public void RestartGame()
    {
        OnClick();

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void ShowMainMenu()
    {
        OnClick();
        AudioManager.instance.StopAll();

        SceneManager.LoadScene(0);
    }
}