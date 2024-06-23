using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 屏幕渐入渐出效果遮罩
/// </summary>
public class Fade : MonoBehaviour
{

    public Image image;

    public FadeChangeEventSO fadeListener;

    private void OnEnable() {
        fadeListener.OnFadeChangeAction += FadeChange;
    }

    private void OnDisable() {
        fadeListener.OnFadeChangeAction -= FadeChange;
    }


    /// <summary>
    /// 渐变
    /// </summary>
    /// <param name="endValue">渐变结束颜色</param>
    /// <param name="duration">渐变开始颜色</param>
    public void FadeChange(Color endValue,float duration){
        image.DOBlendableColor(endValue,duration);
    }
}   
