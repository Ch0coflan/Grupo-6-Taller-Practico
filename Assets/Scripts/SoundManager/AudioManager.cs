using System;

using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public Sound[] musicSounds, sfxSounds;
    public AudioSource musicSource, sfxSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayMusic(string nameMusic)
    {
        Sound s = Array.Find(musicSounds, x => x.name == nameMusic);

        if (s == null)
        {
            Debug.Log($"Sound not Found {nameMusic}");
        }
        else
        {
            musicSource.clip = s.clip;
            musicSource.Play();
        }
    }

    public void PlaySfx(string nameSfx)
    {
        Sound s = Array.Find(sfxSounds, x => x.name == nameSfx);

        if (s == null)
        {
            Debug.Log($"Sound not Found {nameSfx}");
        }
        else
        {
            sfxSource.PlayOneShot(s.clip);
        }
    }
    
    //UI SOUND CONTROLS

    public void ToggleMaster()
    {
        musicSource.mute = !musicSource.mute;
        sfxSource.mute = !sfxSource.mute;
    }
    
    public void ToggleMusic()
    {
        musicSource.mute = !musicSource.mute;
    }

    public void ToggleSfx()
    {
        sfxSource.mute = !sfxSource.mute;
    }
}
