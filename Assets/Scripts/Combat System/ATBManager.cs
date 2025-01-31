using System.Collections.Generic;
using UnityEngine;

public class ATBManager : MonoBehaviour
{
    public List<CharacterCore> characters = new List<CharacterCore>();
    public float globalTimeMultiplier = 1f;

    private void Update()
    {
        foreach (var character in characters)
        {
            if (!character.IsReady())
            {
                character.IncreaseATB(globalTimeMultiplier * Time.deltaTime);
            }
            else if (!character.HasActed())
            {
                ExecuteAction(character);
            }
        }
    }

    public void ExecuteAction(CharacterCore character)
    {
        Debug.Log($"{character.characterData.characterName} is ready to act!");
        character.SetReadyState(true);
    }
}
