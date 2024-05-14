using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.TextCore.Text;
/// <summary>
/// 角色属性事件的中间存储件
/// </summary>
[CreateAssetMenu( menuName = "Event/CharactorEventSO")]
public class CharactorEventSO : ScriptableObject
{
    public UnityAction<Character> OnCharactorEventRaised;
    /// <summary>
    /// 发送通知下游订阅组件
    /// </summary>
    /// <param name="character">发送的内容</param>
    public void RaiseEvent(Character character){
        OnCharactorEventRaised?.Invoke(character);
    }
}
