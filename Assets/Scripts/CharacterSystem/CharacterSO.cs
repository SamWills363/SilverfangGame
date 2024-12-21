using UnityEngine;

[CreateAssetMenu(fileName = "NewCharacter", menuName = "Combat/Character")]
public class CharacterSO : ScriptableObject
{
    public string characterName;
    public float speed; // Determines gauge fill rate
    public float atbGauge; // Current gauge value
    public bool isReady => atbGauge >= 100f; // Ready to act when gauge reaches 100
}
