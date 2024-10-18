using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class ForceEnemyTarget : MonoBehaviour
{
    public List<EnemyController> enemies = new List<EnemyController>();

    void Start()
    {
        foreach (EnemyController enemy in gameObject.transform.parent.GetComponentsInChildren<EnemyController>()) // getting all the EnemyControllers in the parent
        {
            enemies.Add(enemy);
        }
    }

    
    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider other) // if the player touches the trigger will force the enemy.agent to change it's target to the player.
    {
        if (other.CompareTag("Player"))
        {
            foreach (EnemyController enemy in enemies)
            {
                if (enemy != null)
                {
                    Debug.Log("Changing the agent target to the player");
                    EnemyFollowState enemyFollowState = new EnemyFollowState(enemy);

                    enemy.player = other.gameObject.transform;
                    enemy.ChangeState(enemyFollowState);
                    enemy.ForceEnemyTarget(enemyFollowState, true);


                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            foreach (EnemyController enemy in enemies)
            {
                if (enemy != null)
                {
                    Debug.Log("Changing the agent target to the player");
                    EnemyFollowState enemyFollowState = new EnemyFollowState(enemy);

                    enemy.player = null;
                    enemy.ChangeState(new EnemyIdleState(enemy)); // making the enemyState change to idle
                    enemy.ForceEnemyTarget(enemyFollowState, false);
                }
            }
        }
    }


}
