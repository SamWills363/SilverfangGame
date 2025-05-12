using UnityEngine;

public class Hurtbox : MonoBehaviour
{
    private CharacterCore characterCore;

    private void Awake()
    {
        characterCore = GetComponentInParent<CharacterCore>();

        if (characterCore == null)
        {
            Debug.LogError($"Hurtbox on {gameObject.name} couldn't find a CharacterCore!");
        }
    }

    public void TakeDamage(int damage)
    {
        if (characterCore != null)
        {
            characterCore.TakeDamage(damage);
            Debug.Log($"{gameObject.name} took {damage} damage! Remaining HP: {characterCore.Health}");
        }
    }
}
