using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
/// <summary>
/// 单位属性
/// </summary>
public class Character : MonoBehaviour, ISavable
{
    [Header("基本属性")]
    public float maxHealth;
    public float currentHealth;
    [Header("受伤无敌")]
    public float invincibleDuration;
    private float invincibleTimer;
    //get属性
    public bool isInvincible { get { return invincibleTimer > 0; } }
    [Header("菜单事件")]
    public MenuConfirmEventSO menuConfirmListener;
    public SceneLoadEventSO sceneLoadListener;
    [Header("受伤事件")]
    public UnityEvent<Transform> onTakeDamage;
    [Header("死亡事件")]
    public UnityEvent onDie;
    [Header("改变血量事件")]
    public UnityEvent<Character> onHealthChange;


    private void OnEnable()
    {
        if (DataManager.instance.isNew)
        {
            OnNewGame();
        }
        ISavable savable = this;
        savable.RigisterData();
    }

    private void OnDisable()
    {
        ISavable savable = this;
        savable.UnRigisterData();
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
        Debug.Log(gameObject.name + "受到攻击:" + attack.damage);
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


    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Water"))
        {
            //已经死亡旧跳过
            if (currentHealth == 0)
            {
                return;
            }
            //判定死亡
            currentHealth = 0;
            Debug.Log("落水死亡");
            onHealthChange?.Invoke(this);
            onDie?.Invoke();
            return;
        }
    }

    /// <summary>
    /// 监听菜单新游戏
    /// </summary>
    public void OnNewGame()
    {
        currentHealth = maxHealth;
        onHealthChange?.Invoke(this);
        Debug.Log("新游戏" + gameObject.name + "血量："+ currentHealth);
    }


    public DataDefination GetDataDefination()
    {
        return GetComponent<DataDefination>();
    }

    public void SaveData(DataModel dataModel)
    {
        if (dataModel.dataDict.ContainsKey(GetDataDefination().ID))
        {
            dataModel.dataDict[GetDataDefination().ID] = this.transform.position;
            dataModel.floatDataDict[GetDataDefination().ID + DataModel.HEALTH] = currentHealth;
        }
        else
        {
            dataModel.dataDict.Add(GetDataDefination().ID, this.transform.position);
            dataModel.floatDataDict.Add(GetDataDefination().ID + DataModel.HEALTH, currentHealth);
        }
    }

    public void LoadData(DataModel dataModel)
    {
        if (dataModel.dataDict.ContainsKey(GetDataDefination().ID))
        {
            this.transform.position = dataModel.dataDict[GetDataDefination().ID];
            currentHealth = dataModel.floatDataDict[GetDataDefination().ID + DataModel.HEALTH];
            Debug.Log("加载数据_血量" + currentHealth);
            onHealthChange?.Invoke(this);
        }
    }



}
