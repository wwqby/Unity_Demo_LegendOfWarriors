using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 音频定义
/// 包含音轨和音效
/// </summary>
public class AudioDefination : MonoBehaviour
{   
    [Header("音频定义")]
    public  AudioClip audioClip;
    [Header("音轨定义")]
    public  AudioEventSO audioEventSO;
    [Header("是否在启动时播放")]
    public bool playOnStart = true;



    /// <summary>
    /// 在启动脚本时，是否自动播放音频
    /// </summary>
    private void OnEnable() {
        if (!playOnStart) return;
        PlayAudioClip();
    }


    
    /// <summary>
    /// 手动播放音频
    /// </summary>
    public void PlayAudioClip()
    {
        audioEventSO.OnAudioPlayRaised(audioClip);
    }
}
