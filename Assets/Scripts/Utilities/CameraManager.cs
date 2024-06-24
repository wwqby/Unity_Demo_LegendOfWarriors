using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private CinemachineConfiner2D cinemachineConfiner2D;

    public CinemachineImpulseSource cinemachineImpulseSource;
    public VoidEventSO cameraShakedEvent;
    public VoidEventSO afterSceneLoad;

    private void Awake() {
        cinemachineConfiner2D = GetComponent<CinemachineConfiner2D>();
    }

    private void OnEnable() {
        cameraShakedEvent.OnVoidEventAction += OnCameraShaked;
        afterSceneLoad.OnVoidEventAction += GetNewBounds;
    }

    private void OnDisable() {
        cameraShakedEvent.OnVoidEventAction -= OnCameraShaked;
        afterSceneLoad.OnVoidEventAction -= GetNewBounds;
    }


    private void Start() {
        GetNewBounds();
    }

    private void GetNewBounds() {
        //找到bounds对象
        var obj = GameObject.Find("Bounds");
        if(obj ==null){
            return;
        }
        //重新赋值相机限制区域
        cinemachineConfiner2D.m_BoundingShape2D = obj.GetComponent<Collider2D>();
        //刷新区域缓存
        cinemachineConfiner2D.InvalidateCache();
    }

    /// <summary>
    /// 触发相机震动
    /// </summary>
    private void OnCameraShaked(){
        cinemachineImpulseSource.GenerateImpulse();
    }
}
