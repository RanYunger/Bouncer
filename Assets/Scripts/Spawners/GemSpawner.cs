using System.Diagnostics;
using UnityEngine;

public class GemSpawner : MonoBehaviour
{
    // Constants
    private const int GEMSPAWNINTERVAL = 16;
    private const int BLINKINGINTERVAL = 3;
    private const int EFFECTINTERVAL = 10;

    // Fields
    public GameObject extraLifeGemPrefab;
    public GameObject fullLifeGemPrefab;
    public GameObject clearHolesGemPrefab;
    public GameObject clearObstaclesGemPrefab;
    public static Stopwatch gemSpawnStopwatch, gemEffectStopwatch;
    private GameObject[] gemPrefabs;
    private string[] gemNames;

    // Methods
    public void Start() // Start is called before the first frame update
    {
        gemPrefabs = new GameObject[] { extraLifeGemPrefab, fullLifeGemPrefab, clearHolesGemPrefab, clearObstaclesGemPrefab };
        gemNames = new string[] { "ExtraLife", "FullLife", "ClearHoles", "ClearObstacles" };
        gemSpawnStopwatch = new Stopwatch();
        gemEffectStopwatch = new Stopwatch();

        gemSpawnStopwatch.Start();
    }

    public void Update() // Update is called once per frame
    {
        if ((gemSpawnStopwatch.Elapsed.Seconds == GEMSPAWNINTERVAL) && (Random.value < 0.2))
        {
            SpawnGem();
            gemSpawnStopwatch.Restart();
        }
        if ((gemEffectStopwatch.Elapsed.Seconds == (EFFECTINTERVAL - BLINKINGINTERVAL)) && (!CorridorHandler.isBlinking))
        {
            CorridorHandler.isBlinking = true;
            AudioManager.instance.Play("GemCountdown");
            FindObjectOfType<CorridorHandler>().BlinkObjects();
        }
        else if ((gemEffectStopwatch.Elapsed.Seconds == EFFECTINTERVAL) && (CorridorHandler.isBlinking))
        {
            CorridorHandler.isBlinking = false;
            AudioManager.instance.Stop("GemCountdown");
            gemEffectStopwatch.Reset();
        }
    }
    private void SpawnGem()
    {
        int gemType = Random.Range(0, gemPrefabs.Length);
        Vector3 position = FindObjectOfType<CorridorHandler>().GetGemSpawn();
        GameObject gem = Instantiate(gemPrefabs[gemType], position, Quaternion.AngleAxis(-90f, new Vector3(1, 0, 0)));

        gem.name = gemNames[gemType];
    }
}