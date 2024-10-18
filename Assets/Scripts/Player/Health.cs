using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Health : MonoBehaviour
{
    [SerializeField] private float maxHealth;


    public event Action<float> OnHealthUpdated;
    //public event Action OnDeath;

    public bool isDead { get; private set; }
    private float health;

    void Start()
    {
        health = maxHealth;
        OnHealthUpdated?.Invoke(maxHealth);
    }

    public void DeductHealth(float healthToDeduct)
    {
        if (isDead) return;
        Debug.Log("Health to deduct: " + healthToDeduct);

        health -= healthToDeduct;

        if (health <= 0)
        {
            isDead = true;
            //OnDeath(); // just keeping this stuff to show in the video when i submit the assignment, usually would get rid of unused code.
            health = 0;
            gameObject.GetComponent<PlayerSpawn>().RespawnThisPlayer(); // "respawning" the player
        }
        OnHealthUpdated?.Invoke(health);
    }

    public void AddHealth(float healthToAdd)
    {
        if (isDead)
            return;

        health += Math.Clamp(healthToAdd, health, maxHealth);
        OnHealthUpdated?.Invoke(health);
    }

    public void RevivePlayer()
    {
        isDead = false;
        AddHealth(maxHealth);
        OnHealthUpdated?.Invoke(health);
    }

   

}
