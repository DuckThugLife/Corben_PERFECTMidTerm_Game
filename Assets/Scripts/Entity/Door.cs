using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class Door : MonoBehaviour
{

    [SerializeField] private Animator doorAnimator;

    public int numberOfLocks;

    [SerializeField] private bool isLocked = true;

    private float timer = 0f;

    private const float WAIT_TIME = 1.0f;


    private void OnTriggerEnter(Collider other)
    {
        if (!isLocked && other.CompareTag("Player"))
        {
            timer = 0;
            
        }
    }

    private void OnTriggerStay(Collider other)
    {

        if (isLocked)
            return;

        if (!other.CompareTag("Player"))
            return;

        timer += Time.deltaTime;

        if (timer > WAIT_TIME)
        {
            timer = WAIT_TIME;
            OpenDoor(true);
        }

    }

    public void LockDoor()
    {
        isLocked = true;
    }

    public void UnlockDoor()
    {
        isLocked = false;
    }


    public void OpenDoor(bool doorState)
    {
        if (!isLocked)
        {
            doorAnimator.SetBool("isOpen", doorState);
        }
            
    }

    public bool IsDoorOpen()
    {
        return doorAnimator.GetBool("isOpen");
    }
    
    public void BreakLock()
    {
        if (numberOfLocks >= 0)
        {
            numberOfLocks--;
        }
            

        if (numberOfLocks == 0) // unlocking the door and returning so the locks cant be a negative number
        {
            UnlockDoor();
            return;
        }

        
    }

    public void AddLock()
    {
        numberOfLocks++;

        if (!isLocked) // if the door isn't locked and it's open
        {
            LockDoor();
        }
        
    }


}
