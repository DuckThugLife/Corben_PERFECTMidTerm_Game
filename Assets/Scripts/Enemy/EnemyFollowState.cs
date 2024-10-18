using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollowState : EnemyState
{

    private float distanceToPlayer;
    public bool forceFollowPlayer = false;

    public EnemyFollowState(EnemyController enemy) : base(enemy)
    {

    }

    public override void OnStateEnter()
    {
        Debug.Log("Enemy will start following the player :D");
    }

    public override void OnStateExit()
    {
        Debug.Log("Enemy will no longer follow the player :(");
    }

    public override void OnStateUpdate()
    {
        if (_enemy.player != null)
        {
            distanceToPlayer = Vector3.Distance(_enemy.transform.position, _enemy.player.position);
            if (distanceToPlayer > 10 && !forceFollowPlayer)
            {
                // Going back to idle state
                _enemy.ChangeState(new EnemyIdleState(_enemy));
            }

            // set the attack
            if (distanceToPlayer < 2)
            {
                _enemy.ChangeState(new EnemyAttackState(_enemy, _enemy.enemyDamage));
            }

            _enemy.agent.destination = _enemy.player.position;
        }
        else
        {
            // Idle state
            _enemy.ChangeState(new EnemyIdleState(_enemy));
        }
    }
}
