using UnityEngine;

public abstract class InventoryItemSO : ScriptableObject
{
    [Header("General Information")]
    public string itemName;
    public string description;
    public Sprite icon;

    public abstract void Use(); // Abstract method for polymorphic behavior
}
