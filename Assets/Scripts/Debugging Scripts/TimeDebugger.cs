using UnityEngine;
using UnityEditor;

public class TimeDebugger : MonoBehaviour
{
    [Header("Scene Timer Settings")]
    public Color textColor = Color.green;
    public int fontSize = 14;
    public Vector2 screenPosition = new Vector2(10, 10); // X, Y position on Scene View

    private float sceneStartTime;
    private float playTime;

    private void Awake()
    {
        sceneStartTime = Time.unscaledTime; // Track when the scene starts
    }

    private void Update()
    {
        playTime = Time.unscaledTime - sceneStartTime; // Calculate time elapsed
    }

    private void OnDrawGizmos()
    {
        if (!Application.isPlaying) return; // Only show in Play Mode

        Handles.BeginGUI();
        GUIStyle style = new GUIStyle
        {
            fontSize = fontSize,
            normal = new GUIStyleState { textColor = textColor }
        };

        GUI.Label(new Rect(screenPosition.x, screenPosition.y, 200, 50), $"Scene Time: {playTime:F2} s", style);
        Handles.EndGUI();
    }
}
