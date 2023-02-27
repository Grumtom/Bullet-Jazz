using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{

    public enum EnemyMode
    {
        Chase,
        Strafe,
        Fire
    }
    public EnemyMode myMode;

    public float range;
    private Transform player;
    private NavMeshAgent myController;

    // Start is called before the first frame update
    void Start()
    {

        player = FindObjectOfType<PlayerControls>().transform;

    }

    // Update is called once per frame
    void Update()
    {
        if(myMode == EnemyMode.Chase)
        {

            myController.SetDestination(player.position);

            if (myController.remainingDistance < range)
            {
                myMode = EnemyMode.Strafe;
                myController.SetDestination(transform.position);
            }

        }
        else if(myMode == EnemyMode.Strafe)
        {
            if (myController.remainingDistance < 0.5f)
            {
                Vector3 posTarget = transform.position - player.position;

                posTarget = Vector3.ClampMagnitude(posTarget, 1);

                Quaternion newAngle = Quaternion.LookRotation(posTarget);

                newAngle = Quaternion.Euler(newAngle.x, newAngle.y + Random.Range(-20, 20), newAngle.z);

                posTarget = player.position + newAngle * Vector3.forward * range;

                myController.SetDestination(posTarget);
            }
        }
        else if(myMode == EnemyMode.Fire)
        {
            myController.SetDestination(transform.position);

            transform.LookAt(player.position);

        }
    }
}
