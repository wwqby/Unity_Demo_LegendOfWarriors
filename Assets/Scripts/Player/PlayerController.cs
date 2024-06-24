using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    #region 成员变量
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

    [Header("物理材质")]
    public PhysicsMaterial2D ground;
    public PhysicsMaterial2D wall;
    [Header("音频")]
    public AudioDefination audioDefination;

    [Header("场景切换事件监听")]
    public SceneLoadEventSO sceneLoadEventListener;


    #endregion


    #region 周期函数

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        physicsCheck = GetComponent<PhysicsCheck>();
        inputControls = new PlayerInputControls();
        playerAnimation = GetComponent<PlayerAnimation>();
        inputControls.GamePlay.Jump.started += OnJump;
        inputControls.GamePlay.Attack.started += OnPlayerAttack;
        audioDefination = GetComponentInChildren<AudioDefination>();
    }



    private void OnEnable()
    {
        inputControls.Enable();
        sceneLoadEventListener.OnSceneLoadRequestAction += OnSceneLoadStart;
        sceneLoadEventListener.OnSceneLoadCompleteAction += OnSceneLoadEnd;
    }

    private void OnDisable()
    {
        inputControls.Disable();
        sceneLoadEventListener.OnSceneLoadRequestAction -= OnSceneLoadStart;
        sceneLoadEventListener.OnSceneLoadCompleteAction -= OnSceneLoadEnd;
    }

    private void Update()
    {
        inputDirection = inputControls.GamePlay.Move.ReadValue<Vector2>();
        UpdateMatierial();
    }


    //物理引擎更新，可以在projectSettings中设置，0.002s
    private void FixedUpdate()
    {
        //受伤禁止移动
        if (isHurt || isAttack)
        {
            return;
        }
        Move();
    }


    #endregion


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
            transform.localScale = new Vector3(direaction, 1, 1);
            //通过flip来控制,无法控制子节点方向
            // GetComponent<SpriteRenderer>().flipX = direaction == -1;
            //更新辅助点
            physicsCheck.ChangeDirection(transform);
        }
    }

    /// <summary>
    /// 跳跃事件
    /// </summary>
    /// <param name="context"></param>
    private void OnJump(InputAction.CallbackContext context)
    {
        if (physicsCheck.isGround)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            //播放音效
            audioDefination?.PlayAudioClip();
        }
    }

    /// <summary>
    ///     攻击事件
    /// </summary>
    /// <param name="context"></param>
    private void OnPlayerAttack(InputAction.CallbackContext context)
    {
        //不允许空中攻击
        if (!physicsCheck.isGround)
        {
            return;
        }
        playerAnimation.PlayerAttack();
        isAttack = true;
    }

    /// <summary>
    /// 检查更新当前物理材质
    /// </summary>
    private void UpdateMatierial()
    {
        rb.sharedMaterial = physicsCheck.isGround ? ground : wall;
    }



    #region 事件
    /// <summary>
    /// 受伤事件
    /// </summary>
    public void OnHurt(Transform attacker)
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




    /// <summary>
    /// 场景加载开始
    /// 禁止操作输入
    /// </summary>
    /// <param name="arg0"></param>
    /// <param name="arg1"></param>
    /// <param name="arg2"></param>
    private void OnSceneLoadStart(SceneSO arg0, Vector3 arg1, bool arg2)
    {
        inputControls.GamePlay.Disable();
    }

    /// <summary>
    /// 场景加载结束
    /// 打开操作输入
    /// </summary>
    private void OnSceneLoadEnd(SceneSO scene)
    {
        if (scene.sceneType == SceneType.Scene)
        {
            inputControls.GamePlay.Enable();

        }
    }


    #endregion

}
