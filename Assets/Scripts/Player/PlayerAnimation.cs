using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 角色自身的动画控制
/// </summary>
public class PlayerAnimation : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rb;
    private PhysicsCheck physicsCheck;

    private void Awake() {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        physicsCheck = GetComponent<PhysicsCheck>();
    }

    private void Update() {
        SetAnmimation();
    }

    /// <summary>
    /// 设置动画控制参数
    /// </summary>
    private void SetAnmimation()
    {   
        //设置人物移动速度
        animator.SetFloat("velocityX",Mathf.Abs(rb.velocity.x));
        animator.SetFloat("velocityY",rb.velocity.y);
        //设置人物是否在地面
        animator.SetBool("isGround",physicsCheck.isGround);
    }
}
