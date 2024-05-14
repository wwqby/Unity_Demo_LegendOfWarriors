using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoarPotrolState : BaseState
{

    public float timeDuration = 5f;
    public float timeTimer;
    public bool isWaiting { get { return timeTimer > 0; } }
    public override void OnEnter(Enemy enemy)
    {
        currentEnemy = enemy;
        currentEnemy.currentSpeed = currentEnemy.normalSpeed;
    }

    public override void OnLogicUpdate()
    {
        //TODO 切换到追击状态
        if (currentEnemy.FoundPlayer())
        {
            currentEnemy.SwitchState(NPCState.Chase);
            return;
        }
        // 正常巡逻
        if ((currentEnemy.physicsCheck.isLeftWall && currentEnemy.transform.localScale.x > 0)
        || (currentEnemy.physicsCheck.isRightWall && currentEnemy.transform.localScale.x < 0)
        || !currentEnemy.physicsCheck.isGround)
        {
            if (!isWaiting)
            {
                //计时器赋值
                timeTimer = timeDuration;
            }
        }
        TimerCounter();
        currentEnemy.animator.SetBool("isWalk", !isWaiting);
    }

    public override void OnPhysicsUpdate()
    {
        if (isWaiting)
        {
            currentEnemy.Stop();
            return;
        }
        currentEnemy.Move();
    }

    public override void OnExit()
    {
        currentEnemy.animator.SetBool("isWalk", false);
    }


    private void TimerCounter()
    {
        if (isWaiting)
        {
            //TODO 给切换左右留下0.5的过程时间 这里很重要，有可能会出现悬崖边回头，然后原地不动，因为地形判断触发了计时器启动
            if (timeTimer > 0.5 && timeTimer - Time.deltaTime <= 0.5)
            {
                //更新地面判断点
                currentEnemy.physicsCheck.ChangeDirection(currentEnemy.transform);
                //模型转身
                currentEnemy.transform.localScale = new Vector3(-currentEnemy.transform.localScale.x, currentEnemy.transform.localScale.y, currentEnemy.transform.localScale.z);
            }
            timeTimer -= Time.deltaTime;
        }
    }
}
