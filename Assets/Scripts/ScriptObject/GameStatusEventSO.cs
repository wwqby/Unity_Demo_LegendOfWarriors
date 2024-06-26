using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Event/GameStatusEventSO")]
public class GameStatusEventSO : ScriptableObject
{
    public UnityAction OnGamePauseAction;
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
