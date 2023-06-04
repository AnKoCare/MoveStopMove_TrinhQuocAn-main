using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : UICanvas
{
    public override void Setup()
    {
        base.Setup();
        GameManager.Ins.ChangeState(GameState.Pause);
    }

    public void ButtonContinueGame()
    {
        UIManager.Ins.CloseAll();
        UIManager.Ins.OpenUI(UIID.Gameplay);
        GameManager.Ins.ChangeState(GameState.Gameplay);
    }

    public void ButtonMainMenu()
    {
        UIManager.Ins.CloseAll();
        LevelManager.Ins.ReloadGame();
        UIManager.Ins.OpenUI(UIID.MainMenu);
    }
}
