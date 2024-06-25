using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePoint : MonoBehaviour, IInteractable
{

    public SpriteRenderer saveMark;
    public GameObject lightObj;
    public Sprite darkMark;
    public Sprite lightMark;
    public bool isDone;
    public DataEventSO dataEventBroadcast;


    private void Awake()
    {
        saveMark.sprite = isDone ? lightMark : darkMark;
        lightObj.SetActive(isDone);
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
        lightObj.SetActive(isDone);
        //todo 保存进度
        dataEventBroadcast.OnDataSaveEventRaised();
    }


}
