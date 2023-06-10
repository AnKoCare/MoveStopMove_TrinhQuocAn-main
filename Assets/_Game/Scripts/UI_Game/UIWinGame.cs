using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class UIWinGame : UICanvas
{
    public TextMeshProUGUI earnCoin;

    public override void Setup()
    {
        GameManager.Ins.ChangeState(GameState.WinGame);
        LevelManager.Ins.player.boxCollider.enabled = false;
        LevelManager.Ins.player.ChangeState(new Dance_Win());
        CameraFollow.Ins.SetUpWinGame();
        SoundController.Ins.GetwinGameAudio().Play();
        LevelManager.Ins.player.TF.rotation = Quaternion.Euler(0f,0f,0f);
        earnCoin.text = "" + LevelManager.Ins.player.numberKillBot * LevelManager.Ins.player.rateUpGold;
    }

    public void ButtonContinue()
    {
        SoundController.Ins.GetbuttonAudio().Play();
        UIManager.Ins.CloseAll();
        UIManager.Ins.OpenUI(UIID.MainMenu);
        LevelManager.Ins.ReloadGame();
    }

    public override void SetDeActive()
    {
        SoundController.Ins.GetwinGameAudio().Stop();
        LevelManager.Ins.player.ChangeState(new IdleState());
    }
}
