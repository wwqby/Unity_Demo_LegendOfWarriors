using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using System;
/// <summary>
/// 管理场景切换
/// </summary>
public class SceneManager : MonoBehaviour
{
    [Header("起始场景和位置")]
    public SceneSO oriScene;
    public Vector3 oriPosition;
    [Header("当前场景和位置")]
    public SceneSO currentScene;
    public Vector3 currentPosition;
    [Header("淡入淡出时间")]
    public float fadeDuration = 1;
    [Header("玩家位置")]
    public Transform player;
    [Header("事件监听")]
    public SceneLoadEventSO sceneLoadEventListener;

    public VoidEventSO afterSceneLoadListener;

    [Header("事件广播")]
    public FadeChangeEventSO fadeBroadcast;


    private void OnEnable()
    {
        sceneLoadEventListener.OnSceneLoadAction += OnSceneLoad;
    }

    private void OnDisable()
    {
        sceneLoadEventListener.OnSceneLoadAction -= OnSceneLoad;
    }

    private void Start() {
        
        LoadOriScene();
    }

    /// <summary>
    /// 场景加载
    /// </summary>
    /// <param name="transScene"></param>
    /// <param name="transPosition"></param>
    /// <param name="fade"></param>
    private void OnSceneLoad(SceneSO transScene, Vector3 transPosition, bool fade)
    {
        StartCoroutine(TransGateActive(transScene, transPosition, fade));
    }

    /// <summary>
    /// 协程内部完成场景切换
    /// </summary>
    /// <param name="transScene"></param>
    /// <param name="transPosition"></param>
    /// <param name="fade"></param>
    /// <returns></returns>
    private IEnumerator TransGateActive(SceneSO transScene, Vector3 transPosition, bool fade)
    {
        //todo 渐入渐出
        if (fade)
        {
            fadeBroadcast.OnFadeOut(fadeDuration);
        }
        yield return new WaitForSeconds(fadeDuration);
        //退出旧场景
        yield return currentScene?.sceneReference?.UnLoadScene();
        //加载新场景
        currentScene = transScene;
        var loadFuture = currentScene.sceneReference.LoadSceneAsync(LoadSceneMode.Additive);
        //加载完成处理
        loadFuture.Completed += (obj) =>
        {
            //移动角色位置
            player.position = transPosition;
            currentPosition = transPosition;
            //场景加载完毕做处理
            if (currentScene.sceneType == SceneType.Scene)
            {
                afterSceneLoadListener.RaiseEvent();
            }
            //渐入渐出
            if (fade)
            {
                fadeBroadcast.OnFadeIn(fadeDuration);
            }
        };

    }

    /// <summary>
    /// 加载第一个场景-菜单
    /// </summary>
    private void LoadOriScene()
    {   
        //直接调用事件本身来触发广播和事件
        // StartCoroutine(TransGateActive(oriScene, oriPosition, true));
        sceneLoadEventListener.OnSceneLoadEventRaised(oriScene, oriPosition, true);
    }
}
