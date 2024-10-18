using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovementBehaviour : MonoBehaviour
{
    private PlayerInput input;

    [Header("Camera Turn")]
    [SerializeField] private float turnSpeed;
    [SerializeField] private bool invertMouse;

    [SerializeField] private float maxYRotation = 85;
    private float camYRotation;

    void Start()
    {
        input = PlayerInput.GetInstance();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

   
    void Update()
    {
        RotateCamera();
    }

    private void RotateCamera()
    {
        camYRotation += Time.deltaTime * input.mouseY * turnSpeed * (invertMouse ? 1 : -1);
        camYRotation = Mathf.Clamp(camYRotation, -maxYRotation, maxYRotation);

        transform.localRotation = Quaternion.Euler(camYRotation, 0, 0);
    }

}
