using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public float enemyDamage = 25;
    private EnemyState currentState;
    public float maxWalkingRandomness;
    private float walkingRandomness;

    public Transform[] targetPoints;
    public Transform enemyEye;
    public float playerCheckDistance;
    public float checkRadius = 0.8f;

    public NavMeshAgent agent;

    [HideInInspector] public Transform player;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        currentState = new EnemyIdleState(this);
        currentState.OnStateEnter();
    }

    void Update()
    {
        currentState.OnStateUpdate();
    }

    public void ChangeState(EnemyState state)
    {
        currentState.OnStateExit();
        currentState = state;

        currentState.OnStateEnter();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(enemyEye.position, checkRadius);
        Gizmos.DrawWireSphere(enemyEye.position + enemyEye.forward * playerCheckDistance, checkRadius);

        Gizmos.DrawLine(enemyEye.position, enemyEye.position + enemyEye.forward * playerCheckDistance);
    }

    public float WalkingRandomness()
    {
        walkingRandomness = Random.Range(1, maxWalkingRandomness);
        return walkingRandomness;
    }

    public void ForceEnemyTarget(EnemyFollowState state, bool status)
    {
       state.forceFollowPlayer = true;
    }

    public int GetRandomTargetPoint()
    {
        int targetPointIndex = Random.Range(0, targetPoints.Length);

        return targetPointIndex;
    }

}
