using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{   
    [Header("状态条")]
    public PlayerStatebar playerStatebar;
    [Header("正常游戏面板")]
    public GameObject normalGamePanel;
    [Header("游戏结束面板")]
    public GameObject gameOverPanel;
    [Header("模拟手柄面板")]
    public GameObject mobileTouchPanel;
    [Header("暂停游戏面板")]
    public GameObject pauseGamePanel;
    [Header("事件监听")]
    public CharactorEventSO healthChangeListener;
    public SceneLoadEventSO sceneLoadListener;
    public GameStatusEventSO gameStatusEventListener;
    public MenuConfirmEventSO menuConfirmEvent;

    private void Awake() {
        //如果是桌面端，隐藏触摸面板
        #if UNITY_STANDALONE || UNITY_WEBGL
            mobileTouchPanel.SetActive(false);
        #endif    
    }


    private void OnEnable() {
        healthChangeListener.OnCharactorEventRaised += OnHealthChangeEvent;
        sceneLoadListener.OnSceneLoadCompleteAction += OnSceneLoadComplete;
        sceneLoadListener.OnSceneLoadRequestAction += OnSceneLoadRequest;
        gameStatusEventListener.OnGameOverAction += OnGameOverEvent;
        menuConfirmEvent.onPauseGameAction += OnMenuPauseEvent;
        menuConfirmEvent.onResumeGameAction += OnMenuResumeEvent;
    }

    private void OnDisable() {
        healthChangeListener.OnCharactorEventRaised -= OnHealthChangeEvent;
        sceneLoadListener.OnSceneLoadCompleteAction -= OnSceneLoadComplete;
        sceneLoadListener.OnSceneLoadRequestAction -= OnSceneLoadRequest;
        gameStatusEventListener.OnGameOverAction -= OnGameOverEvent;
        menuConfirmEvent.onPauseGameAction -= OnMenuPauseEvent;
        menuConfirmEvent.onResumeGameAction -= OnMenuResumeEvent;
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
        normalGamePanel.SetActive(false);
        pauseGamePanel.SetActive(false);
        gameOverPanel.SetActive(false);
    }


    /// <summary>
    /// 场景加载完成
    /// 检查是否显示头像
    /// </summary>
    /// <param name="scene"></param>
    private void OnSceneLoadComplete(SceneSO scene)
    {
        normalGamePanel.SetActive(scene.sceneType == SceneType.Scene);
        pauseGamePanel.SetActive(false);
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
        normalGamePanel.SetActive(false);
        pauseGamePanel.SetActive(false);
    }

    /// <summary>
    /// 暂停游戏
    /// </summary>
    /// <exception cref="NotImplementedException"></exception>
    private void OnMenuResumeEvent()
    {
        normalGamePanel.SetActive(true);
        pauseGamePanel.SetActive(false);
    }

    /// <summary>
    /// 恢复游戏
    /// </summary>
    /// <exception cref="NotImplementedException"></exception>
    private void OnMenuPauseEvent()
    {
        pauseGamePanel.SetActive(true);
        normalGamePanel.SetActive(false);
    }
}
