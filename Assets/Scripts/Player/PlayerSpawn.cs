using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    [SerializeField] private PlayerSpawnManager playerSpawnManager;

    public Transform currentSpawnLocation; // the player spawns off the PlayerSpawnManager location, just a visual location on the player.

    void Start()
    {
        if (playerSpawnManager == null)
        {
            playerSpawnManager = FindAnyObjectByType<PlayerSpawnManager>(); // getting the spawn manager if I forgot to set it on the prefab.
        }

        if (playerSpawnManager != null)
        {
            if (playerSpawnManager.spawnLocation == null) // Just incase the spawnLocation is null dont set it
                return;

            currentSpawnLocation = playerSpawnManager.spawnLocation; // if there is a playerSpawnManager then set the player spawn location to the manager spawn location
            RespawnThisPlayer();
        }

    }

    public void RespawnThisPlayer()
    {
        Health health = GetComponent<Health>();
        if (health.isDead)
        {
            health.RevivePlayer();
        }
        
        playerSpawnManager.RespawnPlayer(gameObject); // this script is attached to the main player gameObject
    }


    void Update()
    {

    }



}
