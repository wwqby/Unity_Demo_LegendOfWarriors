using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// 控制交互逻辑
/// 当碰撞体检测interactable为true时，可以进行交互
/// </summary>
public class Sign : MonoBehaviour
{

    public SpriteRenderer spriteRenderer;
    public Transform playerTransform;
    public Animator animator;
    public bool interactable = false;

    public PlayerInputControls playerInputControls;
    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();
        playerTransform = transform.parent;
        playerInputControls = new PlayerInputControls();
        playerInputControls.Enable();
    }

    private void OnEnable()
    {
        InputSystem.onActionChange += onActionChange;
    }

    private void OnDisable()
    {
        InputSystem.onActionChange -= onActionChange;
    }

    private void onActionChange(object action, InputActionChange change)
    {
        if (change == InputActionChange.ActionStarted)
        {
            var device = ((InputAction)(action)).activeControl.device;
            switch (device)
            {
                case Keyboard:
                    animator.Play("Sign_Keyboard");
                    break;
                case Gamepad:
                    animator.Play("Sign_Gamepad");
                    break;
            }
        }

    }

    void Update()
    {
        //设置交互标志的可见性
        spriteRenderer.enabled = interactable;
        //设置交互标志的方向
        transform.localScale = playerTransform.localScale;
    }

    /// <summary>
    /// 检测到交互物体
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        interactable = true;
    }
    /// <summary>
    /// 离开检测范围
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerExit2D(Collider2D other)
    {
        interactable = false;
    }
}
