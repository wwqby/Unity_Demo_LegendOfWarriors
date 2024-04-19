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
    private PlayerController playerController;

    private void Awake() {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        physicsCheck = GetComponent<PhysicsCheck>();
        playerController = GetComponent<PlayerController>();
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
        animator.SetBool("isDead",playerController.isDead);
        animator.SetBool("isAttack",playerController.isAttack);
    }

    /// <summary>
    /// 触发受伤动画
    /// </summary>
    public void PlayerHurt(){
        animator.SetTrigger("hurtTrigger");
    }
    /// <summary>
    /// 触发攻击动画
    /// </summary>
    public void PlayerAttack(){
        animator.SetTrigger("attackTrigger");
    }
    
}
