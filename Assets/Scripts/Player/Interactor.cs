using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactor : MonoBehaviour
{
    protected PlayerInput input;



    void Start()
    {
        input = PlayerInput.GetInstance();
    }

   
    public virtual void Update()
    {
        Interact();
    }

    public abstract void Interact();
}
