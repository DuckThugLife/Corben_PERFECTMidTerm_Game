using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class PickCube : MonoBehaviour, IPickable, ISelectable
{
    public UnityEvent onHoverEnter, onHoverExit;
    

    public PickupInteractor pickupInteractor;
    public Transform originalParent;

    private Rigidbody cubeRB;

    public bool canPickUp = true;
    public bool currentlyHeld = false;

    void Start()
    {
        originalParent = transform.parent;
        cubeRB = GetComponent<Rigidbody>();
    }



    public void OnDropped(float throwForce)
    {
        gameObject.layer = 13; // Pickable Layer
        Debug.Log("Layer is: " + gameObject.layer);
        currentlyHeld = false;
        cubeRB.isKinematic = false;
        cubeRB.constraints = RigidbodyConstraints.None;
        transform.SetParent(originalParent);

        if (cubeRB)
        {
            cubeRB.AddForce(pickupInteractor.attachPoint.transform.forward * throwForce, ForceMode.Impulse);
        }

        pickupInteractor = null;
    }

    public void OnPicked(Transform attachTransform, PickupInteractor _pickupInteractor)
    {
        gameObject.layer = 2; // Ignore Raycast layer so i cant jump mid air
        Debug.Log("Layer is: " + gameObject.layer);
        pickupInteractor = _pickupInteractor;
        cubeRB.isKinematic = true;
        currentlyHeld = true;

        transform.position = attachTransform.position;
        transform.rotation = attachTransform.rotation;

        transform.SetParent(attachTransform);

        UnstickObject();
      
    }

    public void UnstickObject()
    {
        if (gameObject.GetComponent<StickyObject>() && gameObject.GetComponent<StickyObject>().wall != null)
        {
            gameObject.GetComponent<StickyObject>().wall.GetComponent<StickyWall>().UnstickObject(gameObject);
        }
    }

    public void OnSelect()
    {
        
    }

    public void OnHoverEnter()
    {
        onHoverEnter?.Invoke();
    }

    public void OnHoverExit()
    {
        onHoverExit?.Invoke();
    }
}
