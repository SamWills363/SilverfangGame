using UnityEngine;

[CreateAssetMenu(fileName = "NewCharacter", menuName = "Character/CharacterProfile")]
public class CharacterSO : ScriptableObject
{
    public string characterName;

    // Core stats similar to D&D
    public int strength;
    public int dexterity;
    public int constitution;
    public int intelligence;
    public int wisdom;
    public int charisma;

    // Additional attributes for gameplay
    public float speed; // Determines gauge fill rate
    public float atbGauge; // Current gauge value
    public bool isReady => atbGauge >= 100f; // Ready to act when gauge reaches 100
}
