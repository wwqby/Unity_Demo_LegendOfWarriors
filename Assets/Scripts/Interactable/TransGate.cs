using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

/// <summary>
/// 传送点
/// </summary>
public class TransGate : MonoBehaviour, IInteractable
{

    public TransGateEventSO transGateEventBroadcast;
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
        //触发传送事件 传送地图，传送坐标，传送特效
        transGateEventBroadcast.OnTransGateEventRaised(transToGo, positionToGo, transWithFade);
    }
    #endregion
}
