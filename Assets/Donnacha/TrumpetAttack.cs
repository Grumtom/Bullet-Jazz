using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrumpetAttack : MonoBehaviour
{

    [SerializeField] int beatPoint;
    [SerializeField] GameObject bullet;
    [SerializeField] Transform firingPoint;
    [SerializeField] List<BulletPattern> bulletPattern = new();
    private EnemyMovement myMover;

    // Start is called before the first frame update
    void Start()
    {
        myMover = GetComponent<EnemyMovement>();
    }

    public void Attack()
    {
        if (beatPoint == 0)
        {
            beatPoint++;
            myMover.myMode = EnemyMovement.EnemyMode.Fire;
        }
        else if (beatPoint < bulletPattern.Count)
        {
            Fire(bulletPattern[beatPoint-1]);
            beatPoint++;
        }
        else
            myMover.myMode = EnemyMovement.EnemyMode.Chase;
    }

    private void Fire(BulletPattern pattern)
    {
        float initialAngle = pattern.angleBetween * pattern.count * 0.5f;

        for(int i = 0; i < pattern.count; i++)
        {
            GameObject newBullet = GameObject.Instantiate(bullet, firingPoint.position,
                firingPoint.rotation * Quaternion.Euler(0, initialAngle - pattern.angleBetween * i, 0));
        }
    }

    [System.Serializable]
    struct BulletPattern
    {
        public int count;
        public int angleBetween;
    }

}
