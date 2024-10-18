using UnityEngine;

public interface IPickable 
{

    public void OnPicked(Transform attachTransform, PickupInteractor pickupInteractor)
    {

    }

    public void OnDropped(float throwForce);

}
