using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawnManager : MonoBehaviour
{

    public Transform spawnLocation;
    
    void Start()
    {
        
    }


    public void SetPlayerSpawn(Transform spawn)
    {
        spawnLocation = spawn;
    }

    public void RespawnPlayer(GameObject player)
    {
        if (player == null || spawnLocation == null) 
            return;

        // Can make a respawn animation if I want to in the future
        
        if (player.GetComponent<PlayerMoveBehaviour>()) // if the player has the PlayerMoveBehaviour
        {

            player.GetComponent<CharacterController>().enabled = false; // Had to disable the character controller since it was not allowing me to teleport the player
            player.transform.position = spawnLocation.position;
            player.GetComponent<CharacterController>().enabled = true;

        }
    }



}
