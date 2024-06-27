using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [Header("菜单确认事件")]
    public MenuConfirmEventSO menuConfirmEvent;
    [Header("游戏进度管理器")]
    public GameStatusEventSO gameStatusEvent;
    [Header("音量管理器")]
    public AudioSettingEventSO audioSettingEvent;

    private void OnEnable() {
        //根据菜单修改游戏状态
        menuConfirmEvent.onPauseGameAction += gameStatusEvent.OnGamePauseRaised;
        menuConfirmEvent.onResumeGameAction += gameStatusEvent.OnGameResumeRaised;
        //todo 暂停的时候，打开音量设置
        menuConfirmEvent.onPauseGameAction += audioSettingEvent.OnAudioSettingOpendEventRaised;
    }

    private void OnDisable() {
        menuConfirmEvent.onPauseGameAction -= gameStatusEvent.OnGamePauseRaised;
        menuConfirmEvent.onResumeGameAction -= gameStatusEvent.OnGameResumeRaised;
        menuConfirmEvent.onPauseGameAction -= audioSettingEvent.OnAudioSettingOpendEventRaised;
    }
}
