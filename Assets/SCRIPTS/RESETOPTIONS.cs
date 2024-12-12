using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class RESETOPTIONS: MonoBehaviour
{

    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;
    public TMP_Dropdown FullscreenDropdown;
    public TMP_Dropdown ResolutionDropdown;


    public void PURGE(){
        PlayerPrefs.SetFloat("musicVolume", 0f);
        PlayerPrefs.SetFloat("sfxVolume", 0f);

        PlayerPrefs.SetFloat("musicSlider", 1f);
        PlayerPrefs.SetFloat("sfxSlider", 1f);

        PlayerPrefs.SetInt("resolutionIndex", 1);
        PlayerPrefs.SetInt("fullscreenIndex", 0);

        sfxSlider.value = 1f;
        musicSlider.value = 1f;

        audioMixer.SetFloat("Music", 0f);
        audioMixer.SetFloat("SFX", 0f);

        ResolutionDropdown.value = 1;
        FullscreenDropdown.value = 0;

        Screen.SetResolution(1280, 720, FullScreenMode.FullScreenWindow);
    }
}