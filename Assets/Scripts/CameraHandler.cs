using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    // Constants
    private Vector3 CAMDISTANCE = new Vector3(0f, 3f, -8f);

    // Fields
    public Transform player;

    // Methods
    public void Update() // Update is called once per frame
    {
        transform.position = player.position + CAMDISTANCE;
    }
}
