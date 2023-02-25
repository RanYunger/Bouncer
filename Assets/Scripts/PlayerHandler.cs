using System;
using System.Diagnostics;
using UnityEngine;

public class PlayerHandler : MonoBehaviour
{
    // Constants
    private const float SPEED = 5f;
    private const float CLOCKWISE = 90f;

    // Fields
    public Rigidbody player;
    public CorridorHandler corridorHandler;
    public bool isAirborne;
    public bool isWithinCorridor;
    public int lives;

    // Methods
    public void Start()
    {
        name = "Player"; // For gems collision detection
    }
    public void Update() // Update is called once per frame
    {
        Vector3 forwardMove = transform.forward * SPEED * Time.fixedDeltaTime;
        Vector3 horizontalMove = transform.right * Input.GetAxis("Horizontal") * SPEED * Time.fixedDeltaTime;

        if ((!GameManager.gamePaused) && (!GameManager.gameEnded))
        {
            player.MovePosition(player.position + forwardMove + horizontalMove);
            if ((Input.GetKey(KeyCode.Space)) && (!isAirborne))
            {
                isAirborne = true;
                player.AddForce(0, 350f, 0);
            }

            isWithinCorridor = corridorHandler.IsPlayerWithinCorridor();
        }
    }
    private void OnCollisionEnter(Collision collisionInfo)
    {
        isAirborne = collisionInfo.collider.name != corridorHandler.currentGround;

        if (!isAirborne)
            corridorHandler.rotationAngle = 0f;
        else if (collisionInfo.collider.name != "Obstacle")
        {
            corridorHandler.currentGround = collisionInfo.collider.name;
            corridorHandler.rotationAngle = transform.position.x > collisionInfo.collider.transform.position.x ? CLOCKWISE : -CLOCKWISE;
        }

        if (collisionInfo.collider.name == "Obstacle")
        {
            AudioManager.instance.Play("Hit");
            lives -= Convert.ToInt32(lives > 0);
            if (lives == 0)
                FindObjectOfType<GameManager>().GameOver();
        }
    }
}