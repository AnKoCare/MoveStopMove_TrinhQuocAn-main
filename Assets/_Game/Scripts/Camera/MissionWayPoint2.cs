using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MissionWayPoint2 : GameUnit
{
    public Character owner;
    public Image imgPoint;
    public Transform target;
    public Vector3 offset;
    public Vector3 pos;
    public TextMeshProUGUI level;
    public TextMeshProUGUI nameCharacter;
    private Transform TFImg;

    private void Awake() 
    {
       TFImg = imgPoint.transform; 
    }

    void Update()
    {
        if(GameManager.Ins.IsState(GameState.MainMenu)) return;
        level.text = "" + owner.LevelCharacter; 
        nameCharacter.text = "" + owner.nameChar;
        float minX = imgPoint.GetPixelAdjustedRect().width / 2 + 75f;
        float maxX = Screen.width - minX;

        float minY = imgPoint.GetPixelAdjustedRect().height / 2 + 75f;
        float maxY = Screen.height - minY;

        pos = Camera.main.WorldToScreenPoint(target.position + offset);

        if (pos.z < 0)
        {
            pos.y = Screen.height - pos.y;
            pos.x = Screen.width - pos.x;
        }

        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        pos.y = Mathf.Clamp(pos.y, minY, maxY);
        pos.z = 0;
        TFImg.position = Vector3.Lerp(pos, TFImg.position, 0.9f);

        if(pos.x == minX || pos.x == maxX || pos.y == minY || pos.y == maxY)
        {

            nameCharacter.enabled = false;
        }
        else
        {

            nameCharacter.enabled = true;
        }
    }

    public override void OnDespawn()
    {
        TF.position = Vector3.zero;
        TFImg.position = Vector3.zero;
        offset = Vector3.up * 3f;
    }

    public override void OnInit()
    {

    }

    public void OnInit(Character character)
    {
        owner = character;
        TFImg.position = Vector3.zero;
        offset = Vector3.up * 3f;
        offset += Vector3.up * (owner.LevelCharacter * 0.5f);
    }
    public void Setoffset(float size)
    {
        offset += Vector3.up * size;
    }
}
