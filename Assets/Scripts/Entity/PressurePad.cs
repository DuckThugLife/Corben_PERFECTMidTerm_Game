using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PressurePad : MonoBehaviour
{
    [SerializeField] private float checkRadius;
    [SerializeField] public Transform cubeAttachPoint;
    [SerializeField] private LayerMask pressurePadLayer;

    public UnityEvent onCubePlaced;
    public UnityEvent onCubeRemoved;


    

    private void OnCollisionEnter(Collision collision)
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, checkRadius, pressurePadLayer);
        Debug.Log("Robot Collision, is the collider gameobject have the tag Robot? " + collision.collider.gameObject.CompareTag("Robot"));

        if (collision.gameObject.CompareTag("Robot"))
        {
            onCubePlaced.Invoke();
            return;
        }

        foreach (Collider collider in hitColliders)
        {
            if (collider.CompareTag("PickCube"))
            {
                onCubePlaced?.Invoke();
                break;
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("PickCube") || collision.gameObject.CompareTag("Robot"))
        {
            onCubeRemoved?.Invoke();
        }
       
    }

}
