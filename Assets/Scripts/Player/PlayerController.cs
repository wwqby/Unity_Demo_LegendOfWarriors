using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public PlayerInputControls inputControls;
    [Header("Movement")]
    public Vector2 inputDirection;
    public float speed;

    public float jumpForce;



    private Rigidbody2D rb;
    //物理检测组件
    private PhysicsCheck physicsCheck;
    private PlayerAnimation playerAnimation;
    [Header("状态")]
    public bool isAttack;
    public bool isHurt;
    public float hurtForce;
    public bool isDead;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        physicsCheck = GetComponent<PhysicsCheck>();
        inputControls = new PlayerInputControls();
        playerAnimation = GetComponent<PlayerAnimation>();
        inputControls.GamePlay.Jump.started += Jump;
        inputControls.GamePlay.Attack.started += PlayerAttack;

    }



    private void OnEnable()
    {
        inputControls.Enable();
    }

    private void OnDisable()
    {
        inputControls.Disable();
    }

    private void Update()
    {
        inputDirection = inputControls.GamePlay.Move.ReadValue<Vector2>();
    }
    //物理引擎更新，可以在projectSettings中设置，0.002s
    private void FixedUpdate()
    {
        //受伤禁止移动
        if (isHurt)
        {
            return;
        }
        Move();
    }
    //移动
    private void Move()
    {
        //设置物理组件的水平熟读
        rb.velocity = new Vector2(inputDirection.x * speed * Time.fixedDeltaTime, rb.velocity.y);
        //控制模型方向
        if (inputDirection.x != 0)
        {
            int direaction = inputDirection.x > 0 ? 1 : -1;
            //通过scale控制方向
            // transform.localScale = new Vector3(direaction, 1, 1);
            //通过flip来控制
            GetComponent<SpriteRenderer>().flipX = direaction == -1;
        }
    }

    //跳跃
    private void Jump(InputAction.CallbackContext context)
    {
        if (physicsCheck.isGround)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    /// <summary>
    ///     攻击事件
    /// </summary>
    /// <param name="context"></param>
    private void PlayerAttack(InputAction.CallbackContext context)
    {
        playerAnimation.PlayerAttack();
        isAttack = true;
    }

    /// <summary>
    /// 受伤事件
    /// </summary>
    public void GetHurt(Transform attacker)
    {
        //更新受伤标志
        isHurt = true;
        //计算反弹力
        var direaction = new Vector2(transform.position.x - attacker.position.x, 0).normalized;
        rb.velocity = Vector2.zero;//取消惯性速度
        rb.AddForce(direaction * hurtForce, ForceMode2D.Impulse);
    }

    public void OnDie()
    {
        isDead = true;
        inputControls.GamePlay.Disable();
    }
}
