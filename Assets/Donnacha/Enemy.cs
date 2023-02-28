using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Enemy
{

    public enum EnemyType
    {
        Drum,
        Trumpet,
        Piano,
        String
    }
    public EnemyType enemyType;

    public GameObject enemy;

    public int maxHp = 3;
    public int currentHp;

}
