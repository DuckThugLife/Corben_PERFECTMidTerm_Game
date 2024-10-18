using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class BulletShootStrategy : IShootStrategy
{

    private ShootInteractor interactor;
    private Transform shootPoint;
    

 
   

    public BulletShootStrategy(ShootInteractor _interactor)
    {
        Debug.Log("Switched gun bullet mode");
        this.interactor = _interactor;
        shootPoint = interactor.GetShootPoint();

        // change the color of the gun
        interactor.gunRenderer.material.color = interactor.bulletColor;
    }

    public void Shoot()
    {

        PooledObject pooledObj = interactor.bulletPool.GetPooledObject();
        pooledObj.gameObject.SetActive(true);

        Rigidbody bullet = pooledObj.GetComponent<Rigidbody>();
        bullet.transform.position = shootPoint.position;
        bullet.transform.rotation = shootPoint.rotation;

        bullet.velocity = shootPoint.forward * interactor.GetShootVelocity();
        interactor.bulletPool.DestroyPooledObject(pooledObj, 5.0f);
    }

}
