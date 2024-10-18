using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayerMouseBehaviour : MonoBehaviour
{
    private PlayerInput input;

    [Header("Player Turn")]
    [SerializeField] private float turnSpeed;    
    
    void Start()
    {
        input = PlayerInput.GetInstance();
    }

    
    void Update()
    {
        RotatePlayer();
    }

    private void RotatePlayer()
    {
        transform.Rotate(Vector3.up* turnSpeed * Time.deltaTime * input.mouseX);
    }
}
