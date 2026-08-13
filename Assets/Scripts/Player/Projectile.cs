using UnityEngine;
using UnityEngine.Pool;

public class Projectile : MonoBehaviour
{
    private IObjectPool<GameObject> pool;
    private bool isReleased;

    [SerializeField] private float baseLifetime = 3f;
    [SerializeField] private float baseSpeed = 5f;
    [SerializeField] private int damage = 1;

    private float currentLifetime;

    public void SetPool(IObjectPool<GameObject> pool)
    {
        this.pool = pool;
    }

    private void OnEnable()
    {
        currentLifetime = baseLifetime;
        isReleased = false;
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * baseSpeed * Time.deltaTime);

        currentLifetime -= Time.deltaTime;
        if (currentLifetime <= 0f && !isReleased)
        {
            isReleased = true;
            ReleaseToPool();
        } 
    }

    private void ReleaseToPool()
    {
        if (pool != null && !isReleased)
        {
            isReleased = true;
            pool.Release(gameObject);
        } else
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<IDamageable>(out var damageable))
        {
            damageable.TakeDamage(damage);
            if (!isReleased)
            {
                isReleased = true;
                ReleaseToPool();
            }
        }
    }
}
