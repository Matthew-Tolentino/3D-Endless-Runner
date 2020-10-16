using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GFXController : MonoBehaviour
{
    public Material originalColor;

    public Material jumpColor;

    public Transform playerTrans;

    public Renderer rend;

    private PlayerController playerCont;

    [Header("Jump Variabels")]
    private bool oddRotation = true;

    public bool jump, spinMove = false;

    public float spinSpeed = 900f;

    [Header("Teleport Variables")]
    public int scalePhase = 0;

    public float scaleRate = 0.1f;

    public bool tele = false;

    private Vector3 scaleChange;
    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();

        playerCont = GameObject.Find("Player").GetComponent<PlayerController>();

        scaleChange = new Vector3(scaleRate, scaleRate, scaleRate);
    }

    // Update is called once per frame
    void Update()
    {
        if (tele)
        {
            switch (scalePhase)
            {
                case 0:
                    ScaleDown();
                    break;
                case 1:
                    playerCont.Teleport();
                    break;
                case 2:
                    ScaleUp();
                    break;
            }
        }

        if (spinMove)
        {
            Spin();
        }
    }

    void ScaleDown()
    {
        transform.localScale -= scaleChange;

        if (transform.localScale.x <= 0)
            scalePhase = 1;
    }

    void ScaleUp()
    {
        transform.localScale += scaleChange;

        if (transform.localScale.x >= 1)
        {
            scalePhase = 0;
            tele = false;
        }
    }

    void Spin()
    {
        transform.Rotate(0, spinSpeed * Time.deltaTime, 0);
        if (transform.localRotation.y <= 0 && oddRotation)
        {
            rend.material = originalColor;
            oddRotation = !oddRotation;
            transform.localRotation = Quaternion.Euler(transform.localRotation.x, 0, transform.localRotation.z);
            spinMove = false;
        }
        else if (transform.localRotation.y <= 0 && !oddRotation)
        {
            rend.material = originalColor;
            oddRotation = !oddRotation;
            transform.localRotation = Quaternion.Euler(transform.localRotation.x, 0, transform.localRotation.z);
            spinMove = false;
        }
    }
}
