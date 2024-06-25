using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// 菜单功能
/// 新的游戏
/// 继续游戏
/// 结束游戏
/// </summary>
public class Menu : MonoBehaviour
{
    [Header("事件监听")]
    public MenuConfirmEventSO menuConfirmEventListener;

    public Button buttonNewGame;
    public Button buttonLoadGame;


    private void OnEnable() {
        //设置默认选中第一项
        EventSystem.current.SetSelectedGameObject(buttonNewGame.gameObject);
        //检查是否可见继续游戏按钮
        buttonLoadGame.gameObject.SetActive(DataManager.instance.GetLastScene().sceneReference != null);
        menuConfirmEventListener.onQuitGameAction += ExitGame;
    }

    private void OnDisable() {
        menuConfirmEventListener.onQuitGameAction -= ExitGame;
    }



    public void ExitGame()
    {
        Debug.Log("ExitGame");
        Application.Quit();
    }
}
