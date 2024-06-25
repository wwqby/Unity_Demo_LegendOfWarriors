using System.Collections;
using System.Collections.Generic;
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
    /// 继续游戏
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
}
