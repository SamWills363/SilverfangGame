using UnityEngine;

[CreateAssetMenu(fileName = "NewWeapon", menuName = "Inventory/Weapon")]
public class WeaponSO : InventoryItemSO
{
    [Header("Weapon Properties")]
    [Range(1, 1000)] public int value; // The value of the weapon
    public Vector2 damageRange; // Min and max potential damage
    public bool hasCustomSkills; // Whether the weapon has custom skills
    public SkillSO customSkill; // Custom skill (optional)
    public float attackSpeed; // Determines how fast the weapon can attack
    public float weight; // Affects character mobility or stamina drain
    

    public override void Use()
    {
        Debug.Log($"{itemName} equipped! Damage: {damageRange.x}-{damageRange.y}, Attack Speed: {attackSpeed}, Weight: {weight}");
        if (hasCustomSkills && customSkill != null)
        {
            Debug.Log($"Custom Skill Available: {customSkill.itemName}");
        }
    }
}
