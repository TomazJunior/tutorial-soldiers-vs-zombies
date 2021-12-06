using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    internal event System.EventHandler<int> OnCoinsChanged;
    internal event System.EventHandler OnGameOver;
    internal event System.EventHandler<int> OnRemainingEnemiesChanged;
    internal event System.EventHandler<int> OnWaveChanged;
    public static LevelManager instance = null;
    [SerializeField] List<int> waves = new List<int>();
    [SerializeField] List<EnemySpawner> enemySpawners = new List<EnemySpawner>();
    [SerializeField] Vector2 rangeTimeBetweenEnemies = new Vector2(1, 2);
    [SerializeField] int initialCoins = 5;
    [SerializeField] int maximumEnemiesToReachEndLine = 5;
    private int enemiesReachedTheEndLine = 0;
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

    private int coins;
    public int Coins
    {
        get { return coins; }
        set
        {
            coins = value;
            OnCoinsChanged?.Invoke(this, coins);
        }
    }

    internal void AddAlly(Ally ally)
    {
        if (!isGameOver)
        {
            Coins -= ally.Coins;
        }
    }

    private int round = 0;
    private int enemiesKilled = 0;
    void Awake()
    {
        instance = this;
        SceneManager.sceneUnloaded += HandleSceneUnloaded;
    }

    private void HandleSceneUnloaded(Scene scene)
    {
        if (scene.name == "GameOverScene")
        {
            ResetLevel();
        }
    }

    void Start()
    {
        ResetLevel();
    }

    void ResetLevel()
    {
        isGameOver = false;
        Coins = initialCoins;
        enemiesReachedTheEndLine = 0;
        enemiesKilled = 0;
        round = 0;
        OnWaveChanged?.Invoke(this, round);
        OnRemainingEnemiesChanged?.Invoke(this, maximumEnemiesToReachEndLine);
        StartLevel();
    }
    void StartLevel()
    {
        if (isGameOver) return;

        if (round >= waves.Count)
        {
            StartCoroutine(SetGameOver());
            return;
        }
        Debug.Log("start level " + round);
        var numberOfEnemies = waves[round];

        round++;
        OnWaveChanged?.Invoke(this, round);

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
        enemiesReachedTheEndLine++;
        var remainingEnemies = maximumEnemiesToReachEndLine - enemiesReachedTheEndLine;
        OnRemainingEnemiesChanged?.Invoke(this, remainingEnemies);

        if (enemiesReachedTheEndLine >= maximumEnemiesToReachEndLine)
        {
            // game over scene
            StartCoroutine(SetGameOver());
        }
        else
        {

            RemoveEnemy(enemy, true);
        }
    }

    private IEnumerator SetGameOver()
    {
        if (this.isGameOver) yield break;

        Debug.Log("Game over");
        this.isGameOver = true;
        OnGameOver?.Invoke(this, System.EventArgs.Empty);

        GameOverModel.enemiesKilled = enemiesKilled;
        AsyncOperation load = SceneManager.LoadSceneAsync("GameOverScene", LoadSceneMode.Additive);
        yield return load;
    }
    internal void RemoveEnemy(Enemy enemy, bool enemyReachedEndLine = false)
    {
        Destroy(enemy.gameObject);
        TotalOfEnemies--;

        if (!enemyReachedEndLine && !isGameOver)
        {
            enemiesKilled++;
            Coins += enemy.Coins;
        }
        if (TotalOfEnemies <= 0)
        {
            StartLevel();
        }
    }
}
