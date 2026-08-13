using UnityEngine;
using UnityEngine.Pool;

public class EnemySpawner : MonoBehaviour
{
    private IObjectPool<GameObject> enemyPool;
    [SerializeField] private GameObject[] enemyPrefabList;
    private int basePoolSize = 10;
    private int maxPoolSize = 20;

    [Space(10)]
    [SerializeField] private float spawnWaveInterval = 5f;
    [SerializeField] private int enemiesPerWave = 3;
    private float nextWaveSpawnTime = 0f;

    [Space(10)]
    private float spawnRangeX = 5f;
    private float spawnPositionY = 10f;
    private float spawnPositionZ = -10f;

    private void Awake()
    {
        enemyPool = new ObjectPool<GameObject>(
                createFunc: CreateEnemies,
                actionOnGet: OnGetFromPool,
                actionOnRelease: OnReleaseToPool,
                actionOnDestroy: OnDestroyObject,
                true,
                basePoolSize,
                maxPoolSize
            );
    }

    private void Update()
    {
        if (Time.time >= nextWaveSpawnTime)
        {
            nextWaveSpawnTime = Time.time + spawnWaveInterval;
            for (int i = 0; i < enemiesPerWave; i++)
            {
                SpawnEnemy();
            }
        }
    }

    private GameObject CreateEnemies()
    {
        GameObject enemyPrefab = enemyPrefabList[Random.Range(0, enemyPrefabList.Length)];

        GameObject gameObject = Instantiate(enemyPrefab);

        if (gameObject.TryGetComponent<EnemyController>(out var enemy))
        {
            enemy.SetPool(enemyPool);
        }

        return gameObject;
    }

    private void OnGetFromPool(GameObject enemy)
    {
        enemy.transform.position = GetRandomSpawnPosition();
        enemy.SetActive(true);
    }

    private void OnReleaseToPool(GameObject enemy)
    {
        enemy.SetActive(false);
    }

    private void OnDestroyObject(GameObject enemy)
    {
        Destroy(enemy);
    }

    private void SpawnEnemy()
    {
        enemyPool.Get();
    }

    private Vector3 GetRandomSpawnPosition() {
        Vector3 randomSpawnPosition = new Vector3(Random.Range(-spawnRangeX, spawnRangeX), spawnPositionY, spawnPositionZ);
        return randomSpawnPosition;
    }
}
