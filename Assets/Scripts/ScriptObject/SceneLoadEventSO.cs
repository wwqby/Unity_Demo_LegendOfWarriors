using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Events;

/// <summary>
/// 传送门事件
/// </summary>
[CreateAssetMenu(fileName = "NewTransGateEvent", menuName = "Event/SceneLoadEvent")]
public class SceneLoadEventSO : ScriptableObject
{
    public UnityAction<SceneSO,Vector3,bool> OnSceneLoadAction;

    /// <summary>
    /// 触发传送门事件
    /// </summary>
    /// <param name="sceneSo">场景SO文件</param>
    /// <param name="position">人物传送后的位置</param>
    /// <param name="isPlayer">是否淡入淡出</param>
    public void OnSceneLoadEventRaised(SceneSO sceneSo, Vector3 position, bool isPlayer)
    {
        OnSceneLoadAction?.Invoke(sceneSo, position, isPlayer);
    }
}
