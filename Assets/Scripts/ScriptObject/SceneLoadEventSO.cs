using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Events;

/// <summary>
/// 场景加载事件
/// </summary>
[CreateAssetMenu(fileName = "NewSceneLoadEventSO", menuName = "Event/SceneLoadEvent")]
public class SceneLoadEventSO : ScriptableObject
{   

    /// <summary>
    /// 请求切换场景
    /// 整个过程有四个切入点
    /// 卸载
    /// 卸载完成
    /// 加载
    /// 加载完成
    /// </summary>
    public UnityAction<SceneSO,Vector3,bool> OnSceneLoadRequestAction;
    /// <summary>
    /// 卸载场景事件
    /// </summary>
    public UnityAction<SceneSO> OnSceneUnloadAction;
    /// <summary>
    /// 卸载完成事件
    /// </summary>
    public UnityAction<SceneSO> OnSceneUnloadCompleteAction;
    /// <summary>
    /// 加载事件
    /// </summary>
    public UnityAction<SceneSO,Vector3,bool> OnSceneLoadAction;
    /// <summary>
    /// 加载完成事件
    /// </summary>
    public UnityAction<SceneSO> OnSceneLoadCompleteAction;





    public void OnSceneLoadRequestEventRaised(SceneSO sceneSo, Vector3 position, bool fade)
    {
        OnSceneLoadRequestAction?.Invoke(sceneSo, position, fade);
    }
    /// <summary>
    /// 卸载场景事件
    /// </summary>
    /// <param name="sceneSo"> 被卸载的场景</param>
    public void OnSceneUnloadEventRaised(SceneSO sceneSo)
    {
        OnSceneUnloadAction?.Invoke(sceneSo);
    }
    /// <summary>
    /// 卸载完成事件
    /// </summary>
    /// <param name="sceneSo">被卸载的场景</param>
    public void OnSceneUnloadCompleteEventRaised(SceneSO sceneSo)
    {
        OnSceneUnloadCompleteAction?.Invoke(sceneSo);
    }

    /// <summary>
    /// 触发传送门事件
    /// </summary>
    /// <param name="sceneSo">场景SO文件</param>
    /// <param name="position">人物传送后的位置</param>
    /// <param name="fade">是否淡入淡出</param>
    public void OnSceneLoadEventRaised(SceneSO sceneSo, Vector3 position, bool fade)
    {
        OnSceneLoadAction?.Invoke(sceneSo, position, fade);
    }

    /// <summary>
    /// 加载场景结束事件
    /// </summary>
    /// <param name="sceneSo">被加载场景</param>
    public void OnSceneLoadCompleteEventRaised(SceneSO sceneSo)
    {
        OnSceneLoadCompleteAction?.Invoke(sceneSo);
    }
}
