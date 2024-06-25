using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// 数据事件
/// 提供数据保存和加载广播
/// </summary>
[CreateAssetMenu(fileName = "NewDataEventSO", menuName = "Event/DataEvent")]
public class DataEventSO : ScriptableObject
{
    public UnityAction onDataSaveAction;
    public UnityAction onDataLoadAction;

    public void OnDataSaveEventRaised()
    {
        onDataSaveAction?.Invoke();
    }

    public void OnDataLoadEventRaised()
    {
        onDataLoadAction?.Invoke();
    }
}
