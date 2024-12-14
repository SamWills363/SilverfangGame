using UnityEngine;

public class Parallaxer : MonoBehaviour
{
    [Header("Parallax Toggles")]
    public bool parallaxX = true;
    public bool parallaxY = true;
    public bool parallaxZ = false;

    [Header("Parallax Speeds")]
    public float speedX = -0.5f;
    public float speedY = -0.5f;
    public float speedZ = 0.0f;

    private Vector3 initialPosition;
    private Transform cameraTransform;

    void Start()
    {
        // Store the initial position of the GameObject
        initialPosition = transform.position;

        // Get the main camera's transform
        cameraTransform = Camera.main.transform;
    }

    void Update()
    {
        // Calculate the parallax effect for each axis
        Vector3 cameraDelta = cameraTransform.position - initialPosition;
        Vector3 newPosition = initialPosition;

        if (parallaxX)
        {
            newPosition.x += cameraDelta.x * speedX;
        }

        if (parallaxY)
        {
            newPosition.y += cameraDelta.y * speedY;
        }

        if (parallaxZ)
        {
            newPosition.z += cameraDelta.z * speedZ;
        }

        // Apply the new position to the GameObject
        transform.position = newPosition;
    }
}
