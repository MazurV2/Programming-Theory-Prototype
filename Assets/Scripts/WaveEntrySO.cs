using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WaveEntrySO", menuName = "Wave/Wave Entry")]
public class WaveEntrySO : ScriptableObject
{
    [System.Serializable]
    public struct WaveEntry
    {
        public GameObject enemyPrefab;
        public int enemyCount;
        public float spawnInterval;
        public float timeUntilNextGroup;
    }

    [Header("Wave settings")]
    [SerializeField] private List<WaveEntry> enemyGroupList;
    [SerializeField] private float timeUntilNextWave = 5f;

    public IReadOnlyList<WaveEntry> EnemyGroupList => enemyGroupList;
    public float TimeUntilNextWave => timeUntilNextWave;
}
