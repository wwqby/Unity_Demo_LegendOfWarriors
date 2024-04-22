using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Boar : Enemy
{
    [Header("计时器")]
    public float timeDuration;
    public float timeTimer;
    public bool isWaiting { get { return timeTimer > 0; } }



    protected override void Update()
    {
        base.Update();
        if ((physicsCheck.isLeftWall && transform.localScale.x > 0) || (physicsCheck.isRightWall && transform.localScale.x < 0))
        {
            if (!isWaiting)
            {
                //计时器赋值
                timeTimer = timeDuration;
            }
        }
        TimerCounter();
    }

    private void TimerCounter()
    {
        if (isWaiting)
        {
            timeTimer -= Time.deltaTime;
            if (timeTimer <= 0)
            {
                //模型转身
                transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
                physicsCheck.ChangeDirection(transform);
            }
        }
    }

    override public void Move()
    {
        base.Move();
        animator.SetBool("isWalk", !isWaiting);
    }
}
