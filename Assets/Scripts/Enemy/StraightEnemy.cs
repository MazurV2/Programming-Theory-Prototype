using UnityEngine;

public class StraightEnemy : EnemyController
{
    protected override void Move()
    {
        currentState = EnemyState.FlyOut;
    }
}
