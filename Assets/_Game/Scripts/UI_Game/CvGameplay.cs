using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CvGameplay : UICanvas
{
    [SerializeField] private Transform NoticeHold;
    [SerializeField] private NoticeKiller PrefabsNotice;
    

    public override void Setup()
    {
        
        base.Setup();
        
        GameManager.Ins.ChangeState(GameState.Gameplay);
        CameraFollow.Ins.SetupGamePlay();
        SoundController.Ins.GetbackGroundAudio().Pause();
        if(!UIManager.Ins.GetUI<PauseGame>(UIID.UIPauseGame).buttonOpenSound.gameObject.activeSelf && UIManager.Ins.GetUI<PauseGame>(UIID.UIPauseGame).buttonRemoveSound.gameObject.activeSelf)
        {
            UIManager.Ins.CloseUI(UIID.UIPauseGame);
            SoundController.Ins.GetinGameAudio().Play();
        }
        else
        {
            UIManager.Ins.CloseUI(UIID.UIPauseGame);
            SoundController.Ins.GetinGameAudio().Stop();
        }
    }

    public override void SetDeActive()
    {
        SoundController.Ins.GetinGameAudio().Stop();
    }

    public void ButtonSettingGame()
    {
        
        SoundController.Ins.GetbuttonAudio().Play();
        if(LevelManager.Ins.player.isDead) return;
        LevelManager.Ins.player.ChangeState(new IdleState());
        //LevelManager.Ins.PauseGame();
        UIManager.Ins.OpenUI(UIID.UIPauseGame);
    }

    public void SpawnNotice(string killer, string victim)
    {
        NoticeKiller noticeKiller = SimplePool.Spawn<NoticeKiller>(PoolType.NoticeKiller);
        noticeKiller.nameKiller.text = killer;
        noticeKiller.nameVictim.text = victim;
        noticeKiller.TF.SetParent(NoticeHold);
    }

    
}
