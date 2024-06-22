using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 可交互物体接口
/// </summary>
public interface IInteractable
{   
    /// <summary>
    /// 是否可以继续交互
    /// </summary>
    public bool CanInteractable();

    /// <summary>
    /// 确认交互
    /// </summary>
    public void OnInteractableConfirm();
}
