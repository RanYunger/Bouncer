using System;
using UnityEngine;

public class GemHandler : MonoBehaviour
{
    // Constants
    private const int FULLLIFE = 3;

    // Methods
    public void Update() // Update is called once per frame
    {
        transform.Rotate(new Vector3(0f, 0f, 90f) * Time.deltaTime);
    }
    public void OnTriggerEnter(Collider collider)
    {
        if (collider.name == "Player")
        {
            CorridorHandler corridorHandler = FindObjectOfType<CorridorHandler>();
            PlayerHandler playerHandler = FindObjectOfType<PlayerHandler>();

            AudioManager.instance.Play(name);
            switch (name)
            {
                case "ExtraLife":
                    playerHandler.lives += Convert.ToInt32(playerHandler.lives < FULLLIFE);
                    break;
                case "FullLife":
                    playerHandler.lives = FULLLIFE;
                    break;
                default:
                    corridorHandler.blinkingObjects = name == "ClearHoles" ? corridorHandler.spawnedHoles : corridorHandler.spawnedObstacles;
                    corridorHandler.ToggleBlinkinObjects(corridorHandler.blinkingObjects == corridorHandler.spawnedHoles);
                    GemSpawner.gemEffectStopwatch.Start();
                    break;
            }

            OnBecameInvisible();
        }
    }
    public void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}