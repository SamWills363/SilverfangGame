using UnityEngine;
using UnityEngine.UI;

public class HealthBar : BarSystem
{
    [Header("UI Components")]
    public RectTransform fillRect;
    public RectTransform backgroundRect;

    protected override void UpdateBarVisual()
    {
        if (fillRect != null && backgroundRect != null)
        {
            float fillPercentage = CurrentValue / MaxValue;
            fillRect.sizeDelta = new Vector2(backgroundRect.sizeDelta.x * fillPercentage, fillRect.sizeDelta.y);
        }
    }
}
