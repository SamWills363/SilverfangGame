using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioVolume : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider musicSlider;

    public void SetMusicVolume(){
        float volume = musicSlider.value;
        audioMixer.SetFloat("Music", Mathf.Log10(volume)*20);
    }
    public void SetSFXVolume(){
        float volume = musicSlider.value;
        audioMixer.SetFloat("SFX", Mathf.Log10(volume)*20);
    }
}
