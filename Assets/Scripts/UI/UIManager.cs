using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{   
    [Header("状态条")]
    public PlayerStatebar playerStatebar;
    [Header("游戏结束面板")]
    public GameObject gameOverPanel;
    [Header("事件监听")]
    public CharactorEventSO healthChangeListener;
    public SceneLoadEventSO sceneLoadListener;
    public GameStatusEventSO gameStatusEventListener;

    private void OnEnable() {
        healthChangeListener.OnCharactorEventRaised += OnHealthChangeEvent;
        sceneLoadListener.OnSceneLoadCompleteAction += OnSceneLoadComplete;
        sceneLoadListener.OnSceneLoadRequestAction += OnSceneLoadRequest;

        gameStatusEventListener.OnGameOverAction += OnGameOverEvent;
    }

    private void OnDisable() {
        healthChangeListener.OnCharactorEventRaised -= OnHealthChangeEvent;
        sceneLoadListener.OnSceneLoadCompleteAction -= OnSceneLoadComplete;
        sceneLoadListener.OnSceneLoadRequestAction -= OnSceneLoadRequest;
        gameStatusEventListener.OnGameOverAction -= OnGameOverEvent;
    }



    /// <summary>
    /// 场景切换时
    /// 关闭血条
    /// </summary>
    /// <param name="arg0"></param>
    /// <param name="arg1"></param>
    /// <param name="arg2"></param>
    private void OnSceneLoadRequest(SceneSO arg0, Vector3 arg1, bool arg2)
    {
        playerStatebar.gameObject.SetActive(false);
        gameOverPanel.SetActive(false);
    }


    /// <summary>
    /// 场景加载完成
    /// 检查是否显示头像
    /// </summary>
    /// <param name="scene"></param>
    private void OnSceneLoadComplete(SceneSO scene)
    {
        playerStatebar.gameObject.SetActive(scene.sceneType == SceneType.Scene);
        gameOverPanel.SetActive(false);
    }
    /// <summary>
    /// 变更人物血量
    /// </summary>
    /// <param name="character"></param>
    public void OnHealthChangeEvent(Character character){
        playerStatebar.ChangeHealth(character.currentHealth/character.maxHealth);
    }

    /// <summary>
    /// 游戏结束
    /// </summary>
    private void OnGameOverEvent()
    {
        gameOverPanel.SetActive(true);
        playerStatebar.gameObject.SetActive(false);
    }
}
