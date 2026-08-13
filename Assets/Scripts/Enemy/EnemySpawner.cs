using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class EnemySpawner : MonoBehaviour
{
    [System.Serializable]
    public class EnemyPoolData
    {
        public IObjectPool<GameObject> pool;
        public GameObject prefab;
        public int basePoolSize = 10;
        public int maxPoolSize = 20;
    }

    [SerializeField] private List<EnemyPoolData> enemyPoolList = new List<EnemyPoolData>();
    private Dictionary<GameObject, IObjectPool<GameObject>> enemyPoolDict = new Dictionary<GameObject, IObjectPool<GameObject>>();

    [Space(10)]
    [SerializeField] private float spawnRangeX = 5f;
    [SerializeField] private float spawnPositionY = 10f;
    [SerializeField] private float spawnPositionZ = -10f;

    private void Awake()
    {
        InitializeEnemyPools();
    }

    private void InitializeEnemyPools()
    {
        foreach (var enemyPool in enemyPoolList)
        {
            enemyPool.pool = new ObjectPool<GameObject>(
                createFunc: () => CreateEnemies(enemyPool.prefab, enemyPool.pool),
                actionOnGet: OnGetFromPool,
                actionOnRelease: OnReleaseToPool,
                actionOnDestroy: OnDestroyObject,
                collectionCheck: true,
                defaultCapacity: enemyPool.basePoolSize,
                maxSize: enemyPool.maxPoolSize
            );

            enemyPoolDict.Add(enemyPool.prefab, enemyPool.pool);
        }
    }

    private GameObject CreateEnemies(GameObject enemyPrefab, IObjectPool<GameObject> enemyPool)
    {
        GameObject enemyInstance = Instantiate(enemyPrefab);

        if (enemyInstance.TryGetComponent<EnemyController>(out var enemy))
        {
            enemy.SetPool(enemyPool);
        }

        return enemyInstance;
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

    private Vector3 GetRandomSpawnPosition() {
        return new Vector3(Random.Range(-spawnRangeX, spawnRangeX), spawnPositionY, spawnPositionZ);
    }

    private void SpawnEnemy(GameObject enemyPrefab)
    {
        if (enemyPoolDict.TryGetValue(enemyPrefab, out var enemyPool))
        {
            enemyPool.Get();
        }
    }
}
