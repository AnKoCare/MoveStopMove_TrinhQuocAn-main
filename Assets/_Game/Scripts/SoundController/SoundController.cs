using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : Singleton<SoundController>
{
    public List<AudioSource> throwWeaponAudio;
    public List<AudioSource> deadCharacterAudio;
    public List<AudioSource> scaleUpCharacterAudio;
    public List<AudioSource> weaponCollisionAudio; 
    public List<AudioSource> loseAudio;
    public List<AudioSource> backGroundAudio;
    public List<AudioSource> inGameAudio;

    public AudioSource GetthrowWeaponAudio()
    {
        int randomType = Random.Range(0,4);
        return throwWeaponAudio[randomType];
    }

    public AudioSource GetdeadCharacterAudio()
    {
        int randomType = Random.Range(0,3);
        return deadCharacterAudio[randomType];
    }

    public AudioSource GetscaleUpAudio()
    {
        int randomType = Random.Range(0,6);
        return scaleUpCharacterAudio[randomType];
    }

    public AudioSource GetweaponCollisionAudio()
    {
        int randomType = Random.Range(0,10);
        return weaponCollisionAudio[randomType];
    }

    public AudioSource GetLoseAudio()
    {
        return loseAudio[0];
    }

    public AudioSource GetbackGroundAudio()
    {
        return backGroundAudio[0];
    }

    public AudioSource GetinGameAudio()
    {
        return inGameAudio[0];
    }
}

public enum throwWeaponType
{
    throw_0 = 0,
    throw_1 = 1,
    throw_2 = 2,
    throw_3 = 3,
    throw_4 = 4
}

public enum deadCharacterType
{
    dead_0 = 0,
    dead_1 = 1,
    dead_2 = 2,
    dead_3 = 3
}

public enum scaleUpType
{
    scale_0 = 0,
    scale_1 = 1,
    scale_2 = 2,
    scale_3 = 3,
    scale_4 = 4,
    scale_5 = 5,
    scale_6 = 6
}

public enum weaponCollisionType
{
    collison_0 = 0,
    collison_1 = 1,
    collison_2 = 2,
    collison_3 = 3,
    collison_4 = 4,
    collison_5 = 5,
    collison_6 = 6,
    collison_7 = 7,
    collison_8 = 8,
    collison_9 = 9,
    collison_10 = 10,
}

public enum loseType
{
    lose_0 = 0
}

public enum backGroundType
{
    backGround_0 = 0
}

public enum inGameType
{
    inGame_0 = 0
}
