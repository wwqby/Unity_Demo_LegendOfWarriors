using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// 音频事件广播
/// </summary>
[CreateAssetMenu(fileName = "New Audio Event", menuName = "Event/Audio Event")]
public class AudioEventSO : ScriptableObject
{
    public UnityAction<AudioClip> OnAudioPlay;


    public void OnAudioPlayRaised(AudioClip clip)
    {
        OnAudioPlay?.Invoke(clip);
    }
}
