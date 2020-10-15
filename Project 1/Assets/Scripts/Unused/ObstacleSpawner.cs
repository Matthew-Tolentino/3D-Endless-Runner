using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject player;

    private Transform playerPos;

    private Vector3 spawnPos;

    [Header("Obstacle Variables")]
    private Vector3 lastSpawnPos;

    public float distanceFromPlayer = 25f;

    // TODO: Make obstacle height a variable
    [Header("Obstacle Types")]
    public GameObject obstacle1;

    public GameObject obstacle2;

    public GameObject obstacle3;

    // Start is called before the first frame update
    void Start()
    {
        playerPos = player.GetComponent<Transform>();

        lastSpawnPos.z = playerPos.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerPos.position.z >= lastSpawnPos.z)
        {
            SpawnObstacle();
            lastSpawnPos.z = playerPos.position.z + distanceFromPlayer;
        }
    }

    void SpawnObstacle()
    {
        float pick = Random.Range(0.0f, 3.0f);
        if (pick <= 1.0f)
        {
            spawnPos = new Vector3(playerPos.position.x, 0.75f, playerPos.position.z + distanceFromPlayer);
            Instantiate(obstacle1, spawnPos, Quaternion.identity);
        }
        else if (pick <= 2.0f)
        {
            spawnPos = new Vector3(playerPos.position.x, 1.75f, playerPos.position.z + distanceFromPlayer);
            Instantiate(obstacle2, spawnPos, Quaternion.identity);
        }
        else if (pick <= 3.0f)
        {
            spawnPos = new Vector3(playerPos.position.x, 4.25f, playerPos.position.z + distanceFromPlayer);
            Instantiate(obstacle3, spawnPos, Quaternion.identity);
        }
    }
}
