using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RobotLift : MonoBehaviour
{
    [SerializeField] private Lift lift;
    public GameObject tempRobot;
    private Transform robotOriginalParent;

    private void Start()
    {
        if (lift == null)
        {
            lift = gameObject.GetComponentInParent<Lift>();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (tempRobot == null && collision.gameObject.CompareTag("Robot") && lift != null) // only attach the robot once
        {
            AttachRobot(collision.gameObject, collision.gameObject.transform.parent);
        }

    }

    public void AttachRobot(GameObject robot, Transform originalParent)
    {
        tempRobot = robot.gameObject;
        robotOriginalParent = originalParent;
        NavMeshAgent navMeshAgent = tempRobot.GetComponentInParent<NavMeshAgent>();
        navMeshAgent.ResetPath();
        navMeshAgent.enabled = false;
            
        tempRobot.transform.parent = gameObject.transform.parent;
      
    }

    public void DetachRobot()
    {
        if (tempRobot != null)
        {
            tempRobot.transform.parent = robotOriginalParent;
            tempRobot.GetComponent<NavMeshAgent>().enabled = true;
            robotOriginalParent = null;
            tempRobot = null;
        }
    }
}
