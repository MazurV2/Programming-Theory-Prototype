using System;
using UnityEngine;

public class HealthSystem : MonoBehaviour, IDamageable
{
    [Header("Variables")]
    [SerializeField] private int maxHealth = 3;
    [SerializeField] private int currentHealth;
    public int MaxHealth => maxHealth;
    public event Action<DamageSource> OnDied;

    [Header("Scriptable Object Variables [Optional]")]
    [SerializeField] private IntVariableSO currentHealthSO;
    [SerializeField] private IntVariableSO maxHealthSO;

    private void Awake()
    {
        ResetHealth();

        UpdateSOVariable(maxHealthSO, maxHealth);
    }

    public void ResetHealth()
    {
        currentHealth = maxHealth;

        UpdateSOVariable(currentHealthSO, currentHealth);
    }

    public void TakeDamage(int amount, DamageSource damageSource)
    {
        currentHealth = Mathf.Max(currentHealth - amount, 0);

        UpdateSOVariable(currentHealthSO, currentHealth);

        if (currentHealth <= 0)
        {
            OnDied?.Invoke(damageSource);
        }
    }

    public void Heal(int amount)
    {
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);

        UpdateSOVariable(currentHealthSO, currentHealth);
    }

    private void UpdateSOVariable(IntVariableSO variableToUpdate, int value)
    {
        if (variableToUpdate != null)
        {
            variableToUpdate.Value = value;
        }
    }
}
