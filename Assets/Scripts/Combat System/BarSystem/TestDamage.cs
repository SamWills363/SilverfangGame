using UnityEngine;

public class TestDamage : MonoBehaviour
{
    [Header("Bar System Reference")]
    public BarSystem barSystem; // Reference to the BarSystem to test.

    [Header("Damage and Heal Settings")]
    public float damageAmount = 10f; // Amount of damage to apply.
    public float healAmount = 5f; // Amount of healing to apply.

    private void Update()
    {
        // Apply damage when the left mouse button is clicked.
        if (Input.GetMouseButtonDown(0)) // Left mouse button
        {
            if (barSystem != null)
            {
                barSystem.DecreaseValue(damageAmount);
                Debug.Log($"Dealt {damageAmount} damage. Current Value: {barSystem.CurrentValue}");
            }
            else
            {
                Debug.LogError("BarSystem reference is missing.");
            }
        }

        // Apply healing when the right mouse button is clicked.
        if (Input.GetMouseButtonDown(1)) // Right mouse button
        {
            if (barSystem != null)
            {
                barSystem.IncreaseValue(healAmount);
                Debug.Log($"Healed {healAmount}. Current Value: {barSystem.CurrentValue}");
            }
            else
            {
                Debug.LogError("BarSystem reference is missing.");
            }
        }
    }
}
