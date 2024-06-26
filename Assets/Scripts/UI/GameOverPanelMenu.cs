using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameOverPanelMenu : MonoBehaviour
{

    public GameObject continueGameButton;

    private void OnEnable() {
        Debug.Log("OnGameOver:choose restart");
        EventSystem.current.SetSelectedGameObject(continueGameButton);
    }

}
