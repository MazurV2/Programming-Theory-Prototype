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
    [SerializeField] private List<WaveEntry> _enemyGroupList;
    [SerializeField] private float _timeUntilNextWave = 5f;

    public IReadOnlyList<WaveEntry> EnemyGroupList => _enemyGroupList;
    public float TimeUntilNextWave => _timeUntilNextWave;
}
