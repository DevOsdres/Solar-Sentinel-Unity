using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class VolumeSettings : MonoBehaviour
{
    [SerializeField] private AudioMixer myMixer;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;

    private void Start() 
    {
        LoadVolume();
    }

    public void SetMusicVolume()
    {
        float volume = musicSlider.value;
        if (volume == 0) 
        {
            myMixer.SetFloat("MusicVolume", -80f); // Silencio total
        }
        else
        {
            myMixer.SetFloat("MusicVolume", Mathf.Log10(volume) * 20f);
        }
        PlayerPrefs.SetFloat("musicVolume", volume);
    }

    public void SetSFXVolume()
    {
        float volume = sfxSlider.value;
        if (volume == 0) 
        {
            myMixer.SetFloat("SFXVolume", -80f); // Silencio total
        }
        else
        {
            myMixer.SetFloat("SFXVolume", Mathf.Log10(volume) * 20f);
        }
        PlayerPrefs.SetFloat("sfxVolume", volume);
    }


    private void LoadVolume()
    {
        if (PlayerPrefs.HasKey("musicVolume"))
        {
            musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
            SetMusicVolume();
        }

        if (PlayerPrefs.HasKey("sfxVolume"))
        {
            sfxSlider.value = PlayerPrefs.GetFloat("sfxVolume");
            SetSFXVolume();
        }
    }
}