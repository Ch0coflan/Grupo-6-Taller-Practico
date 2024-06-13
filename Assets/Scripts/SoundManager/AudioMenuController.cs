using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;


public class AudioMenuController : MonoBehaviour
{
    [SerializeField] private AudioMixer myMixer;
    [SerializeField] public Slider _musicSlider;
    [SerializeField] public Slider _sfxSlider;
    [SerializeField] public Slider _masterSlider;
    [SerializeField] public Slider _musicButton;
    [SerializeField] public Slider _sfxButton;
    [SerializeField] public Slider _masterButton;

    private void Start()
    {
        if (PlayerPrefs.HasKey("musicVolume") && PlayerPrefs.HasKey("sfxVolume"))
        {
            LoadVolume();
        }
        else
        {
            SetMusicVolume();
            SetSfxVolume();
            SetMasterVolume();
        }
    }

    private void LoadVolume()
    {
        _musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
        _sfxSlider.value = PlayerPrefs.GetFloat("sfxVolume");
        _masterSlider.value = PlayerPrefs.GetFloat("masterVolume");
        SetMusicVolume();
        SetSfxVolume();
        SetMasterVolume();
        
    }

    public void ToggleMusic()
    {
        AudioManager.Instance.ToggleMusic();
    }
    
    public void ToggleMaster()
    {
        AudioManager.Instance.ToggleMaster();
    }
    
    public void ToggleSfx()
    {
        AudioManager.Instance.ToggleSfx();
    }

    public void SetMusicVolume()
    {
        float volume = _musicSlider.value;
        myMixer.SetFloat("music", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("musicVolume", volume);
    }
    
    public void SetMasterVolume()
    {
        float volume = _masterSlider.value;
        myMixer.SetFloat("master", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("masterVolume", volume);
    }
    
    public void SetSfxVolume()
    {
        float volume = _sfxSlider.value;
        myMixer.SetFloat("sfx", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("sfxVolume", volume);
    }


}
