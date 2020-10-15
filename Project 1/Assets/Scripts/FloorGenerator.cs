using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorGenerator : MonoBehaviour
{
    public Transform playerPos;

    public GameObject floor;

    [Header("Spawning Variables")]
    //public float spawnRate = 2.0f;

    public float spawnPlatformsAhead = 3.0f;

    private float spawnDistance;

    private Vector3 spawnPos;

    private Vector3 lastSpawn;

    private float spawnOffset;

    // Start is called before the first frame update
    void Start()
    {
        spawnOffset = floor.GetComponent<Transform>().localScale.z;
        spawnDistance = spawnOffset * spawnPlatformsAhead;
        spawnPos = Vector3.zero;
        SpawnFloor();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("distance: " + (spawnPos.z - playerPos.position.z) + " threshold: " + spawnOffset * 3);
        if (spawnPos.z - playerPos.position.z < spawnOffset * spawnPlatformsAhead)
        {
            SpawnFloor();
        }
    }

    void SpawnFloor()
    {
        Instantiate(floor, spawnPos, Quaternion.identity);
        lastSpawn = spawnPos;
        spawnPos = new Vector3(spawnPos.x, spawnPos.y - 3.0f, spawnPos.z + spawnOffset);
    }
}
