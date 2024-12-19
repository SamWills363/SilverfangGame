using UnityEngine;

[CreateAssetMenu(fileName = "NewSkill", menuName = "Inventory/Skill")]
public class SkillSO : InventoryItemSO
{
    public enum SkillType { Active, Passive }
    [Header("Skill Specific Properties")]
    public SkillType type;
    public float cooldown;

    public override void Use()
    {
        Debug.Log($"Casting skill: {itemName} (Type: {type}, Cooldown: {cooldown}s)");
    }
}
