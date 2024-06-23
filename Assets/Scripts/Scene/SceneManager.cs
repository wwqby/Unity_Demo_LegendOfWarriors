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
    public PlayerInputControls inputActions;
    [Header("传送门事件广播")]
    public TransGateEventSO transGateEventListener;

    public VoidEventSO afterSceneLoad;

    public FadeChangeEventSO fadeBroadcast;


    private void Awake()
    {
        inputActions = new PlayerInputControls();
        //todo 这里是新游戏的入口函数
        NewGame();
    }

    private void OnEnable()
    {
        inputActions.Enable();
        transGateEventListener.OnTransGateAction += OnTransGateActive;
    }

    private void OnDisable()
    {
        inputActions.Disable();
        transGateEventListener.OnTransGateAction -= OnTransGateActive;
    }

    /// <summary>
    /// 传送门触发
    /// </summary>
    /// <param name="transScene"></param>
    /// <param name="transPosition"></param>
    /// <param name="fade"></param>
    private void OnTransGateActive(SceneSO transScene, Vector3 transPosition, bool fade)
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
        //隐藏角色，禁止输入
        player.gameObject.SetActive(false);
        inputActions.Disable();
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
            //打开角色，打开输入
            player.gameObject.SetActive(true);
            inputActions.Enable();
            afterSceneLoad.RaiseEvent();
            //todo 渐入渐出
            if (fade)
            {
                fadeBroadcast.OnFadeIn(fadeDuration);
            }
        };

    }


    private void NewGame()
    {
        StartCoroutine(TransGateActive(oriScene, oriPosition, true));
    }
}
