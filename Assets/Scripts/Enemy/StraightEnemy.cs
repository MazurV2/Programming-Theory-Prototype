using UnityEngine;

public class StraightEnemy : EnemyController
{
    protected override void Move()
    {
        if (Time.time < maneuverTimeEnd)
        {

        } else
        {
            currentState = EnemyState.FlyOut;
        }
    }
}
