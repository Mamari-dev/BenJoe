using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    private Queue<Enemy> enemyQueue = new Queue<Enemy>();

    private void Start()
    {
        EnemyManager.Instance.ResetEnemyEvent += ResetEnemy;
    }

    protected void AddEnemyInPool(Enemy enemy)
    {
        enemyQueue.Enqueue(enemy);
    }

    private void ResetEnemy()
    {
        for (int i = 0; i < enemyQueue.Count; i++)
        {
            Enemy enemy = enemyQueue.Dequeue();
            RespawnEnemy(enemy);
        }
    }

    private void RespawnEnemy(Enemy enemy)
    {
        enemy.enabled = true;
    }
}
