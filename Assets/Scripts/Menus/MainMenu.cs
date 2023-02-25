using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu: Menu
{
    // Fields
    public GameObject optionsMenu;

    // Methods
    public void PlayGame()
    {
        OnClick();

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void ShowOptions()
    {
        OnClick();

        gameObject.SetActive(false);
        optionsMenu.SetActive(true);
    }
    public void QuitGame()
    {
        OnClick();

        Application.Quit();
    }
}
