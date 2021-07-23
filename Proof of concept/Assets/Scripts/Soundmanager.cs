using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soundmanager : MonoBehaviour
{
    #region static instance
    public static Soundmanager Instance;

    #endregion

    #region AudioSources
    private AudioSource musicSource;
    private AudioSource musicSource2;
    private AudioSource sfxSource;

    #endregion

    private void Awake()
    {

        DontDestroyOnLoad(this.gameObject);
        Instance = this;

        //créer les audiosources
        musicSource = this.gameObject.AddComponent<AudioSource>();
        musicSource2 = this.gameObject.AddComponent<AudioSource>();
        sfxSource = this.gameObject.AddComponent<AudioSource>();

        Debug.Log(musicSource);
        // loop les musiques
        musicSource.loop = true;
        musicSource2.loop = true;



    }

    public void PlayMusic(AudioClip musicClip,float volume)
    {
        Debug.Log(musicClip);
        Debug.Log(musicSource);
        musicSource.clip = musicClip;
        musicSource.volume = volume;
        musicSource.Play();

    }
    public void stopMusic()
    {
        musicSource.Stop();

    }
    public void PlayMusic2(AudioClip musicClip, float volume)
    {
        musicSource2.clip = musicClip;
        musicSource.volume = volume;
        musicSource2.Play();

    }

    public void PlaySFX(AudioClip clip, float volume)
    {
        sfxSource.PlayOneShot(clip, volume);

    }

}
