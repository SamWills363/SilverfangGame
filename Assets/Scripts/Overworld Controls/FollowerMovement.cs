using UnityEngine;

public class FollowerMovement : MonoBehaviour
{
    public Transform target; // The player to follow
    public float followSpeed = 4f; // Speed for following the target
    public float stopDistance = 1f; // Distance to stop moving
    public float jumpDelayFactor = 0.5f; // Factor to calculate delay based on speed

    public Animator animator;
    public SpriteRenderer spriteRenderer;

    public LayerMask groundLayer; // LayerMask to detect the ground
    public Transform groundCheck; // Position to check if grounded
    public float groundCheckRadius = 0.2f; // Radius of the ground check

    private Rigidbody rb;
    private bool isGrounded;
    private bool jumpTriggered = false; // To manage delayed jumping

    private Vector3 lastTargetPosition;
    private float targetSpeed;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        lastTargetPosition = target.position;
    }

    void Update()
    {
        // Update target's speed
        targetSpeed = (target.position - lastTargetPosition).magnitude / Time.deltaTime;
        lastTargetPosition = target.position;

        // Ground check
        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundLayer);

        // Follow the target
        FollowTarget();

        // Handle jumping
        if (jumpTriggered && isGrounded)
        {
            PerformJump();
        }
    }

    private void FollowTarget()
    {
        Vector3 direction = target.position - transform.position;
        direction.y = 0; // Ignore height differences for movement

        if (direction.magnitude > stopDistance)
        {
            Vector3 move = direction.normalized * followSpeed;
            rb.linearVelocity = new Vector3(move.x, rb.linearVelocity.y, move.z);

            // Update animation
            UpdateAnimation(move);
        }
        else
        {
            rb.linearVelocity = new Vector3(0, rb.linearVelocity.y, 0);
            animator.SetFloat("Speed", 0);
        }
    }

    public void OnLeaderJump(Vector3 jumpStartPosition)
    {
        Debug.Log($"Follower notified of leader's jump at position {jumpStartPosition}");

        if (!jumpTriggered && isGrounded) // Prevent duplicate jump triggers
        {
            float jumpDelay = jumpDelayFactor / Mathf.Max(targetSpeed, 1f);
            Debug.Log($"Jump delay calculated: {jumpDelay}");
            Invoke(nameof(TriggerJump), jumpDelay);
        }
    }

    private void TriggerJump()
    {
        Debug.Log("Follower preparing to jump!");
        jumpTriggered = true;
    }

    private void PerformJump()
    {
        Debug.Log("Follower is jumping!");
        rb.AddForce(Vector3.up * 7f, ForceMode.Impulse); // Adjust force as needed
        jumpTriggered = false;

        // Play jump animation
        animator.Play("Jump");
    }

    private void UpdateAnimation(Vector3 move)
    {
        animator.SetFloat("Speed", move.magnitude);

        if (move.x > 0)
        {
            animator.Play("WalkRight");
            spriteRenderer.flipX = false;
        }
        else if (move.x < 0)
        {
            animator.Play("WalkLeft");
            spriteRenderer.flipX = true;
        }
        else if (move.z > 0)
        {
            animator.Play("WalkUp");
        }
        else if (move.z < 0)
        {
            animator.Play("WalkDown");
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
