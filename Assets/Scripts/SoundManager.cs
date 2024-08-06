using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{

    [SerializeField] Slider volumeSlider;
    void Start()
    {
        if (!PlayerPrefs.HasKey("musicVolume"))
        {
            PlayerPrefs.SetFloat("musicVolume", 1);
            LoadSound();
        }
        else
        {
            LoadSound();
        }
    }


    public void ChangeSoundVolume()
    {
        AudioListener.volume = volumeSlider.value;
        SaveSound();
    }

    private void LoadSound()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("musicVolume");
    }

    private void SaveSound()
    {
        PlayerPrefs.SetFloat("musicVolume", volumeSlider.value);
    }
}
