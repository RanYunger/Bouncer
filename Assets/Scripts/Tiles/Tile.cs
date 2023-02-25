using UnityEngine;

public abstract class Tile : MonoBehaviour
{
    // Fields
    protected TileSpawner tileSpawner;

    // Methods
    public void OnTriggerExit(Collider other)
    {
        for (int i = 0; i < 3; i++)
            tileSpawner.SpawnTile();

        SetForDestruction();
        Destroy(gameObject, 1);
    }
    private void SetForDestruction()
    {
        CorridorHandler corridorHandler = FindObjectOfType<CorridorHandler>();

        for (int i = 0; i < corridorHandler.spawnedObstacles.Count; i++)
            if (corridorHandler.spawnedObstacles[i].gameObject.transform.parent == transform)
                corridorHandler.spawnedObstacles.RemoveAt(i);
        corridorHandler.spawnedHoles.Remove(gameObject);
    }
}