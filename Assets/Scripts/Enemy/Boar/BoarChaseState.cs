using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 追击逻辑
/// </summary>
public class BoarChaseState : BaseState
{

    public float loseTime = 3f;
    public float loseTimer;
    public override void OnEnter(Enemy enemy)
    {
        currentEnemy = enemy;
        currentEnemy.currentSpeed = currentEnemy.chaseSpeed;
        currentEnemy.animator.SetBool("isRun", true);
        loseTimer = loseTime;
    }

    public override void OnLogicUpdate()
    {
        if ((currentEnemy.physicsCheck.isLeftWall && currentEnemy.transform.localScale.x > 0)
        || (currentEnemy.physicsCheck.isRightWall && currentEnemy.transform.localScale.x < 0)
        || !currentEnemy.physicsCheck.isGround)
        {
            //更新地面判断点
            currentEnemy.physicsCheck.ChangeDirection(currentEnemy.transform);
            //模型转身
            currentEnemy.transform.localScale = new Vector3(-currentEnemy.transform.localScale.x, currentEnemy.transform.localScale.y, currentEnemy.transform.localScale.z);
        }
        //如果没有发现玩家，倒计时3s退出
        if (!currentEnemy.FoundPlayer())
        {
            loseTimer -= Time.deltaTime;
            if (loseTimer <= 0)
            {
                currentEnemy.SwitchState(NPCState.Patrols);
            }
        }
        //否则重置倒计时
        else
        {
            loseTimer = loseTime;
        }
    }

    public override void OnPhysicsUpdate()
    {
        currentEnemy.Move();
    }

    public override void OnExit()
    {
        currentEnemy.animator.SetBool("isRun", false);
    }
}
