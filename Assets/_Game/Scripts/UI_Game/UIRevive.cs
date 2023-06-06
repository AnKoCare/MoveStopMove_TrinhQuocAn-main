using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIRevive : UICanvas
{
    public override void Setup()
    {
        SoundController.Ins.GetinGameAudio().Stop();
        SoundController.Ins.GetLoseAudio().Play();
    }

    public override void SetDeActive()
    {
        base.SetDeActive();
        SoundController.Ins.GetLoseAudio().Stop();
    }
    public void ButtonReviveAsCoin()
    {
        SoundController.Ins.GetbuttonAudio().Play();
        UIManager.Ins.CloseAll();
        UIManager.Ins.OpenUI(UIID.Gameplay);
        LevelManager.Ins.RevivePlayer();
    }

    public void ButtonExit()
    {
        SoundController.Ins.GetbuttonAudio().Play();
        UIManager.Ins.CloseAll();
        UIManager.Ins.OpenUI(UIID.UIGameOver);
    }
}
