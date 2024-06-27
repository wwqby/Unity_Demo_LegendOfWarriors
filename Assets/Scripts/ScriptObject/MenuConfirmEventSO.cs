using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
/// <summary>
/// 菜单确认事件
/// </summary>
[CreateAssetMenu(menuName = "Event/MenuConfirmEventSO")]
public class MenuConfirmEventSO : ScriptableObject
{   

    /// <summary>
    /// 新游戏
    /// </summary>
    public UnityAction onNewGameAction;
    /// <summary>
    /// 载入游戏存档
    /// </summary>
    public UnityAction onLoadGameAction;
    /// <summary>
    /// 返回菜单
    /// </summary>
    public UnityAction onBackMenuAction;
    /// <summary>
    /// 退出游戏
    /// </summary>
    public UnityAction onQuitGameAction;
    /// <summary>
    /// 暂停游戏
    /// </summary>
    public UnityAction onPauseGameAction;
    /// <summary>
    /// 恢复游戏
    /// </summary>
    public UnityAction onResumeGameAction;


    public void OnNewGameActionRaised(){
        onNewGameAction.Invoke();
    }

    public void OnBackMenuActionRaised(){
        onBackMenuAction.Invoke();
    }

    public void OnLoadGameActionRaised(){
        onLoadGameAction.Invoke();
    }

    public void OnQuitGameActionRaised(){
        onQuitGameAction.Invoke();
    }


    public void OnPauseGameActionRaised(){
        onPauseGameAction.Invoke();
    }


    public void OnResumeGameActionRaised(){
        onResumeGameAction.Invoke();
    }
}
