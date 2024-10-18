using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PooledObject : MonoBehaviour
{
    private ObjectPool associatedPool;

    private float timer;
    private bool setToDestroy = false;
    private float destroyTime = 0;


    private void Update()
    {
        if (setToDestroy)
        {
            timer += Time.deltaTime;
            if (timer >= destroyTime)
            {
                timer = 0;
                setToDestroy = false;
                Destroy();
            }
        }
    }

    public void SetObjectPool(ObjectPool pool)
    {
        associatedPool = pool;
        timer = 0;
        destroyTime = 0;
        setToDestroy = false;
    }

    public void Destroy()
    {
       if (associatedPool != null)
       {
            associatedPool.RestoreObject(this);
       }

    }

    public void Destroy(float time)
    {
        setToDestroy = true;
        destroyTime = time;
    }


}
