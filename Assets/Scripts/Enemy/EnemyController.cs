using UnityEngine;
using UnityEngine.Pool;

public abstract class EnemyController : MonoBehaviour
{
    protected enum EnemyState
    {
        FlyIn,
        Maneuver,
        FlyOut
    }

    protected EnemyState currentState = EnemyState.FlyIn;

    protected IObjectPool<GameObject> pool;
    
    [Header("Health Variables")]
    [SerializeField] private HealthSystem healthSystem;

    [Header("Movement Parameters")]
    [SerializeField] protected float flyInSpeed = 10f;
    [SerializeField] protected float maneuverSpeed = 5f;
    [SerializeField] protected float flyOutSpeed = 10f;
    
    [Space(5)]
    [SerializeField] protected float maneuverTime = 5f;
    protected float maneuverTimeStart;
    protected float maneuverTimeEnd;

    [Space(5)]
    protected Vector3 maneuverPosition;
    [SerializeField] private GameBoundsSO gameBoundsSO;
    [SerializeField] private float maneuverZ = 20f;
    
    [Space(5)]
    protected Vector3 escapePosition;
    protected float escapePositionZ = -10f;

    private void Start()
    {
        float maneuverX = Random.Range(gameBoundsSO.MinX, gameBoundsSO.MaxX);
        float maneuverY = Random.Range(gameBoundsSO.MinY, gameBoundsSO.MaxY);

        maneuverPosition = new Vector3(maneuverX, maneuverY, maneuverZ);
    }

    private void OnEnable()
    {
        currentState = EnemyState.FlyIn;

        if (healthSystem != null)
        {
            healthSystem.ResetHealth();
            healthSystem.OnDied += Die;
        }
    }

    private void OnDisable()
    {
        if (healthSystem != null)
        {
            healthSystem.OnDied -= Die;
        }
    }

    private void Update()
    {
        switch (currentState)
        {
            case EnemyState.FlyIn: FlyIn(); break;
            case EnemyState.Maneuver: Move(); break;
            case EnemyState.FlyOut: FlyOut(); break;
        }
    }

    public void SetPool(IObjectPool<GameObject> pool)
    {
        this.pool = pool;
    }

    protected virtual void FlyIn()
    {
        MoveToPosition(maneuverPosition, flyInSpeed);

        if (Vector3.Distance(transform.position, maneuverPosition) <= 0.1f)
        {
            if (maneuverTime <= 0)
            {
                currentState = EnemyState.FlyOut;
            } 
            else
            {
                currentState = EnemyState.Maneuver;
                maneuverTimeStart = Time.time;
                maneuverTimeEnd = Time.time + maneuverTime;
            }
        }
    }

    protected abstract void Move();
    
    protected virtual void FlyOut()
    {
        escapePosition = new Vector3(transform.position.x, transform.position.y, escapePositionZ);
        MoveToPosition(escapePosition, flyOutSpeed);

        if (Vector3.Distance(transform.position, escapePosition) <= 0.1f)
        {
            Die(DamageSource.Other);
        }
    }

    protected void MoveToPosition(Vector3 targetPosition, float speed)
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
    }

    protected virtual void Die(DamageSource damageSource)
    {
        if (pool != null)
        {
            pool.Release(gameObject);
        } else
        {
            Destroy(gameObject);
        }
    }
}