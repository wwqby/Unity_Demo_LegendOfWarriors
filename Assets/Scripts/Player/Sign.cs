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
    public bool isInteractable = false;

    public PlayerInputControls playerInputControls;

    public IInteractable interactableObject;

    #region 生命周期方法
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
        playerInputControls.GamePlay.Confirm.performed += OnConfirm;
    }

    private void OnDisable()
    {
        InputSystem.onActionChange -= onActionChange;
        playerInputControls.GamePlay.Confirm.performed -= OnConfirm;
    }



    void Update()
    {
        //设置交互标志的可见性
        spriteRenderer.enabled = isInteractable;
        //设置交互标志的方向
        transform.localScale = playerTransform.localScale;
    }

    #endregion


    #region 碰撞检测
    /// <summary>
    /// 检测到交互物体
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerStay2D(Collider2D other)
    {
        interactableObject = other.GetComponent<IInteractable>();
        //根据接口状态判断是否可以交互
        isInteractable = interactableObject.CanInteractable();
    }
    /// <summary>
    /// 离开检测范围
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerExit2D(Collider2D other)
    {
        isInteractable = false;
        interactableObject = null;
    }

    #endregion


    /// <summary>
    /// 检测到交互按键变化
    /// </summary>
    /// <param name="action"></param>
    /// <param name="change"></param>
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
    /// <summary>
    /// 实现确认交互效果
    /// </summary>
    /// <param name="context"></param>
    /// <exception cref="NotImplementedException"></exception>
    private void OnConfirm(InputAction.CallbackContext context)
    {
        if (isInteractable)
        {
            interactableObject?.OnInteractableConfirm();
        }
    }
}
