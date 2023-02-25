using UnityEngine;

public class TileSpawner : MonoBehaviour
{
    // Constants
    private const float GROUNDLEVEL = 1.5f;
    private const float LEFTWALLLEVEL = -3.5f;
    private const float CEILINGLEVEL = 8.0f;
    private const float RIGHTWALLLEVEL = 3.5f;

    // Fields
    public GameObject tilePrefab;
    public GameObject obstaclePrefab;
    public string tileName;
    [HideInInspector]
    public Vector3 nextSpawnPoint;
    public bool canSpawnHoles, canSpawnObstacles;

    // Methods
    public void Start() // Start is called before the first frame update
    {
        for (int i = 0; i < 3; i++)
            SpawnTile();
        canSpawnObstacles = true;
        canSpawnHoles = true;
        for (int i = 0; i < 17; i++)
            SpawnTile();
    }
    public void SpawnTile()
    {
        GameObject tile = Instantiate(tilePrefab, nextSpawnPoint, Quaternion.identity);
        GameObject obstacle;

        tile.name = tileName;
        tile.transform.parent = transform;
        nextSpawnPoint = tile.transform.GetChild(1).position;

        if ((Random.value < 0.15) && (canSpawnHoles))
        {
            tile.SetActive(false);
            FindObjectOfType<CorridorHandler>().spawnedHoles.Add(tile);
        }
        if ((tile.activeSelf) && ((Random.value < 0.4) && (canSpawnObstacles)))
        {
            obstacle = SpawnObstacle(tile);
            FindObjectOfType<CorridorHandler>().spawnedObstacles.Add(obstacle);
        }
    }
    private GameObject SpawnObstacle(GameObject tile)
    {
        float xPos = 0.0f, yPos = 0.0f;
        Vector3 obstacleScale = new Vector3(Random.Range(1, RIGHTWALLLEVEL / 1.5f), Random.Range(1, CEILINGLEVEL / 1.5f), 1);
        Vector3 position = tile.transform.position;
        GameObject obstacle;

        switch (tile.name)
        {
            case "GroundTile":
                {
                    xPos = Random.Range(LEFTWALLLEVEL, RIGHTWALLLEVEL);
                    yPos = GROUNDLEVEL + (obstacleScale.y / 2);
                }
                break;
            case "LeftWallTile":
                {
                    xPos = LEFTWALLLEVEL + (obstacleScale.x / 2);
                    yPos = Random.Range(GROUNDLEVEL, CEILINGLEVEL);
                }
                break;
            case "CeilingTile":
                {
                    xPos = Random.Range(LEFTWALLLEVEL, RIGHTWALLLEVEL);
                    yPos = CEILINGLEVEL - (obstacleScale.y / 2);
                }
                break;
            case "RightWallTile":
                {
                    xPos = RIGHTWALLLEVEL - (obstacleScale.x / 2);
                    yPos = Random.Range(GROUNDLEVEL, CEILINGLEVEL);
                }
                break;
        }

        position += new Vector3(xPos, yPos, 0);

        obstacle = Instantiate(obstaclePrefab, position, Quaternion.identity);
        obstacle.name = "Obstacle";
        obstacle.transform.localScale = obstacleScale;
        obstacle.transform.parent = tile.transform;

        return obstacle;
    }
}