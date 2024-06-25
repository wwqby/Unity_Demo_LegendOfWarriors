using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// 数据管理器
/// </summary>
public class DataManager : MonoBehaviour
{

    public static DataManager instance;

    [Header("事件监听")]
    public DataEventSO dataEventListener;
    public MenuConfirmEventSO menuConfirmBroadcast;
    [Header("数据管理队列")]
    private List<ISavable> savableDataList;

    private DataModel dataModel;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        savableDataList = new List<ISavable>();
        dataModel = new DataModel();
    }

    private void OnEnable()
    {
        dataEventListener.onDataLoadAction += OnDataLoadEvent;
        dataEventListener.onDataSaveAction += OnDataSaveEvent;
    }

    private void OnDisable()
    {
        dataEventListener.onDataLoadAction -= OnDataLoadEvent;
        dataEventListener.onDataSaveAction -= OnDataSaveEvent;
    }


    private void Update()
    {
        //todo 返回桌面
        if (Keyboard.current.qKey.wasPressedThisFrame)
        {
            menuConfirmBroadcast.OnBackMenuActionRaised();
        }
    }


    public void RegisterISavableData(ISavable savable)
    {
        if (!savableDataList.Contains(savable))
        {
            savableDataList.Add(savable);
        }
    }

    public void UnRegisterISavableData(ISavable savable)
    {
        savableDataList.Remove(savable);
    }

    /// <summary>
    /// 保存场景
    /// </summary>
    /// <param name="scene"></param>
    public void SaveSceneData(SceneSO scene)
    {
        dataModel.lastScene = scene;
    }

    public SceneSO getSceneData()
    {
        return dataModel.lastScene;
    }
    private void OnDataLoadEvent()
    {
        Debug.Log("数据加载");
        foreach (var item in savableDataList)
        {
            item.LoadData(dataModel);
        }
    }

    private void OnDataSaveEvent()
    {
        Debug.Log("数据保存");
        foreach (var item in savableDataList)
        {
            item.SaveData(dataModel);
        }
    }
}
