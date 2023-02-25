using UnityEngine;

public class LeftWallTile : Tile
{
    // Methods
    public void Start() // Start is called before the first frame update
    {
        tileSpawner = GameObject.FindObjectOfType<LeftWallSpawner>();
    }
}