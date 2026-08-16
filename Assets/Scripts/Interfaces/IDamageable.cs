
public interface IDamageable
{
    public int MaxHealth { get; }

    public void TakeDamage(int amount, DamageSource damageSource = DamageSource.Other);
}
