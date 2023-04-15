using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyFire : MonoBehaviour
{

    [SerializeField] private Transform target;
    [SerializeField] private GameObject prefabBullet;

    private EnemyMovement enemyMove;
    public bool readyToFire = false;


    public void BeatHappened()
    {

        if (readyToFire) 
        {
            SendMessage("Attack", SendMessageOptions.DontRequireReceiver);
        }

    }

}
