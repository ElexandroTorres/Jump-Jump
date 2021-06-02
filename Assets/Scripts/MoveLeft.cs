using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    public float speed = 10.0f;
    private float leftLimit = -10.0f;
    private PlayerController playerController;

    void Start() 
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();    
    }

    void Update()
    {
        if(playerController.gameOver == false)
        {
            transform.Translate(Vector3.left * Time.deltaTime * speed);
        }
        if(this.gameObject.CompareTag("Obstacle"))
        {
            if(transform.position.x < leftLimit)
            {
                Destroy(this.gameObject);
            }
        }
    }
}