using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSineBounce : MonoBehaviour
{
    private Vector3 startPosition;
    [SerializeField] private bool isBouncing = false;
    [SerializeField] private bool isRotating = false;

    [SerializeField] private float rotatesPerSecond;

    [SerializeField] private float amplitude;
    [SerializeField] private float frequency;
    [SerializeField] private float verticalOffset;

    private void Start()
    {
        startPosition = transform.position;
    }

    private void Update()
    {
        if (isBouncing)
        {
            BounceObject();
        }

        if (isRotating)
        {
            RotateObject();
        }


    }

    private void RotateObject()
    {
        gameObject.transform.Rotate(0, rotatesPerSecond, 0); // rotates on the Y 
    }

    private void BounceObject()
    {
        float newY = Mathf.Sin(Time.time * frequency) * amplitude + verticalOffset;
        gameObject.transform.position = new Vector3(startPosition.x, startPosition.y + newY, startPosition.z);
    }
}
