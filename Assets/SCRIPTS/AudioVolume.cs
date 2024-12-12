using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioVolume : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;

    public void Start(){
        if(PlayerPrefs.HasKey("musicVolume")){
            LoadVolumeMusic();
        }
        if(PlayerPrefs.HasKey("sfxVolume")){
            LoadVolumeSFX();
        }
    }


    public void SetMusicVolume()
    {
        float musicVolume = Mathf.Clamp(musicSlider.value, 0.01f, 1); // Ensure non-zero values
        float musicDB = Mathf.Log10(musicVolume) * 20;
        audioMixer.SetFloat("Music", musicDB);
        PlayerPrefs.SetFloat("musicVolume", musicDB);
        PlayerPrefs.SetFloat("musicSlider", musicVolume);
        PlayerPrefs.Save();
    }

    public void SetSFXVolume()
    {
        float sfxVolume = Mathf.Clamp(sfxSlider.value, 0.01f, 1); // Ensure non-zero values
        float sfxDB = Mathf.Log10(sfxVolume) * 20;
        audioMixer.SetFloat("SFX", sfxDB);
        PlayerPrefs.SetFloat("sfxVolume", sfxDB); // Corrected key
        PlayerPrefs.SetFloat("sfxSlider", sfxVolume);
        PlayerPrefs.Save();
    }
    private void LoadVolumeMusic (){
        
        audioMixer.SetFloat("Music", PlayerPrefs.GetFloat("musicVolume"));

        musicSlider.value = PlayerPrefs.GetFloat("musicSlider");
    }

    private void LoadVolumeSFX(){
        
        audioMixer.SetFloat("SFX", PlayerPrefs.GetFloat("sfxVolume"));

        sfxSlider.value = PlayerPrefs.GetFloat("sfxSlider");
    }
}
