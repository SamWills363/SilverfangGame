using UnityEngine;
using UnityEngine.UI;

public class InventorySlotUI : MonoBehaviour
{
    public InventoryItemSO linkedItem;
    public Image icon;

    public void SetupSlot(InventoryItemSO item)
    {
        linkedItem = item;
        icon.sprite = item.icon;
    }

    public void OnSlotClicked()
    {
        if (linkedItem != null)
        {
            Debug.Log($"Selected: {linkedItem.itemName}");
            linkedItem.Use();
        }
    }
}
