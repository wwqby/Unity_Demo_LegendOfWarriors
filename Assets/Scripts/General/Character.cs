using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
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
    [Header("受伤事件")]
    public UnityEvent<Transform> onTakeDamage;
    [Header("死亡事件")]
    public UnityEvent onDie;
    [Header("改变血量事件")]
    public UnityEvent<Character> onHealthChange;

    private void Start()
    {
        //游戏开始时，满血
        currentHealth = maxHealth;
        onHealthChange?.Invoke(this);
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
        Debug.Log(gameObject.name + "受到攻击:"+attack.damage);
        if (isInvincible)
        {
            return;
        }
        if (currentHealth - attack.damage > 0)
        {
            currentHealth -= attack.damage;
            TriggerInvincible();
            //安全调用受伤事件
            onTakeDamage?.Invoke(attack.transform);
            onHealthChange?.Invoke(this);
            return;
        }
        //人物死亡
        Debug.Log(gameObject.name + "死亡");
        currentHealth = 0;
        onDie?.Invoke();
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


    private void OnTriggerStay2D(Collider2D other) {
        if (other.CompareTag("Water")){
            currentHealth = 0;
            onHealthChange?.Invoke(this);
            onDie?.Invoke();
            return;
        }
    }
}
