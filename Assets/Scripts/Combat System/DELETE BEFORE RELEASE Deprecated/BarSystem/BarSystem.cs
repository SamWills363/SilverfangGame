using UnityEngine;

public class BarSystem : MonoBehaviour
{
    [Header("Bar Properties")]
    [SerializeField] private AttributeSO attributeSO; // The AttributeSO asset to initialize the bar.
    [SerializeField] private float maxValue = 100f; // The maximum value of the bar.
    [SerializeField] private float currentValue = 100f; // The current value of the bar.

    /// <summary>
    /// Gets the current value of the bar.
    /// </summary>
    public float CurrentValue => currentValue;

    /// <summary>
    /// Gets the maximum value of the bar.
    /// </summary>
    public float MaxValue => maxValue;

    private void Awake()
    {
        if (attributeSO != null)
        {
            // Initialize values based on the AttributeSO.
            maxValue = attributeSO.value;
            currentValue = attributeSO.value;
        }
        UpdateBarVisual();
    }

    /// <summary>
    /// Increases the bar's current value by a specified amount.
    /// </summary>
    /// <param name="amount">The amount to increase.</param>
    public void IncreaseValue(float amount)
    {
        currentValue += amount;
        currentValue = Mathf.Clamp(currentValue, 0, maxValue); // Ensure it doesn't exceed maxValue.
        UpdateBarVisual();
    }

    /// <summary>
    /// Decreases the bar's current value by a specified amount.
    /// </summary>
    /// <param name="amount">The amount to decrease.</param>
    public void DecreaseValue(float amount)
    {
        currentValue -= amount;
        currentValue = Mathf.Clamp(currentValue, 0, maxValue); // Ensure it doesn't fall below 0.
        UpdateBarVisual();
    }

    /// <summary>
    /// Sets the current value to a specific value.
    /// </summary>
    /// <param name="value">The value to set.</param>
    public void SetValue(float value)
    {
        currentValue = Mathf.Clamp(value, 0, maxValue);
        UpdateBarVisual();
    }

    /// <summary>
    /// Updates the visual representation of the bar.
    /// Override this method to customize how the bar is displayed.
    /// </summary>
    protected virtual void UpdateBarVisual()
    {
        // Override in derived classes to update the bar's visuals (e.g., scaling, fill amount).
    }

    /// <summary>
    /// Resets the bar to its maximum value.
    /// </summary>
    public void ResetToMax()
    {
        currentValue = maxValue;
        UpdateBarVisual();
    }
}
