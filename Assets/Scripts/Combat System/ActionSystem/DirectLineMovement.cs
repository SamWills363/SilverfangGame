using UnityEngine;

public class DirectLineMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 10f; // Speed of horizontal movement
    public float jumpHeight = 5f; // Height of the jump
    public float gravity = 9.81f; // Gravity to pull the GameObject down
    public bool enableGravity = true; // Toggle gravity on/off
    
    private Vector3 targetPosition;
    private bool isMoving = false;
    private bool isJumping = false;
    private float verticalVelocity = 0f; // Vertical velocity for jumping and falling

    void Update()
    {
        if (isMoving)
        {
            MoveToTarget();
        }

        if (enableGravity)
        {
            ApplyGravity();
        }
    }

    /// <summary>
    /// Starts moving the GameObject towards the target position.
    /// </summary>
    /// <param name="position">The target position to move towards, including jump height.</param>
    public void StartMovement(Vector3 position)
    {
        targetPosition = position;

        if (position.y > transform.position.y)
        {
            StartJump(position.y);
        }

        isMoving = true;
    }

    /// <summary>
    /// Handles the actual movement logic.
    /// </summary>
    private void MoveToTarget()
    {
        // Move horizontally (XZ plane)
        Vector3 horizontalTarget = new Vector3(targetPosition.x, transform.position.y, targetPosition.z);
        transform.position = Vector3.MoveTowards(transform.position, horizontalTarget, moveSpeed * Time.deltaTime);

        // Stop horizontal movement when close enough
        if (Vector3.Distance(transform.position, horizontalTarget) <= 0.1f)
        {
            isMoving = false;
        }
    }

    /// <summary>
    /// Handles jumping logic.
    /// </summary>
    /// <param name="targetY">The target height for the jump.</param>
    private void StartJump(float targetY)
    {
        if (!isJumping)
        {
            isJumping = true;
            verticalVelocity = Mathf.Sqrt(2 * gravity * (targetY - transform.position.y)); // Calculate initial jump velocity
        }
    }

    /// <summary>
    /// Applies gravity and adjusts vertical position.
    /// </summary>
    private void ApplyGravity()
    {
        if (isJumping || transform.position.y > targetPosition.y)
        {
            verticalVelocity -= gravity * Time.deltaTime; // Apply gravity
            transform.position += new Vector3(0, verticalVelocity * Time.deltaTime, 0);

            // Stop jumping when reaching the ground or target Y position
            if (transform.position.y <= targetPosition.y)
            {
                transform.position = new Vector3(transform.position.x, targetPosition.y, transform.position.z);
                verticalVelocity = 0f;
                isJumping = false;
            }
        }
    }
}
