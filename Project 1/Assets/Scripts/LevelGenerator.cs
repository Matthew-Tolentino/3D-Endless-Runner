﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public Transform playerPos;

    public GameManager manager;

    private GameObject obs;

    [Header("Interactable Level Objects")]
    public GameObject floor;

    public GameObject coin;

    public GameObject obstacle1;

    public GameObject obstacle2;

    public GameObject obstacle3;

    [Header("Visual Level Objects")]
    public GameObject redFlower;

    [Header("floor Spawning Variables")]
    public float spawnPlatformsAhead = 3.0f;

    [Range(0, 5)]
    public int Vegitation;

    private float floorSpawnDistance;

    private Vector3 floorSpawnPos;

    private Vector3 floorLastSpawn;

    private float floorSpawnOffset;

    [Header("Obstacle Spawning Variables")]
    public float distanceFromPlayer = 35f;

    public float obstacleSpawnBuffer = 10f;

    private Vector3 obstacleSpawnPos;

    private Vector3 obstacleLastSpawn;
    
    // Start is called before the first frame update
    void Start()
    {
        floorSpawnOffset = floor.GetComponent<Transform>().localScale.z;
        floorSpawnDistance = floorSpawnOffset * spawnPlatformsAhead;
        floorSpawnPos = Vector3.zero;
        SpawnFloor();

        obstacleLastSpawn.z = playerPos.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        // Floor Spawning
        Debug.Log(floorSpawnPos.z - playerPos.position.z + " : " + floorSpawnOffset * spawnPlatformsAhead);
        if (floorSpawnPos.z - playerPos.position.z < floorSpawnOffset * spawnPlatformsAhead)
            SpawnFloor();

        // Obstacle Spwaning
        if (playerPos.position.z >= obstacleLastSpawn.z - obstacleSpawnBuffer)
        {
            SpawnObstacle();
            obstacleLastSpawn.z = obstacleLastSpawn.z + distanceFromPlayer;
        }
    }

    void SpawnFloor()
    {
        Instantiate(floor, floorSpawnPos, Quaternion.identity);
        floorLastSpawn = floorSpawnPos;
        floorSpawnPos = new Vector3(floorSpawnPos.x, floorSpawnPos.y - 3.0f, floorSpawnPos.z + floorSpawnOffset);
        SpawnVegitation();
    }

    void SpawnObstacle()
    {
        float pick = Random.Range(0.0f, 3.0f);
        //GameObject obs = null;
        if (pick <= 1.0f)
        {
            obstacleSpawnPos = new Vector3(playerPos.position.x, 0.75f, obstacleLastSpawn.z + distanceFromPlayer);
            obs = Instantiate(obstacle1, obstacleSpawnPos, Quaternion.identity);
            //manager.obstacles.Add(obs);
        }
        else if (pick <= 2.0f)
        {
            obstacleSpawnPos = new Vector3(playerPos.position.x, 1.75f, obstacleLastSpawn.z + distanceFromPlayer);
            obs = Instantiate(obstacle2, obstacleSpawnPos, Quaternion.identity);
            //manager.obstacles.Add(obs);
        }
        else if (pick <= 3.0f)
        {
            obstacleSpawnPos = new Vector3(playerPos.position.x, 4.25f, obstacleLastSpawn.z + distanceFromPlayer);
            obs = Instantiate(obstacle3, obstacleSpawnPos, Quaternion.identity);
            //manager.obstacles.Add(obs);
        }
        manager.obstacles.Add(obs);
        SpawnCoin();
    }

    void SpawnCoin()
    {
        Vector3 coinPosition = new Vector3(obstacleSpawnPos.x, 1.50f, obstacleSpawnPos.z - (distanceFromPlayer / 2));
        //Quaternion rot = new Quaternion(coin.transform.rotation.x, 0f, 0f);
        Instantiate(coin, coinPosition, coin.transform.rotation);
    }

    // Pick side of track to spawn on, get position based on spawning platform, add random scale
    void SpawnVegitation()
    {
        for (int i = 0; i < Vegitation; i++)
        {
            float posX = 0;
            float side = Random.Range(0.0f, 1.0f);
            if (side < .5)  // left side
                posX = Random.Range(-2.0f + floorLastSpawn.x, -1.2f + floorLastSpawn.x);
            else            // right side
                posX = Random.Range(1.5f + floorLastSpawn.x, 3.3f + floorLastSpawn.x);
            float posZ = Random.Range(-6.0f + floorLastSpawn.z, 5.6f + floorLastSpawn.z);

            Vector3 plantPos = new Vector3(posX, .25f, posZ);
            GameObject flower = Instantiate(redFlower, plantPos, Quaternion.identity);
            float size = Random.Range(0.3f, 1.0f);
            flower.transform.localScale = new Vector3(size, size, size);
        }
    }
}
