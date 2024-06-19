using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 攻击属性
/// </summary>
public class Attack : MonoBehaviour
{
    [Header("攻击力")]
    public int damage;
    [Header("攻击范围")]
    public int attackRange;
    [Header("攻击速度")]
    public int attackRate;

    /// <summary>
    /// 触发攻击
    /// OnTriggerStay2D 静止不动时有冷却时间
    /// 1 可以在projectSetting中设置冷却时间
    /// 2 可以改变碰撞体的位置，取消冷却时间
    /// </summary>
    /// <param name="other"> 被攻击对象</param>
    private void OnTriggerStay2D(Collider2D other)
    {
        Debug.Log(gameObject.name +"攻击了"+other.name); 
        //被攻击对象调用受伤计算
        other?.GetComponent<Character>()?.TakeDamage(this);
    }
}
