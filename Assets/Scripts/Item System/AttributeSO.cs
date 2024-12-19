using UnityEngine;

[CreateAssetMenu(fileName = "NewAttribute", menuName = "Inventory/Attribute")]
public class AttributeSO : InventoryItemSO
{
    public enum AttributeType { Stat, Trait, Condition }
    [Header("Attribute Specific Properties")]
    public AttributeType type;
    public float value;

    public override void Use()
    {
        Debug.Log($"Applying attribute: {itemName} with value {value}");
    }
}
