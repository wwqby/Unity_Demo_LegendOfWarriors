using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
/// <summary>
/// 无参数事件广播
/// </summary>
[CreateAssetMenu(menuName = "Event/VoidEventSO")]
public class VoidEventSO : ScriptableObject
{
   public UnityAction OnVoidEventRaised;

   public void RaiseEvent(){
       OnVoidEventRaised?.Invoke();
   }
   
}
