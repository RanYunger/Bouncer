using System;
using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{
    // Fields
    public PlayerHandler playerHandler;
    public CorridorHandler corridorHandler;
    public TMP_Text inGameScoreText;
    public TMP_Text finalScoreText;
    public GameObject heart;
    public TMP_Text livesText;
    public TMP_Text notificationText;
    public TMP_Text timerText;

    // Methods
    public void Update() // Update is called once per frame
    {
        TimeSpan startTimeSpan, leftTimeSpan;
        string scoreStr, blinkingStr;

        if ((!GameManager.gamePaused) && (!GameManager.gameEnded))
        {
            startTimeSpan = leftTimeSpan = new TimeSpan(0, 0, !playerHandler.isWithinCorridor ? 3 : 10);
            scoreStr = playerHandler.player.position.z.ToString("0");
            livesText.text = "x" + playerHandler.lives;
            if (playerHandler.isWithinCorridor)
                inGameScoreText.text = finalScoreText.text = string.Format("Score: {0}", new string('0', 6 - scoreStr.Length) + scoreStr);

            notificationText.enabled = timerText.enabled = (GemSpawner.gemEffectStopwatch.IsRunning) || (GameManager.gameOverStopwatch.IsRunning);
            if (GameManager.gameOverStopwatch.IsRunning)
            {
                leftTimeSpan -= GameManager.gameOverStopwatch.Elapsed;
                notificationText.text = "Return to Arena!";
            }
            else if (GemSpawner.gemEffectStopwatch.IsRunning)
            {
                leftTimeSpan -= GemSpawner.gemEffectStopwatch.Elapsed;
                blinkingStr = corridorHandler.blinkingObjects == corridorHandler.spawnedHoles ? "Holes" : "Obstacles";
                notificationText.text = string.Format("No {0} For:", blinkingStr);
            }

            timerText.text = notificationText.enabled ? string.Format("{0:00}.{1:00}", leftTimeSpan.Seconds, leftTimeSpan.Milliseconds / 10) : string.Empty;
        }
    }
}
