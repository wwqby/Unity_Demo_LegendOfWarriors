using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected Rigidbody2D rb;
    protected Animator animator;

    [Header("基本属性")]
    public int normalSpeed;
    public int chaseSpeed;
    public int currentSpeed;

    public Vector2 faceDir;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        currentSpeed = normalSpeed;
    }

    private void FixedUpdate()
    {
        Move();
    }


    private void Update()
    {
        faceDir = new Vector2(-transform.localScale.x, 0);
    }

    public virtual void Move()
    {
        rb.velocity = new Vector2(faceDir.x * currentSpeed * Time.fixedDeltaTime, rb.velocity.y);
    }


}
