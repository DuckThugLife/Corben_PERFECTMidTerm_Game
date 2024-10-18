using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnObject : MonoBehaviour
{
    [SerializeField] private float respawnTime = 5;
    [SerializeField] private bool shootable;
    [SerializeField] private ObjectSpawnManager objectSpawnManager;
    private Vector3 originalSpawnLocation;
    
    void Start()
    {
        originalSpawnLocation = transform.position;

        if (objectSpawnManager == null) // Just incase I forget to set the 
        {
            Debug.LogWarning($"{gameObject.name} should have a spawnManager set, just incase I did it, yes me Corben, the Coding Guy :) Solo project btw");
            objectSpawnManager = FindObjectOfType<ObjectSpawnManager>();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Bullet>() && shootable)
        {
            if (gameObject.GetComponent<StickyObject>() && gameObject.GetComponent<StickyObject>().wall != null)
            {
                gameObject.GetComponent<StickyObject>().wall.UnstickObject(gameObject); // making sure if the object is still stuck to a wall and it's respawning to set it to null
            }

            RespawnThisObject();
        }

       
    }

    public void RespawnThisObject()
    {
        objectSpawnManager.RespawnObject(gameObject, gameObject.GetComponent<PickCube>().originalParent, originalSpawnLocation, respawnTime);
    }

    

}
