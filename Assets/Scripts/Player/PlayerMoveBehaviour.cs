using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerMoveBehaviour : MonoBehaviour
{
    private PlayerInput input;

    [Header("Player Movement")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float sprintSpeedMulti;
    [SerializeField] private float gravity = -9.81f;

    [Header("Ground Check")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayerMask;
    [SerializeField] private float groundCheckDistance;


    private CharacterController characterController;

    public bool isGrounded { get; private set; }
    private float moveMultipler = 1;
    private Vector3 playerVelocity;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        input = PlayerInput.GetInstance();
    }

    
    void Update()
    {
        GroundCheck();
        MovePlayer();

    }

    private void MovePlayer()
    {


        moveMultipler = input.sprintHeld ? sprintSpeedMulti : 1;
        characterController.Move((transform.forward * input.vertical + transform.right * input.horizontal) * moveSpeed * Time.deltaTime * moveMultipler);

        // Ground Check
        if (isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = -2f;

        }

        playerVelocity.y += gravity * Time.deltaTime;

        characterController.Move(playerVelocity * Time.deltaTime);

    }

    private void GroundCheck()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckDistance, groundLayerMask);
    }

    public void SetYVelocity(float value)
    {
        playerVelocity.y = value;
    }


    public float GetForwardSpeed()
    {

        return input.vertical * moveSpeed * moveMultipler;
    }

}
