using UnityEngine;
using UnityEditor;

public class DebugMomentumGizmo : MonoBehaviour
{
    [Header("Momentum Gizmo Settings")]
    public Color lineColor = Color.cyan;
    public float lineScale = 1.0f;
    public bool showMomentum = true;

    private Rigidbody rb;
    private Vector3 lastPosition;
    private Vector3 currentVelocity;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        lastPosition = transform.position;
    }

    private void Update()
    {
        if (!Application.isPlaying || !showMomentum) return;

        // Manually calculate velocity even when Time.timeScale = 0
        currentVelocity = (transform.position - lastPosition) / Time.unscaledDeltaTime;
        lastPosition = transform.position;
    }

    private void OnDrawGizmos()
    {
        if (!Application.isPlaying || !showMomentum) return; // Only draw in Play Mode

        Vector3 momentum = currentVelocity * (rb ? rb.mass : 1);
        if (momentum == Vector3.zero) return; // Prevents "Look rotation viewing vector is zero" error

        Vector3 start = transform.position;
        Vector3 end = start + momentum * lineScale;

        Gizmos.color = lineColor;
        Gizmos.DrawLine(start, end);

        // Draw an arrow only if momentum is nonzero
        Handles.color = lineColor;
        Handles.ArrowHandleCap(0, end, Quaternion.LookRotation(momentum), 0.3f * lineScale, EventType.Repaint);
    }
}
