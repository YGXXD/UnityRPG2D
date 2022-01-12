using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class AudioManager : LongSing<AudioManager>
{
    [SerializeField] private AudioSource sFXPlayer;
    [SerializeField] private Slider volume;
    [SerializeField] private GameObject settingGameObject;
    [SerializeField] private AudioSource[] audio;
    private float tempVolume;
    protected override void Awake()
    {
        UpdateVolume(volume.value);
        base.Awake();
    }

    private void Update()
    {
        if(settingGameObject.activeSelf && settingGameObject != null)
            UpdateVolume(volume.value);
    }

    public void PlaySFXAudioClip(AudioData Audio)
    {
        //sFXPlayer.clip = audioClip;
        //sFXPlayer.volume = volume;
        //sFXPlayer.Play(); 
        sFXPlayer.PlayOneShot(Audio.audioClip, Audio.volume);//播放音频
    }

    private void UpdateVolume(float volumeValue)
    {
        for (int i = 0; i < audio.Length; i++)
        {
            audio[i].volume = volumeValue;
        }
    }

    public void TempVolume()
    {
        tempVolume = volume.value;
    }

    public void CancelAlterVolume()
    {
        volume.value = tempVolume;
        UpdateVolume(tempVolume);
    }
}
