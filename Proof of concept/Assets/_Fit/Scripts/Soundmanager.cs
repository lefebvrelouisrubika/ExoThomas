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
    private AudioSource musicSource3;
    private AudioSource sfxSource;
    private bool musicVolumeChanging;
    private float newMusicVolume;
    private float newMusicVolume2;
    private float newMusicVolume3;
    public float lerpSpeed = 0.5f;
    public bool isInScene = false;
    public AudioClip musicMenu;
    public AudioClip musicPlayNormal;
    public AudioClip musicPlayWrong;


    #endregion

    private void Awake()
    {

        Instance = this;

        //creer les audiosources
        musicSource = this.gameObject.AddComponent<AudioSource>();
        musicSource2 = this.gameObject.AddComponent<AudioSource>();
        musicSource3 = this.gameObject.AddComponent<AudioSource>();
        sfxSource = this.gameObject.AddComponent<AudioSource>();

        Debug.Log(musicSource);
        // loop les musiques
        musicSource.loop = true;
        musicSource2.loop = true;
        musicSource3.loop = true;

        musicVolumeChanging = false;


        if (isInScene == true)
        {
            PlayMusic2(musicPlayNormal, 0.5f);
            PlayMusic3(musicPlayWrong, 0.001f);
        }
        else
        {
            PlayMusic(musicPlayNormal, 0.5f);
        }
    }

    public void Update()
    {
        if (Mathf.Abs(musicSource.volume - newMusicVolume) > 0.01)
        {
            musicSource.volume = Mathf.Lerp(musicSource.volume, newMusicVolume, Time.deltaTime * lerpSpeed);
        }
        if (Mathf.Abs(musicSource2.volume - newMusicVolume2) > 0.01)
        {
            musicSource2.volume = Mathf.Lerp(musicSource2.volume, newMusicVolume2, Time.deltaTime * lerpSpeed);
        }
        if (Mathf.Abs(musicSource3.volume - newMusicVolume3) > 0.01)
        {
            musicSource3.volume = Mathf.Lerp(musicSource3.volume, newMusicVolume3, Time.deltaTime * lerpSpeed);
        }

        //Debug.Log(musicSource.volume);
    }

    public void PlayMusic(AudioClip musicClip,float volume)
    {
        //Debug.Log(musicClip);
        //Debug.Log(musicSource);
        //Debug.Log(volume);
        musicSource.clip = musicClip;
        newMusicVolume = volume;
        musicSource.Play();
        //Debug.Log("Play");

    }

    public void StopMusic()
    {
        musicSource.Stop();

    }
    public void PlayMusic2(AudioClip musicClip, float volume)
    {
        musicSource2.clip = musicClip;
        newMusicVolume2 = volume;
        musicSource2.Play();


    }
    public void StopMusic2()
    {
        musicSource2.Stop();

    }
    public void PlayMusic3(AudioClip musicClip, float volume)
    {
        musicSource3.clip = musicClip;
        musicSource3.volume = volume;
        newMusicVolume3 = volume;
        musicSource3.Play();


    }
    public void StopMusic3()
    {
        musicSource3.Stop();

    }

    public void PlaySFX(AudioClip clip, float volume)
    {
        sfxSource.PlayOneShot(clip, volume);

    }

    public void ChangeVolume1 (float newVolume)
    {
        
            newMusicVolume = newVolume; 
    }
    public void ChangeVolume2(float newVolume)
    {

        newMusicVolume2 = newVolume;
    }
    public void ChangeVolume3(float newVolume)
    {

        newMusicVolume3 = newVolume;
    }
}
