using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] obstaclePrefab;
    private Vector3 spawnPosition = new Vector3(25, 0, 0);
    private float startTime = 2.0f;
    private float delayTime = 1.5f;
    private float randomDelayTime;
    private PlayerController playerController;

    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        randomDelayTime = Random.Range(1.5f, 2.0f);
        InvokeRepeating("SpawnObstacles", startTime, randomDelayTime);
    }

    private void SpawnObstacles()
    {
        int index = Random.Range(0, obstaclePrefab.Length);

        if(!playerController.gameOver)
        {
            Instantiate(obstaclePrefab[index], spawnPosition, obstaclePrefab[index].transform.rotation);
        }
    }
}