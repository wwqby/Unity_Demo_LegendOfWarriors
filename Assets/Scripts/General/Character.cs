using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 单位属性
/// </summary>
public class Character : MonoBehaviour
{
    [Header("基本属性")]
    public float maxHealth;
    public float currentHealth;
    [Header("受伤无敌")]
    public float invincibleDuration;
    private float invincibleTimer;
    //get属性
    public bool isInvincible { get { return invincibleTimer > 0; } }

    private void Start()
    {
        //游戏开始时，满血
        currentHealth = maxHealth;
    }

    private void Update()
    {
        //更新计时器
        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
        }

    }

    /// <summary>
    /// 收到攻击事件
    /// </summary>
    /// <param name="attack"> 受攻击对象</param>
    public void TakeDamage(Attack attack)
    {
        if (isInvincible)
        {
            return;
        }
        if (currentHealth <= 0)
        {
            //人物死亡
            Debug.Log(gameObject.name + "死亡");
            return;
        }
        currentHealth -= attack.damage;
        TriggerInvincible();
    }
    /// <summary>
    /// 触发无敌，打开无敌时间计时器
    /// </summary>
    private void TriggerInvincible()
    {
        if (invincibleTimer <= 0)
        {
            invincibleTimer = invincibleDuration;
        }
    }
}
