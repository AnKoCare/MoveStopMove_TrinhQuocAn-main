using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : Singleton<CameraFollow>
{
    [SerializeField] private GameObject player;       

    readonly Vector3 orriginOffset = new Vector3(0,1,-1);

    public Vector3 offset;          
    
    void LateUpdate () 
    {
        transform.position = player.transform.position + offset;
    }

    public void SetupMainMenu()
    {
        offset = new Vector3(0,4,8);
        transform.localRotation = Quaternion.Euler(30f,-180f,0);
    }

    public void SetupGamePlay()
    {
        offset = new Vector3(0,20,-20);
        transform.localRotation = Quaternion.Euler(40f,0,0);
    }

    public void SetupSuitShop()
    {
        offset = new Vector3(0,2,10);
        transform.localRotation = Quaternion.Euler(30f,-180f,0);
    }

    public void SetUpWhenKill(float dis)
    {
        offset += new Vector3(0,dis,-dis);
    }

    public void SetUpWhenCollectGift()
    {
        offset *= 1.6f;
    }

    public void SetUpWinGame()
    {
        offset /= 2f;
    }
}
