using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
/// <summary>
/// 音频管理
/// </summary>
public class AudioManager : MonoBehaviour
{
    [Header("音频管理")]
    public AudioSource bgm;
    public AudioSource sfx;
    public AudioMixer audioMixer;

    [Header("音轨管理")]
    public AudioEventSO BgmAudioEvent;
    public AudioEventSO SfxAudioEvent;

    [Header("音量管理")]
    public AudioSettingEventSO audioSettingEvent;
    
    #region 脚本注册和取消
    private void OnEnable() {
        BgmAudioEvent.OnAudioPlay += AudioPlayBgm;
        SfxAudioEvent.OnAudioPlay += AudioPlaySfx;
        audioSettingEvent.onAudioSettingOpendEvent += OnAudiosettingOpenedEvent;
        audioSettingEvent.onAudioVolumeChangedEvent += OnAudiovolumeChangedEvent;
    }

    private void OnDisable() {
        BgmAudioEvent.OnAudioPlay -= AudioPlayBgm;
        SfxAudioEvent.OnAudioPlay -= AudioPlaySfx;
        audioSettingEvent.onAudioSettingOpendEvent -= OnAudiosettingOpenedEvent;
        audioSettingEvent.onAudioVolumeChangedEvent -= OnAudiovolumeChangedEvent;
    }

    private void OnAudiovolumeChangedEvent(float volumeAmount)
    {
        audioMixer.SetFloat("MasterVolume", volumeAmount);
    }

    private void OnAudiosettingOpenedEvent()
    {
        audioMixer.GetFloat("MasterVolume", out float volumeAmount);
        audioSettingEvent.OnAudioVolumeReturnedEventRaised(volumeAmount);
    }

    #endregion

    /// <summary>
    /// 播放背景音乐
    /// </summary>
    private void AudioPlayBgm(AudioClip audioClip){
        bgm.clip = audioClip;
        bgm?.Play();
    }
    /// <summary>
    /// 播放音效
    /// </summary>
    private void AudioPlaySfx(AudioClip audioClip){
        sfx.clip = audioClip;
        sfx?.Play();
    }
}
