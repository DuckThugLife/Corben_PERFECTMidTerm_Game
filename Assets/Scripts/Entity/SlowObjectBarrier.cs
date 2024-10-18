using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowObjectBarrier : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Rigidbody>())
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();

          
            if (other.GetComponent<PickCube>())
            {
                if (other.GetComponent<PickCube>().currentlyHeld)
                {
                    GameObject player = other.GetComponent<PickCube>().pickupInteractor.gameObject;

                    player.GetComponent<PickupInteractor>().ObjectDropped();
                    other.GetComponent<PickCube>().OnDropped(0);
                    rb.velocity = Vector3.zero; 
                }
                else
                {
                    rb.velocity = Vector3.zero; // pickcubes velocity will be set to 0 if it hit's the slow wall
                }
               
            }
        }
    }

}
