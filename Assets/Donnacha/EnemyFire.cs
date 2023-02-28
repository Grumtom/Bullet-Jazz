using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyFire : MonoBehaviour
{

    [SerializeField] private Transform target;
    [SerializeField] private GameObject prefabBullet;

    private EnemyMovement enemyMove;

    private void Awake()
    {
        enemyMove = GetComponent<EnemyMovement>();
    }

    // Update is called once per frame
    void Update()
    {

        transform.LookAt(target);

        
    }

    public void BeatHappened()
    {

        if (enemyMove.myMode == EnemyMovement.EnemyMode.Strafe && BeatSender.GiveInstance().beatCount == 0) 
        {
            GameObject.Instantiate(prefabBullet, transform.position, transform.rotation).GetComponent<Rigidbody>().velocity = transform.forward * 8;
            enemyMove.myMode = EnemyMovement.EnemyMode.Fire;
        }
        else if(GetComponent<NavMeshAgent>().remainingDistance > enemyMove.range)
        {
            GetComponent<EnemyMovement>().myMode = EnemyMovement.EnemyMode.Chase;
        }
        else
        {
            enemyMove.myMode = EnemyMovement.EnemyMode.Strafe;
        }

    }
}
