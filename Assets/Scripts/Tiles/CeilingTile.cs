using UnityEngine;

public class CeilingTile : Tile
{
    // Methods
    public void Start() // Start is called before the first frame update
    {
        tileSpawner = GameObject.FindObjectOfType<CeilingSpawner>();
    }
}