using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : UICanvas
{
    public Canvas buttonOpenSound;
    public Canvas buttonRemoveSound;
    public Canvas buttonOpenVibration;
    public Canvas buttonRemoveVibration;

    public override void Setup()
    {
        GameManager.Ins.ChangeState(GameState.Pause);
    }

    public override void SetDeActive()
    {
        
    }

    public void ButtonContinueGame()
    {
        SoundController.Ins.GetbuttonAudio().Play();
        GameManager.Ins.ChangeState(GameState.Gameplay);
        UIManager.Ins.CloseUI(UIID.UIPauseGame);
    }

    public void ButtonMainMenu()
    {
        SoundController.Ins.GetbuttonAudio().Play();
        UIManager.Ins.CloseAll();
        LevelManager.Ins.ReloadGame();
        UIManager.Ins.OpenUI(UIID.MainMenu);
    }

    public void ButtonRemoveSound()
    {
        SoundController.Ins.GetbuttonAudio().Play();
        SoundController.Ins.GetinGameAudio().Stop();
        buttonRemoveSound.gameObject.SetActive(false);
        buttonOpenSound.gameObject.SetActive(true);
    }

    public void ButtonOpenSound()
    {
        SoundController.Ins.GetbuttonAudio().Play();
        SoundController.Ins.GetinGameAudio().Play();
        buttonRemoveSound.gameObject.SetActive(true);
        buttonOpenSound.gameObject.SetActive(false);
    }

    public void ButtonRemoveVibration()
    {
        SoundController.Ins.GetbuttonAudio().Play();
        buttonRemoveVibration.gameObject.SetActive(false);
        buttonOpenVibration.gameObject.SetActive(true);
    }

    public void ButtonOpenVibration()
    {
        SoundController.Ins.GetbuttonAudio().Play();
        buttonRemoveVibration.gameObject.SetActive(true);
        buttonOpenVibration.gameObject.SetActive(false);
    }
}
