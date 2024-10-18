using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class Lift : MonoBehaviour
{

    [SerializeField] private float moveDistance;
    [SerializeField] private float speed;

    [SerializeField] private bool isUp;



    public Vector3 destination;
    bool isMoving;

    
    void Update()
    {
        if (isMoving)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, destination, speed * Time.deltaTime);
        }

        if (Vector3.Distance(transform.localPosition, destination) < 0.05f)
        {
            isMoving = false;
            /*if (gameObject.GetComponentInChildren<RobotLift>()) // if there is a robot attached to the robot lift and the lift reached the destination detach
            {
                if (gameObject.GetComponentInChildren<RobotLift>().tempRobot == null)
                    return;

                navMeshSurface.AddData(); // addind the new position of the lift
                RobotLift robotLift = gameObject.GetComponentInChildren<RobotLift>();
                robotLift.DetachRobot();
            }*/
        }

    }

    public void ToggleLift()
    {
        if (isMoving)
            return;
        
        if (isUp)
        {
            destination = transform.localPosition - new Vector3(0, moveDistance, 0);
            isUp = false;
        }
        else
        {
           destination = transform.localPosition + new Vector3(0, moveDistance, 0);
           isUp = true;
        }

        isMoving = true;

    }

   
    


}
