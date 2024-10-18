using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketShootStrategy : IShootStrategy
{
    private ShootInteractor interactor;
    private Transform shootPoint;


    public RocketShootStrategy(ShootInteractor _interactor)
    {
        Debug.Log("Switched to rocket mode");
        this.interactor = _interactor;
        shootPoint = interactor.GetShootPoint();

        // change gun color
        interactor.gunRenderer.material.color = interactor.rocketColor;
    }

    public void Shoot()
    {
        PooledObject pooledObj = interactor.rocketPool.GetPooledObject();
        pooledObj.gameObject.SetActive(true);

        Rigidbody rocket = pooledObj.GetComponent<Rigidbody>();
        rocket.transform.position = shootPoint.position;
        rocket.transform.rotation = shootPoint.rotation;

        rocket.velocity = shootPoint.forward * interactor.GetShootVelocity();
        interactor.rocketPool.DestroyPooledObject(pooledObj, 5.0f);
    }


}
