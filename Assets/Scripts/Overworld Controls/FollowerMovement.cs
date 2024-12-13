using UnityEngine;

public class FollowerMovement : MonoBehaviour
{
    public Transform target; // The leader to follow
    public float followSpeed = 4f; // Speed for following the target
    public float stopDistance = 1f; // Minimum distance to stop
    public float followDelay = 0.2f; // Delay for smoother following
    public LayerMask groundLayer; // Layer for ground detection
    public Transform groundCheck; // Check for ground
    public float groundCheckRadius = 0.2f; // Radius for ground detection
    public float movementThreshold = 0.01f; // Minimum movement to consider the follower "moving"

    private Rigidbody rb;
    private Animator animator;

    private Vector3 idleDirection = Vector3.zero; // Last movement direction for idling
    private Vector3 lastPosition; // Tracks the last position to calculate movement
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
        lastPosition = transform.position; // Initialize last position
    }

    void Update()
    {
        // Check if grounded
        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundLayer);

        // Update Animator parameters for idle direction
        if (!animator.GetBool("isWalking"))
        {
            animator.SetFloat("idleX", idleDirection.x);
            animator.SetFloat("idleZ", idleDirection.z);
        }
    }

    void FixedUpdate()
    {
        // Follow the leader
        FollowTarget();

        // Detect movement
        Vector3 positionDelta = transform.position - lastPosition;
        if (positionDelta.magnitude > movementThreshold)
        {
            animator.SetBool("isWalking", true); // Follower is moving
        }
        else
        {
            animator.SetBool("isWalking", false); // Follower is idle
        }

        // Update last position
        lastPosition = transform.position;
    }

    private void FollowTarget()
    {
        Vector3 direction = target.position - transform.position;

        if (direction.magnitude > stopDistance)
        {
            // Move towards the leader
            Vector3 move = direction.normalized * followSpeed * Time.fixedDeltaTime;
            rb.MovePosition(rb.position + move);

            // Update Animator parameters for movement
            animator.SetFloat("moveX", direction.normalized.x);
            animator.SetFloat("moveZ", direction.normalized.z);

            // Update idle direction based on movement
            idleDirection = direction.normalized;
        }
        else
        {
            // Stop movement and reset Animator parameters
            animator.SetFloat("moveX", 0);
            animator.SetFloat("moveZ", 0);
        }
    }

    void OnDrawGizmos()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}
