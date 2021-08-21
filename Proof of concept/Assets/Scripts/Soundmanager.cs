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
    private bool musicVolumeChanging;
    private float newMusicVolume;

    #endregion

    private void Awake()
    {

        Instance = this;

        //créer les audiosources
        musicSource = this.gameObject.AddComponent<AudioSource>();
        musicSource2 = this.gameObject.AddComponent<AudioSource>();
        sfxSource = this.gameObject.AddComponent<AudioSource>();

        Debug.Log(musicSource);
        // loop les musiques
        musicSource.loop = true;
        musicSource2.loop = true;

        musicVolumeChanging = false;

    }

    public void Update()
    {
        if (musicVolumeChanging == true)
        {
            musicSource.volume = Mathf.Lerp(musicSource.volume, newMusicVolume, 0.5f * Time.deltaTime);

            if (Mathf.Abs(musicSource.volume - newMusicVolume) < 0.075f)
            {
                musicSource.volume = newMusicVolume;
                musicVolumeChanging = false;
            }
        }
    }

    public void PlayMusic(AudioClip musicClip,float volume)
    {
        //Debug.Log(musicClip);
        //Debug.Log(musicSource);
        musicSource.clip = musicClip;
        musicSource.volume = volume;
        musicSource.Play();

    }
    public void StopMusic()
    {
        musicSource.Stop();

    }
    public void PlayMusic2(AudioClip musicClip, float volume)
    {
        musicSource2.clip = musicClip;
        musicSource2.volume = volume;
        musicSource2.Play();

    }

    public void PlaySFX(AudioClip clip, float volume)
    {
        sfxSource.PlayOneShot(clip, volume);

    }

    public void ChangeVolume (float newVolume)
    {
        if (musicVolumeChanging == false)
        {

            musicVolumeChanging = true;

            newMusicVolume = newVolume;
        }
    }

}
