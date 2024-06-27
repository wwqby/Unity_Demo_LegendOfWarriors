using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Event/GameStatusEventSO")]
public class GameStatusEventSO : ScriptableObject
{   
    /// <summary>
    /// 游戏暂停状态
    /// </summary>
    public UnityAction OnGamePauseAction;
    /// <summary>
    /// 游戏正常状态
    /// </summary>
    public UnityAction OnGameResumeAction;
    public UnityAction OnGameOverAction;

    public void OnGamePauseRaised()
    {
        OnGamePauseAction?.Invoke();
    }

    public void OnGameResumeRaised()
    {
        OnGameResumeAction?.Invoke();
    }

    public void OnGameOverRaised()
    {
        OnGameOverAction?.Invoke();
    }
}
