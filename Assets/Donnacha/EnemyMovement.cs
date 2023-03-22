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
    public Transform player;
    private NavMeshAgent myController;
    [SerializeField] GameObject enemyArt;
    [SerializeField] bool enemyFacePlayer = false;
    [SerializeField] Transform enemyTurner;

    // Start is called before the first frame update
    void Awake()
    {

        player = FindObjectOfType<PlayerControls>().transform;
        myController = GetComponent<NavMeshAgent>();

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

                newAngle = Quaternion.Euler(newAngle.x, newAngle.y + Random.Range(-100, 100), newAngle.z);

                Vector3 directional = NearestDirectional();

                posTarget = player.position + newAngle * directional * range;

                myController.SetDestination(posTarget);
            }

            if(Vector3.Distance(transform.position, player.position) > range)
            {
                myMode = EnemyMode.Strafe;
            }
        }
        else if(myMode == EnemyMode.Fire)
        {
            myController.SetDestination(transform.position);

            if(enemyFacePlayer)
                transform.LookAt(player.position);

        }
    }

    private void FixedUpdate()
    {

        enemyArt.transform.position = transform.position;

        float rotation = Vector3.Angle(transform.forward, Vector3.right);
        float product = Vector3.Dot(transform.forward.normalized, Vector3.back.normalized);

        if (product > 0)
            rotation *= -1;

        enemyTurner.localRotation = Quaternion.identity;
        enemyTurner.Rotate(Vector3.forward, rotation);

    }

    private Vector3 NearestDirectional()
    {

        Vector3 direction = Vector3.right;

        float smallestDistance = Vector3.Distance(player.position + direction * range, transform.position);

        if(Vector3.Distance(player.position + Vector3.forward * range, transform.position) < smallestDistance)
        {
            direction = Vector3.forward;
            smallestDistance = Vector3.Distance(player.position + Vector3.forward * range, transform.position);
        }

        if (Vector3.Distance(player.position + Vector3.left * range, transform.position) < smallestDistance)
        {
            direction = Vector3.left;
            smallestDistance = Vector3.Distance(player.position + Vector3.left * range, transform.position);
        }

        if (Vector3.Distance(player.position + Vector3.back * range, transform.position) < smallestDistance)
        {
            direction = Vector3.back;
        }

        return direction;

    }
}
