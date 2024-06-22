using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 宝箱
/// </summary>
public class Chest : MonoBehaviour, IInteractable
{
    [Header("打开/关闭图片")]
    public Sprite chestOpen;
    public Sprite chestClose;
    [Header("图片渲染组件")]
    public SpriteRenderer spriteRenderer;
    [Header("是否打开")]
    public bool isOpen = false;

    public AudioDefination audioDefination;


    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioDefination = GetComponent<AudioDefination>();
    }

    private void OnEnable()
    {
        spriteRenderer.sprite = isOpen ? chestOpen : chestClose;
    }


    public void OnInteractableConfirm()
    {
        if (isOpen)
        {
            return;
        }
        //修改宝箱图片
        spriteRenderer.sprite = chestOpen;
        //修改宝箱状态
        isOpen = true;
        //todo 播放声音，生成宝物
        audioDefination?.PlayAudioClip();
    }

    bool IInteractable.CanInteractable()
    {   
        //已经打开就不可以继续交互
        return !isOpen;
    }
}
