using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSetting : MonoBehaviour
{
    public Slider master;
    public Slider music;
    public Slider sfx;

    private void Start()
    {
        master.value = PlayerPrefs.GetFloat("masterVolume", 1f) * 100f;
        music.value = PlayerPrefs.GetFloat("musicVolume", 1f) * 100f;
        sfx.value = PlayerPrefs.GetFloat("sfxVolume", 1f) * 100f;
    }

    public void SetMasterVolume(float value)
    {
        PlayerPrefs.SetFloat("masterVolume", value / 100f);
        AudioManager.instance.SetVolume();
    }
    public void SetMusicVolume(float value)
    {
        PlayerPrefs.SetFloat("musicVolume", value / 100f);
        AudioManager.instance.SetVolume();
    }
    public void SetSfxVolume(float value)
    {
        PlayerPrefs.SetFloat("sfxVolume", value / 100f);
        AudioManager.instance.SetVolume();
    }

}
