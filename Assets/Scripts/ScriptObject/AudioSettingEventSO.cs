using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// 音频设置事件
/// 获取当前音量
/// 设置当前音量
/// </summary>
[CreateAssetMenu(menuName = "Event/AudioSettingEventSO")]
public class AudioSettingEventSO : ScriptableObject
{   
    /// <summary>
    /// 打开音量设置
    /// </summary>
    public UnityAction onAudioSettingOpendEvent;
    /// <summary>
    /// 获取音量
    /// </summary>
    public UnityAction<float> OnAudioVolumeReturnedEvent;

    /// <summary>
    /// 设置音量
    /// </summary>
    public UnityAction<float> onAudioVolumeChangedEvent;

    public void OnAudioSettingOpendEventRaised()
    {
        onAudioSettingOpendEvent?.Invoke();
    }

    public void OnAudioVolumeReturnedEventRaised(float volume)
    {
        OnAudioVolumeReturnedEvent?.Invoke(volume);
    }

    public void OnAudioVolumeChangedEventRaised(float volume)
    {
        onAudioVolumeChangedEvent?.Invoke(volume);
    }

}
