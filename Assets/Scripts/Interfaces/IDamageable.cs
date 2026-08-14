
public interface IDamageable
{
    public int MaxHealth { get; }

    public void TakeDamage(int amount);
}
