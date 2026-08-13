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
    }

    [Header("Wave settings")]
    [SerializeField] private List<WaveEntry> waveList;
    [SerializeField] private float timeUntilNextWave = 5f;

    public IReadOnlyList<WaveEntry> WaveList => waveList;
    public float TimeUntilNextWave => timeUntilNextWave;
}
