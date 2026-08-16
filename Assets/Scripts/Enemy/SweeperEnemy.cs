using UnityEngine;

public class SweeperEnemy : EnemyController
{
    [Header("Sweeping Variables")]
    [SerializeField] private float frequency = 3f;
    [SerializeField] private float magnitude = 3f;

    protected override void Move()
    {
        if (Time.time < maneuverTimeEnd)
        {
            float newPositionX = maneuverPosition.x + Mathf.Sin((Time.time - maneuverTimeStart) * frequency) * magnitude;
            Vector3 newPosition = new Vector3(newPositionX, transform.position.y, transform.position.z);
            transform.position = newPosition;
        }
        else
        {
            currentState = EnemyState.FlyOut;
        }
    }
}
