using System;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Fields
    public AudioManager audioManager;
    public PlayerHandler playerHandler;
    public GameObject gameOverUI;
    public GameObject pauseMenu;
    public static Stopwatch gameOverStopwatch;
    public static bool gamePaused;
    public static bool gameEnded;

    // Methods
    public void Start() // Start is called before the first frame update
    {
        Time.timeScale = 1f;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.None;

        gamePaused = gameEnded = false;
        gameOverStopwatch = new Stopwatch();

        audioManager = AudioManager.instance;
        audioManager.Play("Music");
    }
    public void Update() // Update is called once per frame
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gamePaused)
                ResumeGame();
            else
                PauseGame();
        }

        if ((!gamePaused) && (!gameEnded))
        {
            if (!playerHandler.isWithinCorridor)
            {
                gameOverStopwatch.Start();
                audioManager.PlayOrResume("CorridorCountdown");

                if (GemSpawner.gemEffectStopwatch.IsRunning)
                {
                    GemSpawner.gemEffectStopwatch.Stop();
                    audioManager.Stop("GemCountdown");
                }
            }
            else
            {
                gameOverStopwatch.Reset();
                audioManager.Stop("CorridorCountdown");

                if (CorridorHandler.isBlinking)
                {
                    GemSpawner.gemEffectStopwatch.Start();
                    audioManager.PlayOrResume("GemCountdown");
                }
            }

            if (gameOverStopwatch.Elapsed.TotalSeconds >= 3)
            {
                gameOverStopwatch.Reset();
                GameOver();
            }
        }
    }
    public void GameOver()
    {
        Time.timeScale = 0f;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        gameOverUI.SetActive(gameEnded = true);
        Destroy(FindObjectOfType<UIHandler>().heart); // The heart prefab in the player's UI.

        audioManager.StopAll();
    }
    public void PauseGame()
    {
        Time.timeScale = 0f;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        pauseMenu.SetActive(gamePaused = true);
        gameOverStopwatch.Stop();
        GemSpawner.gemEffectStopwatch.Stop();

        audioManager.PauseAll();
    }
    public void ResumeGame()
    {
        Time.timeScale = 1f;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        pauseMenu.SetActive(gamePaused = false);
        if (!playerHandler.isWithinCorridor)
            gameOverStopwatch.Start();
        else if (GemSpawner.gemEffectStopwatch.Elapsed > TimeSpan.Zero)
            GemSpawner.gemEffectStopwatch.Start();

        audioManager.ResumeAll();
    }
}