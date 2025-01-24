using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    [SerializeField] AudioSource audioBGM;
    [SerializeField] AudioSource audioSFX;
    [SerializeField] AudioClip clickButtonClip;
    [SerializeField] AudioClip bgmClip;
    [SerializeField] AudioClip getDameClip;
    [SerializeField] AudioClip gameOverClip;
    [SerializeField] AudioClip levelUpClip;

    private void Start()
    {
        SoundBGM();
    }
    public void SetVolumAudioBGM(bool state)
    {
        if (state)
        {
            audioBGM.volume = 0.5f;
        }
        else
        {
            audioBGM.volume = 0;
        }
    }
    public void SetVolumAudioSFX(bool state)
    {
        if (state)
        {
            audioSFX.volume = 1f;
        }
        else
        {
            audioSFX.volume = 0;
        }
    }
    public void SoundClickButton()
    {
        audioSFX.clip = clickButtonClip;
        audioSFX.Play();
    }
    public void SoundBGM()
    {
        audioBGM.clip = bgmClip;
        audioBGM.loop = true;
        audioBGM.Play();
    }
    public void SoundGetDame()
    {
        audioSFX.clip = getDameClip;
        audioSFX.Play();
    }
    public void SoundGameOver()
    {
        audioSFX.clip = gameOverClip;
        audioSFX.Play();
    }
    public void SoundLevelUp()
    {
        audioSFX.clip = levelUpClip;
        audioSFX.Play();
    }
}