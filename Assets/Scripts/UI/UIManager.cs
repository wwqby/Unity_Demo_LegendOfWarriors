using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{   
    public PlayerStatebar playerStatebar;
    [Header("事件监听")]
    public CharactorEventSO healthChangeListener;
    public SceneLoadEventSO sceneLoadListener;

    private void OnEnable() {
        healthChangeListener.OnCharactorEventRaised += OnHealthChangeEvent;
        sceneLoadListener.OnSceneLoadAction += OnSceneLoad;
    }

    private void OnDisable() {
        healthChangeListener.OnCharactorEventRaised -= OnHealthChangeEvent;
        sceneLoadListener.OnSceneLoadAction -= OnSceneLoad;
    }

    private void OnSceneLoad(SceneSO scene, Vector3 arg1, bool arg2)
    {
        playerStatebar.gameObject.SetActive(scene.sceneType == SceneType.Scene);
    }

    public void OnHealthChangeEvent(Character character){
        playerStatebar.ChangeHealth(character.currentHealth/character.maxHealth);
    }
}
