using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [Header("Inventory Data")]
    public List<ItemSO> items = new List<ItemSO>();
    public List<SkillSO> skills = new List<SkillSO>();
    public List<AttributeSO> attributes = new List<AttributeSO>();

    public void AddItem(ItemSO newItem)
    {
        items.Add(newItem);
        Debug.Log($"Added item: {newItem.itemName}");
    }

    public void AddSkill(SkillSO newSkill)
    {
        skills.Add(newSkill);
        Debug.Log($"Learned skill: {newSkill.itemName}");
    }

    public void AddAttribute(AttributeSO newAttribute)
    {
        attributes.Add(newAttribute);
        Debug.Log($"Gained attribute: {newAttribute.itemName}");
    }

    public void UseInventoryItem(InventoryItemSO item)
    {
        item.Use();
    }

    public void UseSkill(CharacterSO character, SkillSO skill)
    {
        if (character.isReady && skill.type == SkillSO.SkillType.Active)
        {
            Debug.Log($"{character.characterName} uses {skill.itemName}");
            character.atbGauge = 0; // Reset gauge
        }
    }

    public void EquipWeapon(WeaponSO weapon)
    {
        Debug.Log($"Equipping weapon: {weapon.itemName}");
        weapon.Use();
    }

}
