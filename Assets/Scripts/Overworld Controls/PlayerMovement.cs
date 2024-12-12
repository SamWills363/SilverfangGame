using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public LayerMask groundLayer;
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;

    private Rigidbody rb;
    private Animator animator;

    private Vector3 movement;
    private bool isGrounded;
    private float lastMoveX = 0f; // Tracks the last non-zero X movement
    private float lastMoveZ = 0f; // Tracks the last non-zero Z movement

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        // Movement Input
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");
        movement = new Vector3(moveX, 0f, moveZ).normalized;

        // Ground Check
        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundLayer);

        // Update last movement direction
        if (movement.magnitude > 0)
        {
            lastMoveX = moveX;
            lastMoveZ = moveZ;
        }

        // Animator Parameters
        animator.SetBool("isWalking", movement.magnitude > 0);
        animator.SetBool("isJumping", !isGrounded);
        animator.SetFloat("moveX", moveX);
        animator.SetFloat("moveZ", moveZ);

        // Set Idle direction when movement stops
        if (movement.magnitude == 0)
        {
            animator.SetFloat("idleX", lastMoveX);
            animator.SetFloat("idleZ", lastMoveZ);
        }

        // Jump Input
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            rb.AddForce(Vector3.up * 5f, ForceMode.Impulse);
        }
    }

    void FixedUpdate()
    {
        // Apply Movement
        if (movement.magnitude > 0)
        {
            rb.MovePosition(rb.position + movement.normalized * moveSpeed * Time.fixedDeltaTime);
        }
    }
}
