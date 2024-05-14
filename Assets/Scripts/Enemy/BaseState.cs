using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 定义状态基类
/// </summary>
public abstract class BaseState
{   
    protected Enemy currentEnemy;

    /// <summary>
    /// 状态进入
    /// </summary>
    public abstract void OnEnter(Enemy enemy);
    /// <summary>
    /// 逻辑状态更新
    /// </summary>
    public abstract void OnLogicUpdate();
    /// <summary>
    /// 物理状态更新
    /// </summary>
    public abstract void OnPhysicsUpdate();
    /// <summary>
    /// 状态退出
    /// </summary>
    public abstract void OnExit();
}
