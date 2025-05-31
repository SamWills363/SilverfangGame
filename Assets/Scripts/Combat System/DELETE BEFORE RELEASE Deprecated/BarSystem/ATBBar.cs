using UnityEngine;
using UnityEngine.UI;

public class ATBBar : MonoBehaviour
{
    public Slider slider;
    public CharacterSO character;

    private void Update()
    {
        slider.value = character.atbGauge / 100f;
    }
}
