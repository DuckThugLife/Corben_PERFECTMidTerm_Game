using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class ShootInteractor : Interactor
{

    [Header("Gun")]
    public MeshRenderer gunRenderer;
    public Color bulletColor;
    public Color rocketColor;
    
    
    [Header("Shoot")]
    [SerializeField] public ObjectPool bulletPool;
    [SerializeField] public ObjectPool rocketPool;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private PlayerMoveBehaviour moveBehaviour;


    [SerializeField] private float shootVelocity;

    private float finalShootVelocity;
    private IShootStrategy currentStrategy;


    public override void Interact()
    {
       

       if (currentStrategy == null)
       {
          currentStrategy = new BulletShootStrategy(this);
       }

       if (input.weapon1Pressed)
       {
           currentStrategy = new BulletShootStrategy(this);
       }

       if (input.weapon2Pressed)
       {
           currentStrategy = new RocketShootStrategy(this);
       }

       


        // shoot strategy
        if (input.primaryShootPressed && currentStrategy != null)
        {
            currentStrategy.Shoot();
        }

    }

    public Transform GetShootPoint()
    {
        return shootPoint;
    }

    public float GetShootVelocity()
    {
       finalShootVelocity = moveBehaviour.GetForwardSpeed() + shootVelocity;
       return finalShootVelocity;
    }


}
