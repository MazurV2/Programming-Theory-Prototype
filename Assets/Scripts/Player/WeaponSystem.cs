using System;
using UnityEngine;
using UnityEngine.Pool;

public class WeaponSystem : MonoBehaviour
{
    [SerializeField] private InputReaderSO inputReader;
    
    private bool isShooting;
    [SerializeField] private float fireRate = 0.5f;
    private float nextFireTime;

    private IObjectPool<GameObject> projectilePool;
    [SerializeField] private GameObject projectilePrefab;
    private int basePoolSize = 20;
    private int maxPoolSize = 40;

    private void Awake()
    {
        projectilePool = new ObjectPool<GameObject>(
            createFunc: CreateProjectile,
            actionOnGet: OnGetFromPool,
            actionOnRelease: OnReleaseToPool,
            actionOnDestroy: OnDestroyObject,
            true,
            basePoolSize,
            maxPoolSize);
    }

    private GameObject CreateProjectile()
    {
        GameObject gameObject = Instantiate(projectilePrefab);

        if (gameObject.TryGetComponent<Projectile>(out var projectile))
        {
            projectile.SetPool(projectilePool);
        }

        return gameObject;
    }

    private void OnGetFromPool(GameObject projectile)
    {
        projectile.transform.position = transform.position;
        projectile.transform.rotation = transform.rotation;
        projectile.SetActive(true);
    }

    private void OnReleaseToPool(GameObject projectile)
    {
        projectile.SetActive(false);
    }

    private void OnDestroyObject(GameObject projectile)
    {
        Destroy(projectile);
    }

    private void OnEnable()
    {
        inputReader.onShootInput += ChangeShootingState;
    }

    private void OnDisable()
    {
        inputReader.onShootInput -= ChangeShootingState;
    }

    private void Update()
    {
        if (isShooting && Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + fireRate;
            Shoot();
        }
    }

    private void ChangeShootingState(bool shooting)
    {
        isShooting = shooting;
    }

    private void Shoot()
    {
        projectilePool.Get();
    }
}
