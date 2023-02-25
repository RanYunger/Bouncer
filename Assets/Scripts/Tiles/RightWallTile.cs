using UnityEngine;

public class RightWallTile : Tile
{
    // Methods
    public void Start() // Start is called before the first frame update
    {
        tileSpawner = GameObject.FindObjectOfType<RightWallSpawner>();
    }
}