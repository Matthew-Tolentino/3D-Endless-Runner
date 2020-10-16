using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;

    //private Renderer rend;

    private GFXController GFX;

    //public Camera cam;

    //public Material jumpColor;

    //public Material originalColor;

    // Movement
    [Header("Movement")]
    public float moveSpeed = .5f;

    public float maxSpeed = 3f;

    public float horizontalMove = 0f;

    // Jump Variables
    [Header("Jump Variables")]
    private bool jump = false;

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

        GFX = GameObject.Find("GFX").GetComponent<GFXController>();

        jumpsLeft = jumpTimes;
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal");
        jump = Input.GetKeyDown(KeyCode.W);
        
        // Teleport
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GFX.tele = true;
        }

        // Jump Character
        if (jump && jumpsLeft > 0)
        {
            //rb.velocity = Vector3.up * jumpForce;
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            jumpsLeft--;
            if (jumpsLeft == 0)
            {
                GFX.spinMove = true;
                GFX.rend.material = GFX.jumpColor;
            }
            jump = false;
        }

        // Check to see if player fell
        if (transform.position.y < -5)
            dead = true;
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

   public void Teleport()
    {
        Vector3 newPos = new Vector3(transform.position.x, transform.position.y, transform.position.z + teleDist);

        transform.position = newPos;

        GFX.scalePhase = 2;
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
