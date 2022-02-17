using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public Music[] music, ambiance;
    public SFX[] sfx;
    public AudioSource mSource, sSource, aSource;

    public static AudioManager instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }

        else
        {
            Destroy(gameObject);
            return;
        }
        
        DontDestroyOnLoad(gameObject);

        foreach (Music m in music)
        {
            m.source = mSource;
        }

        foreach (Music m in ambiance)
        {
            m.source = aSource;
        }

        foreach (SFX s in sfx)
        {
            s.source = sSource;
        }
    }

    public void SetVolume()
    {
        mSource.volume = PlayerPrefs.GetFloat("masterVolume", 1f) * PlayerPrefs.GetFloat("musicVolume", 1f);
        sSource.volume = PlayerPrefs.GetFloat("masterVolume", 1f) * PlayerPrefs.GetFloat("sfxVolume", 1f);
    }

    float currentBaseVolM, currentBaseVolS, currentBaseVolA;

    public void PlayAmbiance(string name)
    {
        Music m = Array.Find(ambiance, ambiance => ambiance.name == name);
        currentBaseVolA = m.volume;
        m.source.loop = m.loop;
        m.source.volume = currentBaseVolA * PlayerPrefs.GetFloat("masterVolume", 1f) * PlayerPrefs.GetFloat("musicVolume", 1f);
        m.source.clip = m.clip;
        m.source.Play();
    }

    public void PlayM(string name)
    {
        Music m = Array.Find(music, music => music.name == name);
        currentBaseVolM = m.volume;
        m.source.loop = m.loop;
        m.source.volume = currentBaseVolM * PlayerPrefs.GetFloat("masterVolume", 1f) * PlayerPrefs.GetFloat("musicVolume", 1f);
        m.source.clip = m.clip;
        m.source.Play();
    }

    public void PlayS(string name)
    {
        SFX s = Array.Find(sfx, sfx => sfx.name == name);
        currentBaseVolS = s.volume;
        s.source.volume = currentBaseVolS * PlayerPrefs.GetFloat("masterVolume", 1f) * PlayerPrefs.GetFloat("sfxVolume", 1f);
        s.source.clip = s.clip;
        s.source.PlayOneShot(s.clip, s.source.volume);
    }
}
