using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveCommand : Command
{
    private NavMeshAgent agent;
    private Vector3 destination;

    public MoveCommand(NavMeshAgent _agent, Vector3 _destination)
    {
        this.agent = _agent;
        this.destination = _destination;
    }

    public override bool isComplete => ReachedDestination();

    public override void Execute()
    {
        agent.SetDestination(destination);
    }

    bool ReachedDestination()
    {
        if (agent.remainingDistance > 0.5f)
           return false;

        else
        {
            agent.SetDestination(agent.gameObject.transform.position);
            return true;
        }
        
        
    }

}
