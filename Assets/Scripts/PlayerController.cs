using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRB;
    private Animator playerAnim;
    private AudioSource playerAudio;
    
    public ParticleSystem explosionParticle;
    public ParticleSystem splatterParticle;

    public AudioClip explosionSound;
    public AudioClip jumpSound;

    public float jumpForce = 10.0f;
    public float gravityModify = 2;
    public bool isOnGround = true;
    public bool isDoubleJump = false;
    public bool gameOver = false;

    void Start()
    {
        playerRB = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        Physics.gravity *= gravityModify;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && !gameOver && (isOnGround || !isDoubleJump))
        {
            if(isOnGround)
            {
                isOnGround = false;
                jumpForce = 10.0f;
            }
            else if(!isDoubleJump)
            {
                isDoubleJump = true;
                jumpForce = 10.0f;
            }
            playerRB.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            playerAnim.SetTrigger("Jump_trig");
            splatterParticle.Stop();
            playerAudio.PlayOneShot(jumpSound);
        }

        if(Input.GetKeyDown(KeyCode.Escape) && gameOver)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

    }

    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            isDoubleJump = false;
            splatterParticle.Play();
        } else if(other.gameObject.CompareTag("Obstacle"))
        {
            int deathAnim = Random.Range(1, 3);
            playerAnim.SetInteger("DeathType_int", deathAnim);
            playerAnim.SetBool("Death_b", true);
            explosionParticle.Play();
            splatterParticle.Stop();
            playerAudio.PlayOneShot(explosionSound, 1.0f);
            gameOver = true;
        }
        
    }
}