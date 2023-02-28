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
        target = FindObjectOfType<BasicPlayerMovement>().transform;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void BeatHappened()
    {

        if (enemyMove.myMode == EnemyMovement.EnemyMode.Strafe && BeatSender.GiveInstance().beatCount == 0) 
        {

            enemyMove.myMode = EnemyMovement.EnemyMode.Fire;
            Invoke(nameof(ShootBullet), BeatSender.GiveInstance().secondsPerBeat);
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

    private void ShootBullet()
    {

        GameObject currentBullet = GameObject.Instantiate(prefabBullet, transform.position, transform.rotation);
        currentBullet.GetComponent<Rigidbody>().velocity = transform.forward * 8;

        Destroy(currentBullet, 2);
    }
}
