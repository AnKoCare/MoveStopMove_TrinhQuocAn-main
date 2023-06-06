using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainMenu : UICanvas
{
    public TMP_InputField InputField;
    public TextMeshProUGUI InputName;
    public string PlayerName;
    public Canvas buttonRemoveSound;
    public Canvas buttonOpenSound;

    public override void Setup()
    {
        LevelManager.Ins.player.RemoveHair();
        LevelManager.Ins.player.RemoveWeapon();
        LevelManager.Ins.player.RemovePant();
        LevelManager.Ins.player.RemoveSupportItem();
        LevelManager.Ins.player.RemoveCharacterColor();
        LevelManager.Ins.player.RemoveTailItem();
        LevelManager.Ins.player.RemoveWingItem();
        base.Setup();
        GameManager.Ins.ChangeState(GameState.MainMenu);
        CameraFollow.Ins.SetupMainMenu();
        LevelManager.Ins.player.ChangeState(new IdleState());
        InputField.onEndEdit.AddListener(SetName);
        if(!buttonOpenSound.gameObject.activeSelf && buttonRemoveSound.gameObject.activeSelf)
        {
            SoundController.Ins.GetbackGroundAudio().Play();
        }
        else
        {
            SoundController.Ins.GetbackGroundAudio().Stop();
        }
    }

    public void SetName(string text)
    {
        PlayerName = InputName.text;
        LevelManager.Ins.player.nameChar = PlayerName;
    }

    public override void SetDeActive()
    {
        base.SetDeActive();
    }

    public void ButtonWeapon()
    {
        SoundController.Ins.GetbuttonAudio().Play();
        UIManager.Ins.CloseAll();
        UIManager.Ins.OpenUI(UIID.WeaponShop);
    }

    public void ButtonSkin()
    {
        SoundController.Ins.GetbuttonAudio().Play();
        UIManager.Ins.CloseAll();
        UIManager.Ins.OpenUI(UIID.HatShop);
    }

    public void ButtonPlay()
    {
        SoundController.Ins.GetbuttonAudio().Play();
        LevelManager.Ins.player.nameChar = PlayerName;
        UIManager.Ins.CloseAll();
        UIManager.Ins.OpenUI(UIID.Gameplay);
        GameManager.Ins.ChangeState(GameState.Gameplay);
    }

    public void ButtonRemoveSound()
    {
        SoundController.Ins.GetbuttonAudio().Play();
        SoundController.Ins.GetbackGroundAudio().Pause();
        buttonRemoveSound.gameObject.SetActive(false);
        buttonOpenSound.gameObject.SetActive(true);
    }

    public void ButtonOpenSound()
    {
        SoundController.Ins.GetbuttonAudio().Play();
        SoundController.Ins.GetbackGroundAudio().Play();
        buttonRemoveSound.gameObject.SetActive(true);
        buttonOpenSound.gameObject.SetActive(false);
    }
}
