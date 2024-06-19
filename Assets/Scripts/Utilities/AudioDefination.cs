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
    
    private void OnEnable() {
        if (!playOnStart) return;
        audioEventSO.OnAudioPlayRaised(audioClip);
    }
}
