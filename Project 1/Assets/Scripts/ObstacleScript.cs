using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleScript : MonoBehaviour
{
    public float destroyTimer = 3f;

    public void SelfDestruct()
    {
        Destroy(gameObject, destroyTimer);
    }
}
