using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;

    private Renderer rend;

    public Camera cam;

    public Material jumpColor;

    public Material originalColor;

    // Movement
    [Header("Movement")]
    public float moveSpeed = .5f;

    public float maxSpeed = 3f;

    public float horizontalMove = 0f;

    // Jump Variables
    [Header("Jump Variables")]
    private bool oddRotation = true;

    private bool jump, spinMove = false;

    public int jumpTimes = 2;

    public int jumpsLeft;

    public float jumpForce = 3f;

    public float spinSpeed = 20f;

    // Tele Variables
    [Header("Teleport Variables")]
    public float teleDist = 5f;

    public bool tele = false;

    public int scalePhase = 0;

    public float scaleRate = .1f;

    private Vector3 scaleChange;

    // Camera position veriables
    public GameObject camPos;

    // Coin variables
    public Text coinTxt;

    private int coinsCollected;

    public bool dead = false;

    // Start is called before the first frame update
    void Start()
    { 
        rb = GetComponent<Rigidbody>();

        rend = GetComponent<Renderer>();

        jumpsLeft = jumpTimes;

        scaleChange = new Vector3(scaleRate, scaleRate, scaleRate);
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal");
        jump = Input.GetKeyDown(KeyCode.W);
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            tele = true;
            cam.transform.parent = null;
        }

        // Jump Character
        if (jump && jumpsLeft > 0)
        {
            //rb.velocity = Vector3.up * jumpForce;
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            jumpsLeft--;
            if (jumpsLeft == 0)
            {
                spinMove = true;
                rend.material = jumpColor;
                cam.transform.parent = null;
            }
            jump = false;
        }

        if (tele)
        {
            if (scalePhase == 0)
                ScaleDown();
            if (scalePhase == 1)
                Teleport();
            if (scalePhase == 2)
                ScaleUp();
        }

        if (spinMove)
        {
            Spin();
        }
    }

    // Physics here
    void FixedUpdate()
    {
        // Move Character
        if (horizontalMove == 1)
        {
            rb.AddForce(Vector3.forward * moveSpeed, ForceMode.Impulse);
        }
        if (horizontalMove == -1)
        {
            rb.AddForce(Vector3.back * moveSpeed, ForceMode.Impulse);
        }

        // Increase Gravity on player
        rb.AddForce(Physics.gravity * rb.mass);

        // Cap Movementspeed
        if (rb.velocity.magnitude > maxSpeed)
            rb.velocity = rb.velocity.normalized * maxSpeed;
    }

    void OnCollisionEnter(Collision obj)
    {
        if (obj.gameObject.CompareTag("Ground"))
        {
            jumpsLeft = jumpTimes;
        }

        if (obj.gameObject.CompareTag("Obstacle"))
        {
            dead = true;
        }
    }

    void OnTriggerEnter(Collider obj)
    {
        if (obj.gameObject.CompareTag("Coin"))
        {
            coinTxt.text = "Coins: " + ++coinsCollected;
            Destroy(obj.gameObject);
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
            if (!spinMove) // Check to see if spinning
                SetCam();
        }
    }

    void Teleport()
    {
        //TURN OFF GRAVITY
        Vector3 newPos = new Vector3(transform.position.x, transform.position.y, transform.position.z + teleDist);

        transform.position = newPos;

        scalePhase = 2;
    }

    void Spin()
    {
        transform.Rotate(0, spinSpeed * Time.deltaTime, 0);
        if (transform.localRotation.y <= 0 && oddRotation)
        {
            rend.material = originalColor;
            oddRotation = !oddRotation;
            transform.localRotation = Quaternion.Euler(transform.localRotation.x, 0, transform.localRotation.z);
            if (!tele) 
                SetCam();
            spinMove = false;
        }
        else if (transform.localRotation.y <= 0 && !oddRotation)
        {
            rend.material = originalColor;
            oddRotation = !oddRotation;
            transform.localRotation = Quaternion.Euler(transform.localRotation.x, 0, transform.localRotation.z);
            if (!tele)
                SetCam();
            spinMove = false;
        }

    }

    void SetCam()
    {
        cam.transform.position = camPos.transform.position;
        cam.transform.parent = transform;
    }

    // Save and Load
    public void Save()
    {
        Debug.Log("Saved Player");
        PlayerPrefs.SetFloat("player-velocity-x", rb.velocity.x);
        PlayerPrefs.SetFloat("player-velocity-y", rb.velocity.y);
        PlayerPrefs.SetFloat("player-velocity-z", rb.velocity.z);
    }

    public void Load()
    {
        Debug.Log("Loaded Player!");
        float vel_x = PlayerPrefs.GetFloat("player-velocity-x", 0.0f);
        float vel_y = PlayerPrefs.GetFloat("player-velocity-y", 0.0f);
        float vel_z = PlayerPrefs.GetFloat("player-velocity-z", 0.0f);
        rb.velocity = new Vector3(vel_x, vel_y, vel_z);
    }
}
