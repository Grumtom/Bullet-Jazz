using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInfo : MonoBehaviour
{

    public Enemy mySelf;

    private void Start()
    {

        EnemyManager.enemyManager.AddEnemy(mySelf);
        mySelf.currentHp = mySelf.maxHp;

    }

    private void OnTriggerEnter(Collider other)
    {

        if(other.gameObject.layer == LayerMask.NameToLayer("Player Bullet"))
        {
            TakeDamage(1);
            Destroy(other.gameObject);
        }

    }

    public void TakeDamage(int damage)
    {

        mySelf.currentHp -= damage;

        if (mySelf.currentHp <= 0)
            Die();

    }

    private void Die()
    {

        EnemyManager.enemyManager.RemoveEnemy(mySelf);
        Destroy(transform.parent.gameObject);

    }
}
