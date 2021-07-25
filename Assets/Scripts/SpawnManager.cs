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

    public Vector3 AnimEndPoint = new Vector3(0, 0, 0);

    public float animationSpeed = 1.0f;

    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        randomDelayTime = Random.Range(1.5f, 2.0f);
        InvokeRepeating("SpawnObstacles", startTime, randomDelayTime);

        //playerController.gameOver = true;
        //StartCoroutine(PlayIntro());

    }

    private void SpawnObstacles()
    {
        int index = Random.Range(0, obstaclePrefab.Length);

        if(!playerController.gameOver)
        {
            Instantiate(obstaclePrefab[index], spawnPosition, obstaclePrefab[index].transform.rotation);
        }
    }

    IEnumerator PlayIntro()
    {
        Vector3 startPosition = playerController.transform.position;

        float startAnimTime = Time.time;
        float journeyLength = Vector3.Distance(startPosition, AnimEndPoint);

        float distanceCovered = 0;
        //float distanceCovered = (Time.time - startAnimTime) * animationSpeed;
        float fractionOfJorney = animationSpeed / journeyLength;

        while(distanceCovered < 1)
        {
            distanceCovered = (Time.time - startAnimTime) * animationSpeed;
            fractionOfJorney = animationSpeed / journeyLength;
            playerController.transform.position = Vector3.Lerp(startPosition, AnimEndPoint, fractionOfJorney);
            yield return null;
        }

        //playerController.gameOver = false;
    }
}