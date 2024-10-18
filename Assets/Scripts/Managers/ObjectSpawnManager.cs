using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawnManager : MonoBehaviour
{

    public GameObject respawnParticles;

    public void RespawnObject(GameObject objectToRespawn, Transform parent, Vector3 location, float respawnTime)
    {
        if (objectToRespawn == null || location == null) 
            return;
       
        objectToRespawn.transform.SetParent(parent);
        objectToRespawn.transform.position = location;

        // If the object im trying to respawn has a PickCube script then im doing more. (But with the new respawn manager and using particles I dont really
        // need to set canPickUp anymore since you cant grab a inactive object)
        if (objectToRespawn.GetComponent<PickCube>())
        {
            PickCube _pickCube = objectToRespawn.GetComponent<PickCube>();
            _pickCube.canPickUp = false;
            

            objectToRespawn.gameObject.SetActive(false);
            
            if (objectToRespawn.GetComponent<StickyObject>() && objectToRespawn.GetComponent<StickyObject>().wall != null) // if the wall is still attached to on respawn unstick the object
            {
                _pickCube.UnstickObject();
            }

            StartCoroutine(RespawnAnimation(objectToRespawn, parent, respawnTime));
        }

    }

    public IEnumerator RespawnAnimation(GameObject objectToPlayAnimation, Transform objectParent, float respawnTime)
    {

        // make some particles (I did something simple)
        GameObject tempParticlesGameObject = Instantiate(respawnParticles, objectParent);

        Destroy(tempParticlesGameObject, respawnTime); // Should make particles a pooled object for performance,
                                                       // or just add a particle emitter on the cube and play it with the duraction as the respawn time
        yield return new WaitForSeconds(respawnTime); // Waiting the passed in respawn time
        ReenablePickUp(objectToPlayAnimation);
    }

    public void ReenablePickUp(GameObject objectToEnable)
    {
        Rigidbody _rigidBody = objectToEnable.GetComponent<Rigidbody>();

        _rigidBody.velocity = Vector3.zero;
        _rigidBody.constraints = RigidbodyConstraints.None;
        _rigidBody.isKinematic = false;
        

        objectToEnable.transform.rotation = Quaternion.identity;
        objectToEnable.gameObject.SetActive(true);
       

        objectToEnable.GetComponent<PickCube>().canPickUp = true;
    }


}
