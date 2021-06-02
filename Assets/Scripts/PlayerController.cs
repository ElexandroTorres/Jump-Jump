﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRB;
    private Animator playerAnim;
    private AudioSource playerAudio;
    
    public ParticleSystem explosionParticle;
    public ParticleSystem splatterParticle;

    public AudioClip explosionSound;
    public AudioClip jumpSound;

    public float jumpForce = 10;
    public float gravityModify = 2;
    public bool isOnGround = true;
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
        if(Input.GetKeyDown(KeyCode.Space) && isOnGround && !gameOver)
        {
            playerRB.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
            playerAnim.SetTrigger("Jump_trig");
            splatterParticle.Stop();
            playerAudio.PlayOneShot(jumpSound);
        }
    }

    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
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