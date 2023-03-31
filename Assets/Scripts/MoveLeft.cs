using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    private float speed = 15;
    private PlayerController playerControllerScript;
    private float leftBound = -15;
    // Start is called before the first frame update
    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerControllerScript.dashMode)
        {
            speed = 30;
        }
        else
        {
            speed = 15;
        }
        //Debug.Log(playerControllerScript.gameOver);
        if (!playerControllerScript.gameOver)
        {
            transform.Translate(Vector3.left * Time.deltaTime * speed);
        }
        if (gameObject.transform.position.x < leftBound /*&& gameObject.CompareTag("Obstacle")*/)
        {
            Destroy(gameObject);
        }
    }
}
