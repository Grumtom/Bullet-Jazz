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
                if (enemy.enemyType == attacks[0].attackingType &&
                    enemy.enemyBody.GetComponent<EnemyMovement>().myMode == EnemyMovement.EnemyMode.Strafe &&
                    !enemy.enemyBody.GetComponent<EnemyFire>().readyToFire)
                    currentEnemies.Add(enemy);

            if(currentEnemies.Count > 0)
            for(int i = 0; i < count; i++)
            {
                int index = Random.Range(0, currentEnemies.Count - 1);

                enemies[index].enemyBody.GetComponent<EnemyFire>().readyToFire = true;

            }
        }
        if(BeatSender.GiveInstance().beatCount == 0 || BeatSender.GiveInstance().beatCount == 2) //When trumpet attack happens
        {

            int count = attacks[1].attackingCount;

            List<Enemy> currentEnemies = new();
            foreach (Enemy enemy in enemies)
                if (enemy.enemyType == attacks[1].attackingType &&
                    enemy.enemyBody.GetComponent<EnemyMovement>().myMode == EnemyMovement.EnemyMode.Strafe &&
                    !enemy.enemyBody.GetComponent<EnemyFire>().readyToFire)
                    currentEnemies.Add(enemy);

            if (currentEnemies.Count > 0)
                for (int i = 0; i < count; i++)
                {
                    int index = Random.Range(0, currentEnemies.Count - 1);

                    enemies[index].enemyBody.GetComponent<EnemyFire>().readyToFire = true;

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
