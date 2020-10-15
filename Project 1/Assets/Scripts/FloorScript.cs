using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorScript : MonoBehaviour
{
    public float destroyTimer = 3.0f;

    public float tileSpeed = 5.0f;

    private Transform trans;

    // Start is called before the first frame update
    void Start()
    {
        trans = GetComponent<Transform>();

        Destroy(gameObject, destroyTimer);
    }

    void Update()
    {
        if (trans.position.y < 0)
        {
            trans.Translate(Vector3.up * tileSpeed, Space.World);
            if (trans.position.y > 0)
                trans.position = new Vector3(0.0f, 0.0f, trans.position.z);
        }
    }
}
