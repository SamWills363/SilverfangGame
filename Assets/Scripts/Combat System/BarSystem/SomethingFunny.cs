using UnityEngine;

public class SomethingFunny : MonoBehaviour
{
    [Header("Health Bar Reference")]
    public BarSystem healthBar; // Reference to the BarSystem for health.

    private Rigidbody targetRigidbody;

    private void Start()
    {
        // Try to find a Rigidbody on the same GameObject this script is attached to.
        targetRigidbody = GetComponent<Rigidbody>();

        if (targetRigidbody == null)
        {
            Debug.LogError($"No Rigidbody found on {gameObject.name}. This script requires a Rigidbody!");
        }
    }

    private void Update()
    {
        if (healthBar != null && targetRigidbody != null)
        {
            if (healthBar.CurrentValue <= 0 && targetRigidbody.isKinematic)
            {
                targetRigidbody.isKinematic = false;
                Debug.Log("Health reached 0! Rigidbody is now non-kinematic!");
            }
        }
        else if (healthBar == null)
        {
            Debug.LogError("HealthBar reference is missing.");
        }
    }
}
