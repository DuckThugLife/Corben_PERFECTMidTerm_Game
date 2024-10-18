using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPlayerSpawn : MonoBehaviour
{
    private PlayerSpawnManager playerSpawnManager;

    [SerializeField] private Transform spawnLocation;

    private void Start()
    {
        if (playerSpawnManager == null)
        {
            playerSpawnManager = FindObjectOfType<PlayerSpawnManager>();
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (playerSpawnManager != null && other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player touched the checkpoint");

            playerSpawnManager.SetPlayerSpawn(spawnLocation);
            
        }
    }


}
