using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
/// <summary>
/// 渐变事件广播
/// </summary>
[CreateAssetMenu(menuName = "Event/FadeChangeEventSO")]
public class FadeChangeEventSO : ScriptableObject
{
    public UnityAction<Color,float> OnFadeChangeAction;

    public void RaiseEvent(Color endValue,float duration){
        OnFadeChangeAction?.Invoke(endValue,duration);
    }
    /// <summary>
    /// 渐入 变成透明
    /// </summary>
    /// <param name="duration"></param>
    public void OnFadeIn(float duration){
        OnFadeChangeAction?.Invoke(Color.clear,duration);
    }

    /// <summary>
    /// 渐出 变成黑色
    /// </summary>
    /// <param name="duration"></param>
    public void OnFadeOut(float duration){
        OnFadeChangeAction?.Invoke(Color.black,duration);
    }
}
