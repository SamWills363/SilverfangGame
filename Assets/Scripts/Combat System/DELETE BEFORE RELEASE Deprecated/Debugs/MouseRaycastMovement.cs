using UnityEngine;

public class MouseRaycastMovement : MonoBehaviour
{
    [Header("References")]
    public DirectLineMovement movementScript; // Reference to the DirectLineMovement script

    [Header("Raycast Settings")]
    public LayerMask raycastLayer; // Define which layers should be hit by the raycast

    void Update()
    {
        // Check for left mouse button input
        if (Input.GetMouseButtonDown(0))
        {
            PerformRaycast();
        }
    }

    /// <summary>
    /// Casts a ray from the mouse position to detect a collision point in the 3D space.
    /// </summary>
    private void PerformRaycast()
    {
        // Cast a ray from the camera to the mouse position
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity, raycastLayer))
        {
            // Get the point of collision
            Vector3 targetPoint = hitInfo.point;

            // Log the target position for debugging
            Debug.Log($"Target position: {targetPoint}");

            // Call the movement script with the detected point
            movementScript.StartMovement(targetPoint);
        }
    }
}
