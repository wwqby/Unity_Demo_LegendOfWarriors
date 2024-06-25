using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePoint : MonoBehaviour, IInteractable
{

    public SpriteRenderer saveMark;
    public Sprite darkMark;
    public Sprite lightMark;
    public bool isDone;


    private void Awake()
    {
        saveMark.sprite = isDone ? lightMark : darkMark;
    }

    public bool CanInteractable()
    {
        return !isDone;
    }

    public void OnInteractableConfirm()
    {
        if(isDone){
            return;
        }
        isDone = true;
        saveMark.sprite = lightMark;
        //todo 保存进度
    }


}
