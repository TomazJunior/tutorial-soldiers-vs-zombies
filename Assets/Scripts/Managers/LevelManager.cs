using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance = null;
    [SerializeField] List<int> waves = new List<int>();
    [SerializeField] List<EnemySpawner> enemySpawners = new List<EnemySpawner>();
    [SerializeField] Vector2 rangeTimeBetweenEnemies = new Vector2(1, 2);
    private bool isGameOver = false;
    private int totalOfEnemies;
    public int TotalOfEnemies
    {
        get { return totalOfEnemies; }
        set
        {
            totalOfEnemies = value;
        }
    }

    private int round = 0;
    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        StartLevel();
    }
    void StartLevel()
    {
        if (isGameOver) return;

        if (round >= waves.Count)
        {
            isGameOver = true;
            Debug.Log("Game Over");
            return;
        }
        Debug.Log("start level " + round);
        var numberOfEnemies = waves[round];
        round++;

        enemySpawners.ForEach(enemySpawner =>
        {
            StartCoroutine(SpawnEnemies(enemySpawner, numberOfEnemies));
        });
    }

    private IEnumerator SpawnEnemies(EnemySpawner enemySpawner, int numberOfEnemies)
    {
        TotalOfEnemies += numberOfEnemies;
        do
        {
            var delay = Random.Range(rangeTimeBetweenEnemies.x, rangeTimeBetweenEnemies.y);

            yield return new WaitForSeconds(delay);
            enemySpawner.Spawn();

            numberOfEnemies--;
        } while (numberOfEnemies > 0);
    }

    internal void EnemyReachedEndLine(Enemy enemy)
    {
        RemoveEnemy(enemy);
    }

    internal void RemoveEnemy(Enemy enemy)
    {
        Destroy(enemy.gameObject);
        TotalOfEnemies--;

        if (TotalOfEnemies <= 0)
        {
            StartLevel();
        }
    }
}
