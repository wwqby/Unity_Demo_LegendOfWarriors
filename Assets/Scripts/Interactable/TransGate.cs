using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

/// <summary>
/// 传送点
/// </summary>
public class TransGate : MonoBehaviour, IInteractable
{

    public SceneLoadEventSO transGateEventBroadcast;
    public SceneSO transToGo;
    public Vector3 positionToGo;
    public bool transWithFade = true;

    #region 接口方法
    public bool CanInteractable()
    {
        //传送门可以一直交互
        return true;
    }

    public void OnInteractableConfirm()
    {
        //如果位置信息没有赋值，那就获取目标场景的初始位置
        if (positionToGo.x == 0 && positionToGo.y == 0 && positionToGo.z == 0)
        {
            positionToGo = transToGo.playerPosition;
        }
        //触发传送事件 传送地图，传送坐标，传送特效
        transGateEventBroadcast.OnSceneLoadRequestEventRaised(transToGo, positionToGo, transWithFade);
    }
    #endregion
}
