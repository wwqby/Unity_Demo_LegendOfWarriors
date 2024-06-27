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
public class SceneManager : MonoBehaviour, ISavable
{
    [Header("菜单场景")]
    public SceneSO menuScene;
    [Header("起始场景")]
    public SceneSO startScene;
    [Header("当前场景和位置")]
    public SceneSO currentScene;
    public Vector3 currentPosition;
    [Header("淡入淡出时间")]
    public float fadeDuration = 1;
    [Header("玩家位置")]
    public Transform player;
    [Header("事件监听")]
    public SceneLoadEventSO sceneLoadEventListener;

    public MenuConfirmEventSO menuConfirmEvent;

    [Header("事件广播")]
    public FadeChangeEventSO fadeBroadcast;
    public DataEventSO dataBroadcast;


    private void OnEnable()
    {   
        Debug.Log("SceneManager OnEnable");
        menuConfirmEvent.onNewGameAction += OnNewGame;
        menuConfirmEvent.onLoadGameAction += OnLoadGame;
        menuConfirmEvent.onBackMenuAction += LoadMenuScene;
        sceneLoadEventListener.OnSceneLoadRequestAction += OnSceneLoadRequest;
        ISavable savable = this;
        savable.RigisterData();
    }

    private void OnDisable()
    {
        Debug.Log("SceneManager OnDisable");
        menuConfirmEvent.onNewGameAction -= OnNewGame;
        menuConfirmEvent.onLoadGameAction -= OnLoadGame;
        menuConfirmEvent.onBackMenuAction -= LoadMenuScene;
        sceneLoadEventListener.OnSceneLoadRequestAction -= OnSceneLoadRequest;
        ISavable savable = this;
        savable.UnRigisterData();
    }



    private void Start()
    {

        LoadMenuScene();
    }

    /// <summary>
    /// 场景加载
    /// </summary>
    /// <param name="transScene"></param>
    /// <param name="transPosition"></param>
    /// <param name="fade"></param>
    private void OnSceneLoadRequest(SceneSO transScene, Vector3 transPosition, bool fade)
    {
        StartCoroutine(OnSceneChanging(transScene, transPosition, fade));
    }

    /// <summary>
    /// 协程内部完成场景切换
    /// </summary>
    /// <param name="sceneToGo"></param>
    /// <param name="positionToGo"></param>
    /// <param name="fade"></param>
    /// <returns></returns>
    private IEnumerator OnSceneChanging(SceneSO sceneToGo, Vector3 positionToGo, bool fade)
    {
        //todo 渐入渐出
        if (fade)
        {
            fadeBroadcast.OnFadeOut(fadeDuration);
        }
        yield return new WaitForSeconds(fadeDuration);

        if (currentScene != null)
        {
            //退出旧场景
            var oldScene = currentScene;
            sceneLoadEventListener.OnSceneUnloadEventRaised(oldScene);
            player.gameObject.SetActive(false);
            var unloadFuture = currentScene.sceneReference.UnLoadScene();
            unloadFuture.Completed += (obj) =>
            {
                sceneLoadEventListener.OnSceneUnloadCompleteEventRaised(oldScene);
            };
            yield return unloadFuture;
        }

        //加载新场景
        currentScene = sceneToGo;
        sceneLoadEventListener.OnSceneLoadEventRaised(currentScene, positionToGo, fade);
        Debug.Log("场景切换currentScene = " + currentScene.sceneReference.ToString());
        var loadFuture = currentScene.sceneReference.LoadSceneAsync(LoadSceneMode.Additive);
        //加载完成处理
        loadFuture.Completed += (obj) =>
        {
            //移动角色位置
            player.gameObject.SetActive(true);
            player.position = positionToGo;
            currentPosition = positionToGo;
            //场景加载完毕做处理
            sceneLoadEventListener.OnSceneLoadCompleteEventRaised(currentScene);
            //渐入渐出
            if (fade)
            {
                fadeBroadcast.OnFadeIn(fadeDuration);
            }
        };
        yield return loadFuture;

    }

    /// <summary>
    /// 加载第一个场景-菜单
    /// </summary>
    private void LoadMenuScene()
    {
        //直接调用事件本身来触发广播和事件
        sceneLoadEventListener.OnSceneLoadRequestEventRaised(menuScene, menuScene.playerPosition, true);
    }

    /// <summary>
    /// 加载新游戏场景
    /// </summary>
    private void OnNewGame()
    {
        sceneLoadEventListener.OnSceneLoadRequestEventRaised(startScene, startScene.playerPosition, true);
    }

    /// <summary>
    /// 继续游戏
    /// </summary>
    private void OnLoadGame()
    {
        dataBroadcast.OnDataLoadEventRaised();
    }


    public DataDefination GetDataDefination()
    {
        return GetComponent<DataDefination>();
    }

    public void SaveData(DataModel dataModel)
    {
        dataModel.lastSceneObj = dataModel.getSceneDataString(currentScene);
    }

    public void LoadData(DataModel dataModel)
    {
        var lastScene = dataModel.GetSceneDataObj();
        if (lastScene.sceneReference != null)
        {
            //有存档
            sceneLoadEventListener.OnSceneLoadRequestEventRaised(lastScene, lastScene.playerPosition, true);
        }
        else
        {
            //没有查到存档,启动新游戏
            menuConfirmEvent.OnNewGameActionRaised();
        }
    }

}
