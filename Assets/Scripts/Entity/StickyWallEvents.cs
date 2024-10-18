using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StickyWallEvents : StickyWall
{

    public UnityEvent onStuck;
    public UnityEvent unStick;


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
}
