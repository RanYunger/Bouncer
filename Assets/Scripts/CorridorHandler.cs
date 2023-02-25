using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorridorHandler : MonoBehaviour
{
    // Fields
    public GroundSpawner groundSpawner;
    public LeftWallSpawner leftWallSpawner;
    public CeilingSpawner ceilingSpawner;
    public RightWallSpawner rightWallSpawner;
    public GemSpawner gemSpawner;
    public PlayerHandler playerHandler;
    public float rotationAngle;
    public string currentGround;
    public static bool isBlinking;
    public List<GameObject> blinkingObjects, spawnedHoles, spawnedObstacles;
    private Transform[] spawners;

    // Methods
    public void Start() // Start is called before the first frame update
    {
        spawners = new Transform[] { groundSpawner.transform, leftWallSpawner.transform, ceilingSpawner.transform, rightWallSpawner.transform };
        blinkingObjects = null;
        spawnedHoles = new List<GameObject>();
        spawnedObstacles = new List<GameObject>();
    }
    public void Update() // Update is called once per frame
    {
        int[] childCounts = { spawners[0].childCount, spawners[1].childCount, spawners[2].childCount, spawners[3].childCount };
        Quaternion sourceRotation, destinationRotation;

        if ((!GameManager.gamePaused) && (!GameManager.gameEnded))
            if ((playerHandler.isAirborne) && (Mathf.Abs(rotationAngle) == 90f))
            {
                for (int spawnerOffset = 0; spawnerOffset < spawners.Length; spawnerOffset++)
                    for (int i = 0; i < childCounts[spawnerOffset]; i++)
                    {
                        sourceRotation = spawners[spawnerOffset].GetChild(i).rotation;
                        destinationRotation = Quaternion.AngleAxis(rotationAngle - sourceRotation.z, new Vector3(0, 0, 1));
                        spawners[spawnerOffset].GetChild(i).rotation = Quaternion.Slerp(sourceRotation, destinationRotation, Time.deltaTime);
                    }
            }
    }
    public bool IsPlayerWithinCorridor()
    {
        int[] childCounts = { spawners[0].childCount, spawners[1].childCount, spawners[2].childCount, spawners[3].childCount };
        BoxCollider currentBoxCollider = null;

        for (int spawnerOffset = 0; spawnerOffset < spawners.Length; spawnerOffset++)
            for (int i = 0; i < childCounts[spawnerOffset]; i++)
            {
                currentBoxCollider = spawners[spawnerOffset].GetChild(i).GetComponent<BoxCollider>();
                if (currentBoxCollider.bounds.Contains(playerHandler.player.position))
                    return true;
            }

        return false;
    }
    public Vector3 GetGemSpawn()
    {
        Vector3 topSpawnerPosition = GetTopSpawnerPosition(), leftSpawnerPosition = GetLeftSpawnerPosition();

        return new Vector3(leftSpawnerPosition.x, topSpawnerPosition.y + 5f, playerHandler.player.position.z + 150);
    }
    public void ToggleBlinkinObjects(bool active)
    {
        for (int i = 0; i < blinkingObjects.Count; i++)
            blinkingObjects[i].SetActive(active);
    }
    public void BlinkObjects()
    {
        StartCoroutine(BlinkCoroutine());
    }
    private Vector3 GetTopSpawnerPosition()
    {
        Vector3 topSpawnerPosition = spawners[0].position;

        for (int i = 1; i < spawners.Length; i++)
            topSpawnerPosition = spawners[i].position.y > topSpawnerPosition.y ? spawners[i].position : topSpawnerPosition;

        return topSpawnerPosition;
    }
    private Vector3 GetLeftSpawnerPosition()
    {
        Vector3 leftSpawnerPosition = spawners[0].position;

        for (int i = 1; i < spawners.Length; i++)
            leftSpawnerPosition = spawners[i].position.x < leftSpawnerPosition.x ? spawners[i].position : leftSpawnerPosition;

        return leftSpawnerPosition;
    }
    private IEnumerator BlinkCoroutine()
    {
        while (GemSpawner.gemEffectStopwatch.IsRunning)
        {
            ToggleBlinkinObjects(!blinkingObjects[0].activeSelf);
            yield return new WaitForSeconds(0.5f);
            ToggleBlinkinObjects(!blinkingObjects[0].activeSelf);
            yield return new WaitForSeconds(0.5f);
        }
        ToggleBlinkinObjects(blinkingObjects != spawnedHoles); // True = Show holes ; False = Show obstacles
    }
}