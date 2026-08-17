using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class EnemySpawner : MonoBehaviour
{
    [System.Serializable]
    public class EnemyPoolData
    {
        public GameObject prefab;
        public int basePoolSize = 10;
        public int maxPoolSize = 20;
    }

    [Header("Enemy Pool Parameters")]
    [SerializeField] private List<EnemyPoolData> enemyPoolDataList;
    private Dictionary<GameObject, IObjectPool<GameObject>> enemyPoolDict = new Dictionary<GameObject, IObjectPool<GameObject>>();

    [Header("Enemy Waves")]
    [SerializeField] private List<WaveEntrySO> waveList;

    [Header("Spawn Parameters")]
    [SerializeField] private GameBoundsSO gameBoundsSO;
    [SerializeField] private float spawnPositionY = 10f;
    [SerializeField] private float spawnPositionZ = -10f;

    private void Awake()
    {
        InitializeEnemyPools();
    }

    private void Start()
    {
        StartCoroutine(SpawnAllWavesRoutine());
    }

    private void InitializeEnemyPools()
    {
        foreach (var poolData in enemyPoolDataList)
        {
            IObjectPool<GameObject> pool = new ObjectPool<GameObject>(
                createFunc: () => CreateEnemies(poolData.prefab),
                actionOnGet: OnGetFromPool,
                actionOnRelease: OnReleaseToPool,
                actionOnDestroy: OnDestroyObject,
                collectionCheck: true,
                defaultCapacity: poolData.basePoolSize,
                maxSize: poolData.maxPoolSize
            );

            enemyPoolDict.Add(poolData.prefab, pool);
        }
    }

    private GameObject CreateEnemies(GameObject enemyPrefab)
    {
        GameObject enemyInstance = Instantiate(enemyPrefab);

        if (enemyInstance.TryGetComponent<EnemyController>(out var enemy))
        {
            if (enemyPoolDict.TryGetValue(enemyPrefab, out var enemyPool))
            {
                enemy.SetPool(enemyPool);
            }
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
        return new Vector3(Random.Range(gameBoundsSO.MinX, gameBoundsSO.MaxX), spawnPositionY, spawnPositionZ);
    }

    private IEnumerator SpawnAllWavesRoutine()
    {
        foreach (WaveEntrySO waveData in waveList)
        {
            yield return StartCoroutine(SpawnWaveRoutine(waveData));

            yield return new WaitForSeconds(waveData.TimeUntilNextWave);
        }
    }

    private IEnumerator SpawnWaveRoutine(WaveEntrySO waveData)
    {
        foreach (var enemyGroup in waveData.EnemyGroupList)
        {
            for (int i = 0; i < enemyGroup.enemyCount; i++)
            {
                SpawnEnemy(enemyGroup.enemyPrefab);
                yield return new WaitForSeconds(enemyGroup.spawnInterval);
            }
            yield return new WaitForSeconds(enemyGroup.timeUntilNextGroup);
        }
    }

    private void SpawnEnemy(GameObject enemyPrefab)
    {
        if (enemyPoolDict.TryGetValue(enemyPrefab, out var enemyPool))
        {
            enemyPool.Get();
        }
    }
}
