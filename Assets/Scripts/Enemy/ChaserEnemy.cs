using UnityEngine;

public class ChaserEnemy : EnemyController
{
    protected override void Move()
    {
        // TODO: add moving towards the player
        currentState = EnemyState.FlyOut;
    }
}
