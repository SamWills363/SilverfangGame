using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Inventory/Item")]
public class ItemSO : InventoryItemSO
{
    [Header("Item Specific Properties")]
    public int quantity;

    public override void Use()
    {
        Debug.Log($"{itemName} used! Remaining: {--quantity}");
    }
}
