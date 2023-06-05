using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIRevive : UICanvas
{
    public override void Setup()
    {
        SoundController.Ins.GetinGameAudio().Pause();
        SoundController.Ins.GetLoseAudio().Play();
    }

    public override void SetDeActive()
    {
        SoundController.Ins.GetLoseAudio().Pause();
    }
    public void ButtonReviveAsCoin()
    {
        UIManager.Ins.CloseAll();
        UIManager.Ins.OpenUI(UIID.Gameplay);
        LevelManager.Ins.RevivePlayer();
    }

    public void ButtonExit()
    {
        UIManager.Ins.CloseAll();
        UIManager.Ins.OpenUI(UIID.UIGameOver);
    }
}
