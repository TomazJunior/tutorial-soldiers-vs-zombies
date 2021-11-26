using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<EnemyStats> enemyStats = new List<EnemyStats>();
    [SerializeField] Enemy enemyPrefab;

    internal void Spawn()
    {
        Enemy enemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
        var enemyStats = GetRandomEnemyStats();
        enemy.speed = enemyStats.speed;
        enemy.Sprite = enemyStats.sprite;
    }

    private EnemyStats GetRandomEnemyStats()
    {
        var index = Random.Range(0, enemyStats.Count);
        return enemyStats[index];
    }

}
