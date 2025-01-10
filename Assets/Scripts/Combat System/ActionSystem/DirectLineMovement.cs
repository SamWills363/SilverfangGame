using UnityEngine;

public class DirectLineMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 10f; // Speed of horizontal movement
    public float jumpHeight = 5f; // Height of the jump
    public float gravity = 9.81f; // Gravity to pull the GameObject down
    public bool enableGravity = true; // Toggle gravity on/off

    [Header("Target Object")]
    public GameObject targetObject; // The GameObject to move

    private Vector3 targetPosition;
    private bool isMoving = false;
    private bool isJumping = false;
    private float verticalVelocity = 0f; // Vertical velocity for jumping and falling

    private bool hasMoved = false;

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
        if (hasMoved || targetObject == null) {
            return;
        }

        targetPosition = position;
        
        if (position.y > targetObject.transform.position.y)
        {
            StartJump(position.y);
        }
    }

    /// <summary>
    /// Handles the actual movement logic.
    /// </summary>
    private void MoveToTarget()
    {
        if (hasMoved || targetObject == null) {
            hasMoved = false;
            return;
        }

        // Move horizontally (XZ plane)
        Vector3 horizontalTarget = new Vector3(targetPosition.x, targetObject.transform.position.y, targetPosition.z);
        targetObject.transform.position = Vector3.MoveTowards(targetObject.transform.position, horizontalTarget, moveSpeed * Time.deltaTime);

        Debug.Log("Unit is moving to " + targetObject.transform.position);
        // Stop horizontal movement when close enough
        if (Vector3.Distance(targetObject.transform.position, horizontalTarget) <= 0.1f)
        {
            isMoving = false;
            hasMoved = true;
        }
        return;
    }

    /// <summary>
    /// Handles jumping logic.
    /// </summary>
    /// <param name="targetY">The target height for the jump.</param>
    private void StartJump(float targetY)
    {
        if (!isJumping && targetObject != null)
        {
            isJumping = true;
            verticalVelocity = Mathf.Sqrt(2 * gravity * (targetY - targetObject.transform.position.y)); // Calculate initial jump velocity
        }
    }

    /// <summary>
    /// Applies gravity and adjusts vertical position.
    /// </summary>
    private void ApplyGravity()
    {
        if (targetObject == null) return;

        if (isJumping || targetObject.transform.position.y > targetPosition.y)
        {
            verticalVelocity -= gravity * Time.deltaTime; // Apply gravity
            targetObject.transform.position += new Vector3(0, verticalVelocity * Time.deltaTime, 0);

            // Stop jumping when reaching the ground or target Y position
            if (targetObject.transform.position.y <= targetPosition.y)
            {
                targetObject.transform.position = new Vector3(targetObject.transform.position.x, targetPosition.y, targetObject.transform.position.z);
                verticalVelocity = 0f;
                isJumping = false;
            }
        }
    }
}
