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

    private void OnTriggerStay2D(Collider2D other) {
        //被攻击对象调用受伤计算
        other?.GetComponent<Character>().TakeDamage(this);
    }
}
