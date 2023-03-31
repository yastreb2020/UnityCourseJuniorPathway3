using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    private float speed = 15;
    private float normalSpeed;
    private PlayerController playerControllerScript;
    private float leftBound = -15;
    // Start is called before the first frame update
    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        normalSpeed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        if (!(playerControllerScript.transform.position.x < 0)) //if not initial animation
        {
            CheckDashMode();

            CheckGameOver();

            CheckOutOfBounds();
        }
    }

    private void CheckDashMode()
    {
        if (playerControllerScript.dashMode)
        {
            speed = normalSpeed * 2;
        }
        else
        {
            speed = normalSpeed;
        }
    }

    private void CheckGameOver()
    {
        if (!playerControllerScript.gameOver)
        {
            transform.Translate(Vector3.left * Time.deltaTime * speed);
        }
    }

    private void CheckOutOfBounds()
    {
        if (gameObject.transform.position.x < leftBound /*&& gameObject.CompareTag("Obstacle")*/)
        {
            Destroy(gameObject);
        }
    }
}
