using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioVolume : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;

    public void start(){
        if(PlayerPrefs.HasKey("musicVolume")){
            LoadVolumeMusic();
        }
        if(PlayerPrefs.HasKey("sfxvolume")){
            LoadVolumeSFX();
        }
    }


    public void SetMusicVolume(){
        float musicvolume = musicSlider.value;
        float mscvlm = Mathf.Log10(musicvolume)*20;
        audioMixer.SetFloat("Music", mscvlm);
        PlayerPrefs.SetFloat("musicVolume", mscvlm);
        PlayerPrefs.Save();
    }
    public void SetSFXVolume(){
        float sfxvolume = sfxSlider.value;
        float sfxvlm = Mathf.Log10(sfxvolume)*20;
        audioMixer.SetFloat("SFX", sfxvlm);
        PlayerPrefs.SetFloat("sfxvolume", sfxvlm);
        PlayerPrefs.Save();
    }

    private void LoadVolumeMusic(){
        audioMixer.SetFloat("Music", PlayerPrefs.GetFloat("musicVolume")); 
    }

    private void LoadVolumeSFX(){
        audioMixer.SetFloat("Music", PlayerPrefs.GetFloat("musicVolume"));
    }
}
