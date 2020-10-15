using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
    public float rotationSpeed = 5.0f;

    public float hoverSpeed = 1.0f;

    public float maxHeightChange = 1.0f;

    public float minHeightChange = 1.0f;

    private bool up;

    private float startYPos;

    void Start()
    {
        startYPos = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);

        if (transform.position.y > startYPos + maxHeightChange)
            up = false;
        if (transform.position.y < startYPos - maxHeightChange)
            up = true;

        if (up)
            transform.Translate(Vector3.up * hoverSpeed * Time.deltaTime, Space.World);
        else
            transform.Translate(Vector3.down * hoverSpeed * Time.deltaTime, Space.World);
    }
}
