using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnObjectBarrier : MonoBehaviour
{
    [SerializeField] public bool onlyRespawnObject;
    [SerializeField] public PressurePad PressurePad;

    private void OnTriggerEnter(Collider other)
    {

        if (!onlyRespawnObject && other.gameObject.CompareTag("Player"))
        {
            GameObject player = other.gameObject;
            
                if (other.gameObject.GetComponent<PlayerSpawn>())
                {
                    PlayerSpawn playerSpawn = other.transform.GetComponent<PlayerSpawn>();
                    playerSpawn.RespawnThisPlayer();
                }
            
        }

        if (onlyRespawnObject && other.gameObject.CompareTag("Player")) // only respawn a object and the object touching is not the player tag
        {
            GameObject player = other.gameObject;

            Debug.Log(other.gameObject);
            if (other.gameObject.GetComponent<PickupInteractor>().objectHeld != null)
            {
                GameObject pickCube = other.gameObject.GetComponent<PickupInteractor>().objectHeld;
                if(pickCube.GetComponent<PickCube>().currentlyHeld) // dont do anything if the object is held
                    return;

                if (pickCube.GetComponent<RespawnObject>())
                {
                     // respawn the held item
                }
                else if (!pickCube.GetComponent<RespawnObject>()) // there is no respawn object
                {
                    pickCube.GetComponent<PickCube>().OnDropped(0);
                    player.GetComponent<PickupInteractor>().ObjectDropped();
                    if (PressurePad != null) // if a pressurePad has been assigned, then put the pickCube on drop at the location of the assignedPressurePad
                    {
                       pickCube.transform.position = PressurePad.cubeAttachPoint.position;
                    }
                    

                }

                
                
                return; // only respawn the object 
            }

            return; // only respawn the object 
        }

        if (onlyRespawnObject && !other.gameObject.CompareTag("Player") || !other.gameObject.CompareTag("Robot")) // only respawn a object and the object touching is not the player tag
        {
            RespawnObject(other.gameObject);

            if (!other.gameObject.GetComponent<RespawnObject>() && other.gameObject.GetComponent<PickCube>()) // there is no respawn object
            {
                PickCube pickCube = other.gameObject.GetComponent<PickCube>();
                if (pickCube.currentlyHeld) // dont do anything if the object is held
                    return;

                if (PressurePad != null) // if a pressurePad has been assigned, then put the pickCube on drop at the location of the assignedPressurePad
                {
                    if (pickCube.GetComponent<Rigidbody>())
                    {
                        pickCube.GetComponent<Rigidbody>().velocity = Vector3.zero;
                        pickCube.transform.position = PressurePad.cubeAttachPoint.position;
                    }
                    
                }
            }

            return; // only respawn the object 
        }

        


        if (other.transform.CompareTag("PickCube"))
       {
            if (other.gameObject.GetComponent<RespawnObject>() && !other.gameObject.GetComponent<PickCube>().currentlyHeld)
            {
                RespawnObject(other.gameObject);

            }
        }



    }

    private void RespawnObject(GameObject objectToRespawn)
    {
        if (objectToRespawn.transform.CompareTag("PickCube"))
        {
            if (objectToRespawn.gameObject.GetComponent<RespawnObject>() && !objectToRespawn.gameObject.GetComponent<PickCube>().currentlyHeld)
            {
                RespawnObject respawnObject = objectToRespawn.gameObject.GetComponent<RespawnObject>();
                respawnObject.RespawnThisObject();

            }
        }
    }

}
