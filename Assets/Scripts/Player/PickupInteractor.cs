using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PickupInteractor : Interactor
{
    [SerializeField] private GameObject PickupCubeInterface;

    [Header("Pick and Drop")]
    [SerializeField] private ShootInteractor shootInteractor;
    [SerializeField] private Camera cam;
    [SerializeField] private float pickupDistance;
    [SerializeField] private float wallDistanceCheck = 1;
    [SerializeField] private LayerMask pickupLayer;

    [SerializeField] public Transform attachPoint;
    [SerializeField] public GameObject objectHeld;
    [SerializeField] public bool holdingObject = false;

    private IPickable pickable;
    private RaycastHit raycastHit;

    public override void Interact()
    {
        Ray ray = cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        if (Physics.Raycast(ray, out raycastHit, pickupDistance, pickupLayer))
        {
            if (input.activatePressed && !holdingObject)
            {
                shootInteractor = gameObject.GetComponent<ShootInteractor>();
                pickable = raycastHit.transform.GetComponent<IPickable>();
                if (pickable == null || shootInteractor == null)
                    return;

                holdingObject = true;
                objectHeld = raycastHit.transform.gameObject;

                shootInteractor.enabled = false;
                PickupCubeInterface.SetActive(true);
                pickable.OnPicked(attachPoint, this);
                
                return;
            }

        }

        if (input.activatePressed && holdingObject && pickable != null)
        {

            CheckForWall();
            pickable.OnDropped(0);
            ObjectDropped();
        }

        if (input.primaryShootPressed && holdingObject && pickable != null)
        {
            CheckForWall();
            pickable.OnDropped(10);
            ObjectDropped();
        }

        if (input.secondaryShootPressed && holdingObject && pickable != null)
        {
            CheckForWall();
            pickable.OnDropped(15);
            ObjectDropped();
        }
    }

    public void ObjectDropped()
    {
        PickupCubeInterface.SetActive(false);
        shootInteractor.enabled = true;
        holdingObject = false;
        objectHeld = null;
    }


    private void RotateObject()
    {
        if (!holdingObject && objectHeld == null)
            return;

        if (input.middleMouse)
        {
            Debug.Log("Middle Mouse while holding object");
            objectHeld.transform.rotation = objectHeld.transform.parent.rotation;
        }
        
        if (input.ctrlHeld)
        {
            objectHeld.transform.rotation *= Quaternion.Euler(0, 0, input.scrollWheel.y);
            return;
        }

        objectHeld.transform.rotation *= Quaternion.Euler(input.scrollWheel.y, 0, 0);

    }

    public override void Update()
    {
        base.Update();
           if (holdingObject)
           {
             RotateObject();
           }   

    }

    public bool CheckIfHoldingObject()
    {
        return holdingObject;
    }

    private void CheckForWall()
    {
        Ray ray = cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        if (Physics.Raycast(ray, out raycastHit, wallDistanceCheck))
        {
            objectHeld.transform.position = raycastHit.point - gameObject.transform.forward * gameObject.transform.localScale.x;
        }
    }



}
