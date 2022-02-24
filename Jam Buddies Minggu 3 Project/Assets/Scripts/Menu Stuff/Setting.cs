using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Setting : MonoBehaviour
{
    public Slider master;
    public Slider bgm;
    public Slider sfx;
    public GameObject fullscreenButton;
    public Sprite buttonNo;
    public Sprite buttonYes;

    private void Awake()
    {
        master.value = PlayerPrefs.GetFloat("masterVolume", 1f) * 100f;
        bgm.value = PlayerPrefs.GetFloat("musicVolume", 1f) * 100f;
        sfx.value = PlayerPrefs.GetFloat("sfxVolume", 1f) * 100f;
        StartCoroutine( UpdateButtonSprite() );
    }
    public void ToggleFullscreen()
    {
        if (Screen.fullScreen)
        {
            Screen.SetResolution(1280, 720, false);
        }
        else
        {
            Screen.fullScreen = true;
        }
        StartCoroutine( UpdateButtonSprite() );
    }

    private IEnumerator UpdateButtonSprite()
    {
        yield return new WaitForSeconds(0f);
        fullscreenButton.GetComponent<Image>().sprite = (Screen.fullScreen) ? buttonYes : buttonNo;
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
    public void ClearPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
        Application.Quit();
    }
}
