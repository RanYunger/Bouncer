using UnityEngine;

public class GroundTile : Tile
{
    // Methods
    public void Start() // Start is called before the first frame update
    {
        tileSpawner = GameObject.FindObjectOfType<GroundSpawner>();
    }
}