using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Tilemap))]
public class TilemapTo3DCollider : MonoBehaviour
{
    public BoxCollider boxCollider; // Assign the proxy BoxCollider in the inspector
    public float tileSize = 1f;     // Size of each tile (1 unit by default)
    public float floorHeight = 0.1f; // Thin height for the collider in the Y-axis

    void Awake()
    {
        UpdateBoxCollider(); // Initialize the collider when the object awakens
    }

    // Function to update the BoxCollider
    public void UpdateBoxCollider()
    {
        if (boxCollider != null)
        {
            Tilemap tilemap = GetComponent<Tilemap>();

            // Get the cell bounds of the tilemap
            BoundsInt bounds = tilemap.cellBounds;

            // Calculate size based on bounds, keeping Y (height) thin
            Vector3 boxSize = new Vector3(bounds.size.x * tileSize, floorHeight, bounds.size.y * tileSize);

            // Calculate center, aligned to the X-Z plane
            Vector3 boxCenter = new Vector3(
                bounds.min.x * tileSize + boxSize.x / 2f,
                -floorHeight / 2f, // Place the floor at Y=0 level
                bounds.min.y * tileSize + boxSize.z / 2f
            );

            // Apply to BoxCollider
            boxCollider.size = boxSize;
            boxCollider.center = boxCenter - transform.position; // Adjust for Tilemap's transform offset
        }
    }
}
