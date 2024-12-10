using UnityEngine;

public class AudioSettings : MonoBehaviour
{
    public AudioSource musicSource;
    public AudioSource sfxSource;

    public AudioClip background;
    public AudioClip buttonNoise;

    private void Start(){
    musicSource.clip = background;
    musicSource.Play();
    }
    public void PlaySfx(AudioClip clip){
        sfxSource.PlayOneShot(clip);
    }

}

