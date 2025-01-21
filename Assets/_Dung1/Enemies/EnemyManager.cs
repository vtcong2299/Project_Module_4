using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour, IOnGameStart<ITransformGettable>, IOnGameStart<List<IOnEnemyDie>>, IOnEnemyDie
{
    List<IOnEnemyDie> dieCalls;
    ITransformGettable player;
    System.Action<List<IOnEnemyDie>> IOnGameStart<List<IOnEnemyDie>>.onGameStartAction => enemyDieCalls => dieCalls = enemyDieCalls;
    System.Action<ITransformGettable> IOnGameStart<ITransformGettable>.onGameStartAction => theTransform =>
    {
        player = theTransform;
    };

    public int currentRound;
    public int waveInRound;
    public int currentWave;

    public int enemyInWave;
    public int enemyAlive;

    public GameObject[] enemyPrefabs;
    public Vector3 spawnPositionAbove;
    public Vector3 spawnPositionBelow;
    public float spawnDistance = 20f;
    public float timeBetweenSpawns = 0.5f;
    public float timeBetweenWaves = 5f;

    void Start()
    {
        StartCoroutine(NextWave());
    }


    //Hàm spawn enemy ở một khoảng cánh so với player
    private IEnumerator SpawnEnemiesAbove()
    {
        spawnPositionAbove = player._transform.position + new Vector3(0, 0, spawnDistance);
        
        for (int i = 0; i < enemyInWave; i++)
        {
            Vector3 randomOffset = new Vector3(Random.Range(-spawnDistance / 4, spawnDistance / 4), 0, 0);
            GameObject enemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
            GameObject enemy = Instantiate(enemyPrefab, spawnPositionAbove + randomOffset, Quaternion.identity);
            enemy.GetComponent<Enemy>().SetDependencies(player, dieCalls);
            yield return new WaitForSeconds(timeBetweenSpawns);
        }
    }

    private IEnumerator SpawnEnemiesBelow()
    {
        spawnPositionBelow = player._transform.position - new Vector3(0, 0, spawnDistance);

        for (int i = 0; i < enemyInWave; i++)
        {
            Vector3 randomOffset = new Vector3(Random.Range(-spawnDistance / 4, spawnDistance / 4), 0, 0);
            GameObject enemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
            GameObject enemy = Instantiate(enemyPrefab, spawnPositionBelow + randomOffset, Quaternion.identity);
            enemy.GetComponent<Enemy>().SetDependencies(player, dieCalls);
            yield return new WaitForSeconds(timeBetweenSpawns);
        }
    }

    //Hàm respawn enemy khi enemy quá xa player

    //Hàm kiểm tra xem toàn bộ enemy trong wave đã chết hết chưa, nếu hết rồi thì chuyển wave hoặc chuyển round nếu đã là wave cuối

    //Hàm chuyển wave
    private IEnumerator NextWave()
    {
        enemyAlive = enemyInWave * 2;
        yield return new WaitForSeconds(timeBetweenWaves);
        currentWave++;
        StartCoroutine(SpawnEnemiesAbove());
        StartCoroutine(SpawnEnemiesBelow());
    }

    public void OnEnemyDie()
    {
        enemyAlive--;
        if (enemyAlive == 0)
        {
            StartCoroutine(NextWave());
        }
    }

    //Hàm chuyển round
}
