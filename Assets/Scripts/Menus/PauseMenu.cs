using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : Menu
{
    // Methods
    public void ResumeGame()
    {
        OnClick();

        FindObjectOfType<GameManager>().ResumeGame();
    }
    public void ShowMainMenu()
    {
        OnClick();
        AudioManager.instance.StopAll();

        SceneManager.LoadScene(0);
    }
}
