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

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        physicsCheck = GetComponent<PhysicsCheck>();
        inputControls = new PlayerInputControls();
        inputControls.GamePlay.Jump.started += Jump;
            
        
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
        //TODO 空中可以无限跳跃
        //TODO 跳跃后，在空中贴墙按住方向键，可以卡在墙上不落地
        if (physicsCheck.isGround)
        {
            rb.AddForce(Vector2.up * jumpForce,ForceMode2D.Impulse);
        }
    }
}
