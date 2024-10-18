using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : EnemyState
{
    private Health playerHealth;

    private float distanceToPlayer;
    private float damagePerSecond;

    public EnemyAttackState(EnemyController enemy, float damagePerSecond) : base(enemy)
    {
        playerHealth = enemy.player.GetComponent<Health>();
        this.damagePerSecond = damagePerSecond;
    }

    public override void OnStateEnter()
    {
        Debug.Log("Enemy will attack the homie");
    }

    public override void OnStateExit()
    {
        Debug.Log("Enemy will NOT attack the homie");
    }

    public override void OnStateUpdate()
    {
        Attack();

        if (_enemy.player != null)
        {
            distanceToPlayer = Vector3.Distance(_enemy.transform.position, _enemy.player.position);
            if (distanceToPlayer > 2)
            {
                // Going back to follow state
                _enemy.ChangeState(new EnemyFollowState(_enemy));
            }

            _enemy.agent.destination = _enemy.player.position;
        }
        else
        {
            // Idle state
            _enemy.ChangeState(new EnemyIdleState(_enemy));
        }
    }

    void Attack()
    {
        if (playerHealth != null)
        {
            playerHealth.DeductHealth(damagePerSecond * Time.deltaTime);
        }
    }

}

