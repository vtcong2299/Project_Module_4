using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : Singleton<EnemyManager>, IOnGameStart<ITransformGettable>, IOnGameStart<List<IOnEnemyDie>>, IOnEnemyDie, IRespawnable
{
    //public static EnemyManager Instance {get; set;}
    List<IOnEnemyDie> dieCalls;
    ITransformGettable player;
    System.Action<List<IOnEnemyDie>> IOnGameStart<List<IOnEnemyDie>>.onGameStartAction => enemyDieCalls => dieCalls = enemyDieCalls;
    System.Action<ITransformGettable> IOnGameStart<ITransformGettable>.onGameStartAction => theTransform =>
    {
        player = theTransform;
    };

    public float respawnDistance => 45f;

    List<GameObject> enemies;
    GameObject enemyParent;

    public int currentRound;
    public int waveInRound;
    public int currentWave;

    public int enemyInWave;
    public int enemyAlive;

    public GameObject[] enemyPrefabs;
    public Vector3 spawnPositionAbove;
    public Vector3 spawnPositionBelow;
    public float spawnDistance = 30f;
    public float timeBetweenUnitSpawns = 5f;
    public float timeBetweenWaves = 10f;
    //int enemyEachSpawn = 3;
    //float timeBetweenEachSpawn = 3;
    

    //private void Awake() {
    //    if (Instance != null && Instance != this)
    //    {
    //        Destroy(gameObject);
    //    } else {
    //        Instance = this;
    //    }
    //}

    void Start()
    {
        enemyParent = new GameObject("Enemies");
        enemyPools = new List<GameObject>[enemyPrefabs.Length];
        for (int i = 0; i < enemyPools.Length; i++)
        {
            enemyPools[i] = new List<GameObject>();
        }
        enemies = new List<GameObject>();
        StartCoroutine(NextWave());
    }

    //Hàm spawn enemy ở một khoảng cánh so với player

    List<GameObject>[] enemyPools;
    IEnumerator SpawnEnemies(Vector3 spawnOffsetOnForward)
    {

        for (int i = 0; i < enemyInWave; i++)
        {
            Vector3 spawnPosition = player._transform.position + spawnOffsetOnForward;
            spawnPosition.x = 0;

            Vector3 randomOffset = new Vector3(Random.Range(-spawnDistance / 2, spawnDistance / 2), 0, 0);
            int randomIndex = Random.Range(0, enemyPrefabs.Length);
            GameObject enemyPrefab = enemyPrefabs[randomIndex];
            if (enemyPools[randomIndex].Count == 0)
            {
                CreateNewEnemy(enemyPrefab, spawnPosition + randomOffset, randomIndex);
            }
            else
            {
                bool allActive = false;
                ReuseFromPool(randomIndex, spawnPosition + randomOffset, ref allActive);
                if (allActive)
                {
                    CreateNewEnemy(enemyPrefab, spawnPosition + randomOffset, randomIndex);
                }
            }
            yield return new WaitForSeconds(timeBetweenUnitSpawns);
            //if ((i - 1) % enemyEachSpawn == 0)
            //{
            //    yield return new WaitForSeconds(timeBetweenEachSpawn - timeBetweenUnitSpawns);
            //}
        }
    }

    void CreateNewEnemy(GameObject prefab, Vector3 position, int poolIndex)
    {
        GameObject enemy = Instantiate(prefab, position, Quaternion.identity, enemyParent.transform);
        enemy.GetComponent<Enemy>().SetDependencies(player, dieCalls);
        enemyPools[poolIndex].Add(enemy);
        enemies.Add(enemy);
    }

    void ReuseFromPool(int poolIndex, Vector3 newPosition, ref bool allActive)
    {
        for (int i = 0; i < enemyPools[poolIndex].Count; i++)
        {
            if (!enemyPools[poolIndex][i].activeSelf)
            {
                enemyPools[poolIndex][i].transform.position = newPosition;
                enemyPools[poolIndex][i].transform.rotation = Quaternion.identity;
                enemyPools[poolIndex][i].SetActive(true);
                enemyPools[poolIndex][i].GetComponent<Enemy>().Revive();
                enemies.Add(enemyPools[poolIndex][i]);
                break;
            }
            if (i == enemyPools[poolIndex].Count - 1)
            {
                allActive = true;
            }
        }
    }

    //Hàm respawn enemy khi enemy quá xa player
    public void Respawn()
    {
        StopAllCoroutines();
        foreach (GameObject enemy in enemies)
        {
            enemy.SetActive(false);
        }
        enemies.Clear();
        StartCoroutine(RestartWave());
    }

    //Hàm kiểm tra xem toàn bộ enemy trong wave đã chết hết chưa, nếu hết rồi thì chuyển wave hoặc chuyển round nếu đã là wave cuối

    //Hàm chuyển wave
    private IEnumerator NextWave()
    {
        currentWave++;
        UIManager.Instance.OnNextWave();
        enemyInWave += currentWave * 2;
        enemyAlive = enemyInWave * 2;
        yield return new WaitForSeconds(timeBetweenWaves);
        StartCoroutine(SpawnEnemies(new Vector3(0, 0, spawnDistance)));
        StartCoroutine(SpawnEnemies(-new Vector3(0, 0, spawnDistance)));
    }

    IEnumerator RestartWave()
    {
        enemyAlive = enemyInWave * 2;
        yield return new WaitForSeconds(timeBetweenWaves);
        StartCoroutine(SpawnEnemies(new Vector3(0, 0, spawnDistance)));
        StartCoroutine(SpawnEnemies(-new Vector3(0, 0, spawnDistance)));
    }

    public void OnEnemyDie(GameObject dyingEnemy, float exp)
    {
        enemyAlive--;
        if (enemyAlive <= 0)
        {
            StartCoroutine(NextWave());
        }
    }

    //Hàm chuyển round

}
