using UnityEngine;
using UnityEditor;

public class DistanceDebugger : MonoBehaviour
{
    [Header("Target to draw line & arrow to")]
    public Transform target;

    [Header("Gizmo Line Settings")]
    public Color lineColor = Color.red;
    public Color arrowColor = Color.yellow;
    public float arrowSize = 0.5f;
    public bool showDistance = true;
    public bool showArrow = true;

    private void OnDrawGizmos()
    {
        if (target == null) return;

        // Draw line
        Gizmos.color = lineColor;
        Gizmos.DrawLine(transform.position, target.position);

        // Midpoint for arrow placement
        Vector3 midPoint = (transform.position + target.position) / 2;
        Vector3 direction = (target.position - transform.position).normalized;

        if (showArrow)
        {
            // Draw an arrow using Handles
            Handles.color = arrowColor;
            Handles.ArrowHandleCap(0, midPoint, Quaternion.LookRotation(direction), arrowSize, EventType.Repaint);
        }

        if (showDistance)
        {
            // Display distance text
            Handles.color = Color.white;
            Handles.Label(midPoint + Vector3.up * 0.5f, $"Distance: {Vector3.Distance(transform.position, target.position):F2}");
        }
    }
}
