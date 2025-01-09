using UnityEngine;

[CreateAssetMenu(fileName = "NewCharacter", menuName = "Combat/Character")]
public class CharacterSO : ScriptableObject
{
    [Header("Character Info")]
    public string characterName;

    [Header("Core Stats (D&D-like)")]
    [Tooltip("Strength determines physical power and athletics.")]
    public int strength;
    [Tooltip("Dexterity determines agility, acrobatics, and stealth.")]
    public int dexterity;
    [Tooltip("Constitution determines stamina and durability.")]
    public int constitution;
    [Tooltip("Intelligence determines knowledge and reasoning.")]
    public int intelligence;
    [Tooltip("Wisdom determines perception and insight.")]
    public int wisdom;
    [Tooltip("Charisma determines social skills and influence.")]
    public int charisma;

    [Header("Gameplay Attributes")]
    [Tooltip("The speed at which the character's ATB gauge fills.")]
    public float speed; // Determines gauge fill rate
    [Tooltip("The current state of the character's ATB gauge.")]
    public float atbGauge; // Current gauge value
    [Tooltip("The maximum health points of the character.")]
    public float maxHP; // Maximum Health Points

    [Header("Status Flags")]
    [Tooltip("Indicates if the character is ready to act.")]
    public bool isReady => atbGauge >= 100f; // Ready to act when gauge reaches 100
}
