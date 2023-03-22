using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{

    [SerializeField] List<Enemy> enemies = new();
    public int EnemiesAttackCount;

    public static EnemyManager enemyManager;

    private void Awake()
    {

        enemyManager = GetComponent<EnemyManager>();
        if (GetComponent<BeatReciver>() == null)
            gameObject.AddComponent<BeatReciver>();
    }

    public void AddEnemy(Enemy enemy)
    {
        enemies.Add(enemy);

    }
    public void RemoveEnemy(Enemy enemy)
    {
        enemies.Remove(enemy);
    }

    public void BeatHappened()
    {
        if(BeatSender.GiveInstance().beatCount == 3)
        {
            int count = EnemiesAttackCount;
            if (count > enemies.Count)
                count = enemies.Count;

            for(int i = 0; i < count; i++)
            {

                int index = Random.Range(0, enemies.Count - 1);

                enemies[index].enemyBody.GetComponent<EnemyFire>().readyToFire = true;

                print("Enemy attack");
            }
        }
    }

}
