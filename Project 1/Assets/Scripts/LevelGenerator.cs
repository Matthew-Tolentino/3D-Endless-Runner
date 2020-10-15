using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public Transform playerPos;

    [Header("Level Objects")]
    public GameObject floor;

    public GameObject coin;

    public GameObject obstacle1;

    public GameObject obstacle2;

    public GameObject obstacle3;

    [Header("floor Spawning Variables")]
    public float spawnPlatformsAhead = 3.0f;

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
    }

    void SpawnObstacle()
    {
        float pick = Random.Range(0.0f, 3.0f);
        if (pick <= 1.0f)
        {
            obstacleSpawnPos = new Vector3(playerPos.position.x, 0.75f, obstacleLastSpawn.z + distanceFromPlayer);
            Instantiate(obstacle1, obstacleSpawnPos, Quaternion.identity);
        }
        else if (pick <= 2.0f)
        {
            obstacleSpawnPos = new Vector3(playerPos.position.x, 1.75f, obstacleLastSpawn.z + distanceFromPlayer);
            Instantiate(obstacle2, obstacleSpawnPos, Quaternion.identity);
        }
        else if (pick <= 3.0f)
        {
            obstacleSpawnPos = new Vector3(playerPos.position.x, 4.25f, obstacleLastSpawn.z + distanceFromPlayer);
            Instantiate(obstacle3, obstacleSpawnPos, Quaternion.identity);
        }
        SpawnCoin();
    }

    void SpawnCoin()
    {
        Vector3 coinPosition = new Vector3(obstacleSpawnPos.x, 1.50f, obstacleSpawnPos.z - (distanceFromPlayer / 2));
        //Quaternion rot = new Quaternion(coin.transform.rotation.x, 0f, 0f);
        Instantiate(coin, coinPosition, coin.transform.rotation);
    }
}
