using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyManager : MonoBehaviour
{

    [SerializeField] List<Enemy> enemies = new();
    [SerializeField] List<EnemyAttack> attacks = new();

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
        if(BeatSender.GiveInstance().beatCount == 1)//When Drum attack happens
        {
            int count = attacks[0].attackingCount;

            List<Enemy> currentEnemies = new();
            foreach (Enemy enemy in enemies)
                if (enemy.enemyType == attacks[0].attackingType)
                    currentEnemies.Add(enemy);

            for(int i = 0; i < count; i++)
            {

                int index = Random.Range(0, currentEnemies.Count - 1);

                enemies[index].enemyBody.GetComponent<EnemyFire>().readyToFire = true;

                print("Enemy attack");
            }
        }
    }

}

[System.Serializable]
struct EnemyAttack
{
    public Enemy.EnemyType attackingType;
    public int attackingCount;
}
