using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 音频管理
/// </summary>
public class AudioManager : MonoBehaviour
{
    [Header("音频管理")]
    public AudioSource bgm;
    public AudioSource sfx;

    [Header("音轨管理")]
    public AudioEventSO BgmAudioEvent;
    public AudioEventSO SfxAudioEvent;
    
    #region 脚本注册和取消
    private void OnEnable() {
        BgmAudioEvent.OnAudioPlay += AudioPlayBgm;
        SfxAudioEvent.OnAudioPlay += AudioPlaySfx;
    }

    private void OnDisable() {
        BgmAudioEvent.OnAudioPlay -= AudioPlayBgm;
        SfxAudioEvent.OnAudioPlay -= AudioPlaySfx;
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
