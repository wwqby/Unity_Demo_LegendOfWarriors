using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 游戏进度管理器
/// </summary>
public class GameManager : MonoBehaviour
{   
    [Header("游戏状态事件监听")]
    public GameStatusEventSO gameStatusEvent;

    private void OnEnable() {
        gameStatusEvent.OnGamePauseAction += OnPauseGameEvent;
        gameStatusEvent.OnGameResumeAction += OnResumeGameEvent;
    }

    private void OnDisable() {
        gameStatusEvent.OnGamePauseAction -= OnPauseGameEvent;
        gameStatusEvent.OnGameResumeAction -= OnResumeGameEvent;
    }

    private void OnResumeGameEvent()
    {
        Time.timeScale = 1;
    }

    private void OnPauseGameEvent()
    {
        Time.timeScale = 0;
    }
}
