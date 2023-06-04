using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRange : MonoBehaviour
{ 
    public Character Owner;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(Constant.TAG_CHARACTER) && !Owner.isDead)
        {
            Character chars = Cache.GetCharacter(other);
            if (chars == Owner) return;
            if(chars.isDead) return;
            Owner.characterList.Add(chars);
            chars.onDespawnEvent += () => {Owner.characterList.Remove(chars);};
        }

        if(other.CompareTag(Constant.TAG_OBSTACLE) && Owner == LevelManager.Ins.player)
        {
            Obstacle obstacle = Cache.GetObstacle(other);
            obstacle.meshRendererObstacle.material = LevelManager.Ins.colorDataManager.GetColor(ColorsType.Transparent).Color;
        }
    }

    private void OnTriggerExit(Collider other) 
    {
        if(other.CompareTag(Constant.TAG_CHARACTER) && !Owner.isDead)
        {
            Character chars = Cache.GetCharacter(other);
            if(chars == Owner) return;
            Owner.isAttack = false;
            Owner.characterList.Remove(chars);
        }

        if(other.CompareTag(Constant.TAG_OBSTACLE) && Owner == LevelManager.Ins.player)
        {
            Obstacle obstacle = Cache.GetObstacle(other);
            obstacle.meshRendererObstacle.material = obstacle.materialOrigin;
        }
    }

}
