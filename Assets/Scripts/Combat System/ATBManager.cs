using System.Collections.Generic;
using UnityEngine;

public class ATBManager : MonoBehaviour
{
    public List<CharacterSO> characters = new List<CharacterSO>();
    public float globalTimeMultiplier = 1f;

    private void Update()
    {
        foreach (var character in characters)
        {
            if (!character.isReady)
            {
                character.atbGauge += character.speed * globalTimeMultiplier * Time.deltaTime;
                character.atbGauge = Mathf.Clamp(character.atbGauge, 0f, 100f);
            }
        }
    }

    public void ExecuteAction(CharacterSO character, string action)
    {
        if (character.isReady)
        {
            Debug.Log($"{character.characterName} executes {action}");
            character.atbGauge = 0; // Reset the gauge
        }
    }
}
