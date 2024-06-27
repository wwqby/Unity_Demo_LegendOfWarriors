using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PausePanel : MonoBehaviour
{
    public Slider volumeSlider;
    public AudioSettingEventSO audioSettingEvent;

    private void OnEnable()
    {
        audioSettingEvent.OnAudioVolumeReturnedEvent += OnAudioVolumeReturnedEvent;
    }


    private void OnDisable()
    {
        audioSettingEvent.OnAudioVolumeReturnedEvent -= OnAudioVolumeReturnedEvent;
    }

    private void OnAudioVolumeReturnedEvent(float volumeAmount)
    {
        volumeSlider.value = (volumeAmount + 80) / 100;
    }

    public void OnAudioVolumeChangedEvent(float amount)
    {
        audioSettingEvent.OnAudioVolumeChangedEventRaised(amount * 100 - 80);
    }
}
