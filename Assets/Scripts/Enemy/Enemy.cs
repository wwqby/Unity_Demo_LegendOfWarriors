using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator animator;
    public PhysicsCheck physicsCheck;

    [Header("基本属性")]
    public int normalSpeed;
    public int chaseSpeed;
    public int currentSpeed;

    public Vector2 faceDir;
    public float hurtForce;

    [Header("状态")]
    public bool isHurt;
    public bool isDead;

    [Header("检测")]
    public Vector2 castOffset;
    public Vector2 boxSize;
    public float castDistance;
    public LayerMask castLayer;
    /// <summary>
    /// 当前行为状态
    /// </summary>
    protected BaseState currentState;


    #region 生命周期方法

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        physicsCheck = GetComponent<PhysicsCheck>();
        currentSpeed = normalSpeed;
    }
    protected void OnEnable()
    {
        currentState.OnEnter(this);
    }
    protected virtual void Update()
    {
        //怪物默认向左，所以取scale的反方向
        faceDir = new Vector2(-transform.localScale.x, 0);
        currentState.OnLogicUpdate();
    }
    protected virtual void FixedUpdate()
    {
        currentState.OnPhysicsUpdate();
    }

    protected virtual void OnDisable()
    {
        currentState.OnExit();
    }

    #endregion

    #region  私有方法

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere((Vector2)transform.position + castOffset, 0.2f);
        Gizmos.DrawRay((Vector2)transform.position + castOffset, transform.TransformDirection(faceDir) * castDistance);
    }

    #endregion

    #region 公共成员方法
    /// <summary>
    /// 移动
    /// </summary>
    public virtual void Move()
    {
        if (isHurt || isDead)
        {
            return;
        }
        rb.velocity = new Vector2(faceDir.x * currentSpeed * Time.fixedDeltaTime, rb.velocity.y);
    }
    /// <summary>
    /// 停止移动
    /// </summary>
    public void Stop()
    {
        rb.velocity = Vector2.zero;
    }

    public bool FoundPlayer()
    {
        return Physics2D.BoxCast(transform.position + (Vector3)castOffset, boxSize, 0, faceDir, castDistance, castLayer);
    }

    public void SwitchState(NPCState state)
    {
        BaseState newState = state switch
        {
            NPCState.Patrols => new BoarPotrolState(),
            NPCState.Chase => new BoarChaseState(),
            _ => null
        };
        currentState.OnExit();
        currentState = newState;
        currentState.OnEnter(this);
    }
    #endregion

    #region  事件方法

    /// <summary>
    /// 受伤事件
    /// </summary>
    /// <param name="attacker">攻击对象</param>
    public virtual void OnTakeDamage(Transform attacker)
    {
        //设置受伤状态和受伤动画
        isHurt = true;
        animator.SetTrigger("hurt");
        //设置面朝攻击对象
        var direction = new Vector2(transform.position.x - attacker.position.x, 0).normalized;
        transform.localScale = new Vector3(direction.x, 1, 1);
        //给被攻击对象击退效果
        rb.velocity = Vector2.zero;
        rb.AddForce(direction * hurtForce, ForceMode2D.Impulse);
        StartCoroutine(HurtAnimationEnd());
    }

    public virtual void OnDie()
    {
        gameObject.layer = 2;
        animator.SetBool("isDead", true);
        isDead = true;
    }


    public virtual void OnDestroy()
    {
        Destroy(gameObject);
    }

    private IEnumerator HurtAnimationEnd()
    {
        yield return new WaitForSeconds(1.0f);
        isHurt = false;
    }

    #endregion
}
