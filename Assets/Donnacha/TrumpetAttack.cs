using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class TrumpetAttack : MonoBehaviour
{

    [SerializeField] int beatPoint;
    [SerializeField] GameObject bullet;
    private Transform player;
    [SerializeField] List<BulletPattern> bulletPattern = new();
    [SerializeField] float fireSpeed = 8;
    private EnemyMovement myMover;

    // Start is called before the first frame update
    void Start()
    {
        myMover = GetComponent<EnemyMovement>();

        player = FindObjectOfType<PlayerControls>().transform;
    }

    public void Attack()
    {
        if (beatPoint == 0)
        {
            beatPoint++;
            myMover.myMode = EnemyMovement.EnemyMode.Fire;
            myMover.enemyArt.GetComponent<Animator>().SetTrigger("Fire");
        }
        else if (beatPoint < bulletPattern.Count + 1)
        {
            Fire(bulletPattern[beatPoint - 1]);
            beatPoint++;
        }
        else
        {
            myMover.myMode = EnemyMovement.EnemyMode.Chase;
            GetComponent<EnemyFire>().readyToFire = false;
            beatPoint = 0;
        }
    }

    private void Fire(BulletPattern pattern)
    {
        float initialAngle = pattern.angleBetween * pattern.count * 0.5f;

        myMover.enemyArt.transform.rotation = transform.position.x < player.position.x ? Quaternion.Euler(0, 0, 180) : Quaternion.Euler(0, 0, 0);

        for (int i = 0; i < pattern.count; i++)
        {
            Vector3 direction = player.position - transform.position;

            direction.y = 0;

            Quaternion angleToPlayer = Quaternion.LookRotation(direction, transform.up);

            angleToPlayer *= Quaternion.Euler(0, pattern.angleBetween * i - initialAngle, 0);

            Vector3 posTarget = angleToPlayer * Vector3.forward * 0.6f;

            GameObject newBullet = GameObject.Instantiate(bullet, posTarget + transform.position, angleToPlayer);

            newBullet.GetComponent<Rigidbody>().velocity = newBullet.transform.forward * fireSpeed;
        }
    }

    [System.Serializable]
    struct BulletPattern
    {
        public int count;
        public int angleBetween;
    }

}
