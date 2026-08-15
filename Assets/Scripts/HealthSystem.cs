using System;
using UnityEngine;

public class HealthSystem : MonoBehaviour, IDamageable
{
    [Header("Variables")]
    [SerializeField] private int maxHealth = 3;
    [SerializeField] private int currentHealth;
    public int MaxHealth => maxHealth;
    public event Action OnDied;

    [Header("Scriptable Object Variables [Optional]")]
    [SerializeField] private IntVariableSO currentHealthSO;
    [SerializeField] private IntVariableSO maxHealthSO;

    private void Awake()
    {
        ResetHealth();

        UpdateScriptableObjects(maxHealthSO, maxHealth);
    }

    public void ResetHealth()
    {
        currentHealth = maxHealth;

        UpdateScriptableObjects(currentHealthSO, currentHealth);
    }

    public void TakeDamage(int amount)
    {
        currentHealth = Mathf.Max(currentHealth - amount, 0);

        UpdateScriptableObjects(currentHealthSO, currentHealth);

        if (currentHealth <= 0)
        {
            OnDied?.Invoke();
        }
    }

    public void Heal(int amount)
    {
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);

        UpdateScriptableObjects(currentHealthSO, currentHealth);
    }

    private void UpdateScriptableObjects(IntVariableSO variableToUpdate, int value)
    {
        if (variableToUpdate != null)
        {
            variableToUpdate.Value = value;
        }
    }
}
