using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    /// <summary>
    /// 当前行为状态
    /// </summary>
    protected BaseState currentState;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        physicsCheck = GetComponent<PhysicsCheck>();
        currentSpeed = normalSpeed;
    }
    protected void OnEnable() {
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

    protected virtual void OnDisable() {
        currentState.OnExit();
    }

    public virtual void Move()
    {
        if (isHurt || isDead )
        {
            return;
        }
        rb.velocity = new Vector2(faceDir.x * currentSpeed * Time.fixedDeltaTime, rb.velocity.y);
        Debug.Log("Move:" + rb.velocity.x);
    }

    public void Stop(){
        rb.velocity = Vector2.zero;
    }

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


    public virtual void OnDestroy() {
        Destroy(gameObject);
    }

    private IEnumerator HurtAnimationEnd()
    {
        yield return new WaitForSeconds(0.4f);
        isHurt = false;
    }
}
