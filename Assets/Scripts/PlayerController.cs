using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveForceGround = 5f;   // Ground Speed Force
    public float moveForceAir = 2f;      // Air Speed Force
    public float jumpForce = 3.5f;         // Jump impulse

    public Transform startPoint;
    public string floorTag = "Floor";

    private Rigidbody rb;
    private Vector3 moveInput;
    private bool jumpInput;
    private bool isGrounded;
    // private bool hasFinished = false; // For Full Stop at the finish

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // continous
        // Move input 
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        moveInput = new Vector3(h, 0, v).normalized;

        // Jump input
        if (Input.GetKeyDown(KeyCode.Space))
            jumpInput = true;
    }

    void FixedUpdate()
    {
        // Full Stopping if the monitor is hit
        // if (hasFinished) return;

        // Applying horizontal movement
        float currentForce = isGrounded ? moveForceGround : moveForceAir;
        Vector3 horizontalForce = new Vector3(moveInput.x, 0, moveInput.z) * currentForce;
        rb.AddForce(horizontalForce, ForceMode.Force);

        // Jump impulse
        if (jumpInput && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
        jumpInput = false;

        // Smooth deceleration only when grounded AND no input
        if (isGrounded && moveInput.magnitude < 0.1f)
        {
            Vector3 horizontalVel = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            horizontalVel *= 0.9f; // smooth stop
            rb.velocity = new Vector3(horizontalVel.x, rb.velocity.y, horizontalVel.z);
        }

        // Checking falling
        if (transform.position.y < -10f)
        {
            RestartPlayer();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Reset player if floor is touched
        if (collision.gameObject.CompareTag(floorTag))
        {
            RestartPlayer();
        }
        // Reset player if drone is touched
        if (collision.gameObject.CompareTag("Drone"))
        {
            RestartPlayer();
        }

        isGrounded = true;
    }

    // Flags when we touch the ground and when we don't
    private void OnCollisionStay(Collision collision)
    {
        isGrounded = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        isGrounded = false;
    }

    public void RestartPlayer()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        transform.position = startPoint.position;
    }

    // Finish Logic
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Finish"))
        {
            FinishGame();
        }
    }

    void FinishGame()
    {
        // Full Stop
        // hasFinished = true;
        // rb.velocity = Vector3.zero;
        // rb.angularVelocity = Vector3.zero;

        Debug.Log("Congrats! You reached the end and hit the hellish monitor!");
    }
}