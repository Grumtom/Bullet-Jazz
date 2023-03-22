using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInfo : MonoBehaviour
{

    [SerializeField] Enemy mySelf;

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
        Destroy(gameObject);

    }
}
