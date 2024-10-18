using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StickyWallDoor : StickyWall
{
    public UnityEvent onStuck;
    public UnityEvent unStick;

    public Door door;

    public override void OnCollison(GameObject collisionGameObject)
    {
        base.OnCollison(collisionGameObject);
        onStuck?.Invoke();
    }

    public override void UnstickObject(GameObject collisionGameObject)
    {
        base.UnstickObject(collisionGameObject); 
        unStick?.Invoke();
    }

    private void Start()
    {
        if (door != null)
        {
            door.AddLock(); // adding 1 lock each based on the stickyWallDoor so I dont need to manually set the lock count everytime i remove or add more to open the door.
        }
    }

}
