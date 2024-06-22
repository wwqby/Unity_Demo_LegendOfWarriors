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
    public SceneSO currentScene;

    public TransGateEventSO transGateEventSO;
    public float fadeDuration = 1;
    public Transform player;


    private void Awake() {
        //激活加载的场景Additive，默认single会替代原场景
        currentScene.sceneReference.LoadSceneAsync(LoadSceneMode.Additive);
    }

    private void OnEnable() {
        transGateEventSO.OnTransGateAction += OnTransGateActive;
    }

    private void OnDisable() {
        transGateEventSO.OnTransGateAction -= OnTransGateActive;
    }

    private void OnTransGateActive(SceneSO transScene, Vector3 transPosition, bool fade)
    {
        
        StartCoroutine(TransGateActive(transScene, transPosition, fade));
    }

    private IEnumerator TransGateActive(SceneSO transScene, Vector3 transPosition, bool fade)
    {
        if(fade){
            //todo 渐入渐出
        }
        yield return new WaitForSeconds(fadeDuration);
        //画面退出动画
        currentScene.sceneReference.UnLoadScene();
        currentScene = transScene;
        yield return currentScene.sceneReference.LoadSceneAsync(LoadSceneMode.Additive);
        player.position = transPosition;
    }
}
