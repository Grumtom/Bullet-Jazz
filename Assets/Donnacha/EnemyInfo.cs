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
            TakeDamage(FindObjectOfType<Gun_Script>().comboLevels[0] + 3);
            Destroy(other.gameObject);
        }

    }

    public void TakeDamage(int damage)
    {

        mySelf.currentHp -= damage;

        GetComponent<EnemyMovement>().enemyArt.GetComponent<Animator>().SetTrigger("Take Damage");

        if (mySelf.currentHp <= 0)
            Die();
    }

    private void Die()
    {

        EnemyManager.enemyManager.RemoveEnemy(mySelf);
        BeatSender.GiveInstance().recivers.Remove(GetComponent<BeatReciver>());
        Destroy(transform.parent.gameObject);

    }
}
