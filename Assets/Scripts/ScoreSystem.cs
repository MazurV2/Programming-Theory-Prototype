using System;
using UnityEngine;

public class ScoreSystem : MonoBehaviour
{
    [Header("Score Variables")]
    [SerializeField] private int scoreToGive = 0;
    [SerializeField] private IntVariableSO scoreSO;

    [Header("Health System")]
    [SerializeField] private HealthSystem healthSystem;

    private void OnEnable()
    {
        if (healthSystem != null)
        {
            healthSystem.OnDied += GiveScore;
        }
    }

    private void OnDisable()
    {
        if (healthSystem != null)
        {
            healthSystem.OnDied -= GiveScore;
        }
    }

    private void GiveScore(DamageSource damageSource)
    {
        if (damageSource == DamageSource.PlayerProjectile && scoreSO != null)
        {
            scoreSO.Value += scoreToGive;
        }
    }
}
