using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    private Animator playerAnim;
    private AudioSource playerAudio;
    public ParticleSystem explosionParticle;
    public ParticleSystem dirtParticle;
    public AudioClip jumpSound;
    public AudioClip crashSound;
    public float jumpForce;
    public float gravityModifier;
    public bool isOnGround = true;
    private bool doubleJump = true;
    public bool gameOver = false;
    private int score;
    public bool dashMode = false;
    private int scoreStep = 1;

    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        Physics.gravity *= gravityModifier;
        gameOver = false;
        score = 0;
    }

    void Update()
    {
        if (!gameOver)
        {
            score += scoreStep;
            Debug.Log("Score: " + score);

            if (Input.GetKeyDown(KeyCode.Space) && (isOnGround || doubleJump))
            {
                playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                if (!isOnGround) // means it's a double jump
                {
                    doubleJump = false;
                    playerAnim.Play("Running_Jump"); // animation of second jump must start immediately
                }
                else
                {
                    isOnGround = false;
                    playerAnim.SetTrigger("Jump_trig");
                    dirtParticle.Stop();
                }
                playerAudio.PlayOneShot(jumpSound);
            }

            if (Input.GetKey(KeyCode.LeftShift))
            {
                dashMode = true;
                playerAnim.speed = 1.5f;
                scoreStep = 2;
            }
            else
            {
                dashMode = false;
                playerAnim.speed = 1;
                scoreStep = 1;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground")) {
            isOnGround = true;
            doubleJump = true;
            if (!gameOver)
                dirtParticle.Play();
        } else if (collision.gameObject.CompareTag("Obstacle") && !gameOver) { // second clause makes sure we can't die twice
            Debug.Log("Game Over");
            Debug.Log("Score: " + score);
            gameOver = true;
            playerAnim.SetBool("Death_b", true);
            playerAnim.SetInteger("DeathType_int", 1);
            explosionParticle.Play();
            dirtParticle.Stop();
            playerAudio.PlayOneShot(crashSound);
        }
    }
}
