using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    public SceneLoadEventSO sceneLoadListener;
    public MenuConfirmEventSO menuConfirmBroadcast;
    [Header("是否新游戏")]
    public bool isNew;
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
        menuConfirmBroadcast.onNewGameAction += OnNewGame;
        menuConfirmBroadcast.onLoadGameAction += OnLoadGame;
    }

    private void OnDisable()
    {
        dataEventListener.onDataLoadAction -= OnDataLoadEvent;
        dataEventListener.onDataSaveAction -= OnDataSaveEvent;
        menuConfirmBroadcast.onNewGameAction -= OnNewGame;
        menuConfirmBroadcast.onLoadGameAction -= OnLoadGame;
        //确保本对象被销毁时解除绑定
        sceneLoadListener.OnSceneLoadCompleteAction -= OnSceneLoadComplete;
    }

    

    private void Update()
    {
        //todo 返回桌面
        if (Keyboard.current.qKey.wasPressedThisFrame)
        {
            menuConfirmBroadcast.OnBackMenuActionRaised();
        }
    }

    public SceneSO GetLastScene(){
        return dataModel.GetSceneDataObj();
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


    private void OnDataLoadEvent()
    {
        Debug.Log("数据加载");
        //先加载场景数据
        var sceneManager = savableDataList.Find(obj=>obj is SceneManager) as SceneManager;
        if(sceneManager != null){
            //先绑定场景加载完成时通知事件
            sceneLoadListener.OnSceneLoadCompleteAction += OnSceneLoadComplete;
            Debug.Log("LoadData");
            sceneManager.LoadData(dataModel);
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

    private void OnSceneLoadComplete(SceneSO arg0)
    {
        foreach (var item in savableDataList)
        {
            //场景数据跳过
            if(item is SceneManager){
                continue;
            }
            item.LoadData(dataModel);
        }
        //解绑事件监听
        sceneLoadListener.OnSceneLoadCompleteAction -= OnSceneLoadComplete;
    }


    private void OnLoadGame()
    {
       isNew = false;
    }

    private void OnNewGame()
    {
        isNew = true;
    }

}
