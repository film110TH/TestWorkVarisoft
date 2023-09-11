using System.Collections.Generic;
using UnityEngine;

public static class AttackPoint
{
    public static readonly Dictionary<int, Vector2> AttackPointDic = new Dictionary<int, Vector2>()
    {
        {0, new Vector2(0f,1f) },
        {1, new Vector2(-1f,1f) },
        {2, new Vector2(-1f,0f) },
        {3, new Vector2(-1f,-1f) },
        {4, new Vector2(0f,-1f) },
        {5, new Vector2(1f,-1f) },
        {6, new Vector2(1f,0f) },
        {7, new Vector2(1f,1f) },
    };

    public static Vector2 getAttackPoint(int Index)
    {
        return AttackPointDic[Index];
    }
}