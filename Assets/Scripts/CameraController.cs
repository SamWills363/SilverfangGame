using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;
    public float leadOffsetMultiplier = 0.5f;
    public float smoothSpeed = 0.125f;
    public Vector2 defaultOffset = new Vector2(0, 5);

    public float zoomSpeed = 2f;           // Speed of zoom in/out
    public float minZoom = 5f;            // Minimum zoom level
    public float maxZoom = 15f;           // Maximum zoom level

    private Camera cam;                   // Reference to the Camera component
    private Vector3 targetPosition;

    void Start()
    {
        // Get the Camera component attached to this object
        cam = GetComponent<Camera>();
    }

    void FixedUpdate()
    {
        // Camera follow logic
        Vector3 offset = defaultOffset + (Vector2)player.up * leadOffsetMultiplier * player.GetComponent<Rigidbody>().linearVelocity.magnitude;
        targetPosition = player.position + new Vector3(offset.x, offset.y, -10);

        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed);

        // Handle zoom input
        HandleZoom();
    }

    void HandleZoom()
    {
        // Get scroll input from the mouse
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        if (scroll != 0)
        {
            // Adjust the orthographic size based on scroll input
            cam.orthographicSize -= scroll * zoomSpeed;
            cam.orthographicSize = Mathf.Clamp(cam.orthographicSize, minZoom, maxZoom);
        }
    }
}
