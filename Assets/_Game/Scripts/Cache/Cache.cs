using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cache
{
    private static Dictionary<Collider, Character> m_CharacterList = new Dictionary<Collider, Character>();

    private static Dictionary<Collider, Obstacle> m_ObstacleList = new Dictionary<Collider, Obstacle>();

    private static Dictionary<Collider, Gift> m_GiftList = new Dictionary<Collider, Gift>();

    public static Character GetCharacter(Collider key)
    {
        if(!m_CharacterList.ContainsKey(key))
        {
            Character character = key.GetComponent<Character>();

            if(character != null)
            {
                m_CharacterList.Add(key,character);
            }
            else
            {
                return null;
            }
        }

        return m_CharacterList[key];
    }

    public static Obstacle GetObstacle(Collider key)
    {
        if(!m_ObstacleList.ContainsKey(key))
        {
            Obstacle obstacle = key.GetComponent<Obstacle>();

            if(obstacle != null)
            {
                m_ObstacleList.Add(key,obstacle);
            }
            else
            {
                return null;
            }
        }

        return m_ObstacleList[key];
    }

    public static Gift GetGift(Collider key)
    {
        if(!m_GiftList.ContainsKey(key))
        {
            Gift gift = key.GetComponent<Gift>();

            if(gift != null)
            {
                m_GiftList.Add(key,gift);
            }
            else
            {
                return null;
            }
        }

        return m_GiftList[key];
    }
}