using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[DefaultExecutionOrder(-100)]
public class PlayerInput : MonoBehaviour
{



    [field: SerializeField] public float mouseScrollsensitivity { get; private set; }
    [field: SerializeField] public float mouseSensitivity { get; private set; }
    
    public float horizontal { get; private set; }
    public float vertical { get; private set; }
    public float mouseX { get; private set; }
    public float mouseY { get; private set; }
    public Vector2 scrollWheel { get; private set; }


    public bool middleMouse { get; private set; }
    public bool escape { get; private set; }
    public bool sprintHeld { get; private set; }
    public bool ctrlHeld { get; private set; }
    public bool jumpPressed { get; private set; }
    public bool activatePressed { get; private set; }
    public bool primaryShootPressed { get; private set; }
    public bool secondaryShootPressed { get; private set; }
    public bool weapon1Pressed { get; private set; }
    public bool weapon2Pressed { get; private set; }

    public bool commandPressed { get; private set; }

    private bool clear;

    //Singleton
    private static PlayerInput instance;

    private void Awake()
    {
         if (instance != null && instance != this)
         {
                Destroy(instance);
                return;
         }

         instance = this;

    }


    
    void Update()
    {
        ClearInputs();
        ProcessInputs();
    }

    public static PlayerInput GetInstance()
    {
        return instance;
    }

    private void ProcessInputs()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;
        
        scrollWheel = Input.mouseScrollDelta * mouseScrollsensitivity;

        sprintHeld = sprintHeld || Input.GetButton("Sprint");
        ctrlHeld = ctrlHeld || Input.GetButton("Left Ctrl");
        jumpPressed = jumpPressed || Input.GetButtonDown("Jump");
        escape = Input.GetButtonDown("Escape");
        activatePressed = activatePressed || Input.GetKeyDown(KeyCode.E);

        middleMouse = middleMouse || Input.GetButtonDown("Middle Mouse");
        primaryShootPressed = primaryShootPressed || Input.GetButtonDown("Fire1");
        secondaryShootPressed = secondaryShootPressed || Input.GetButtonDown("Fire2");

        weapon1Pressed = weapon1Pressed || Input.GetKeyDown(KeyCode.Alpha1);
        weapon2Pressed = weapon2Pressed || Input.GetKeyDown(KeyCode.Alpha2);

        commandPressed = commandPressed || Input.GetKeyDown(KeyCode.Q);
    }

    private void FixedUpdate()
    {
        clear = true;
    }

    public void ClearInputs()
    {
        if (!clear)
            return;

        horizontal = 0;
        vertical = 0;
        mouseX = 0;
        mouseY = 0;
        scrollWheel = Vector2.zero;

        sprintHeld = false;
        jumpPressed = false;
        activatePressed = false;
        escape = false;
        ctrlHeld = false;

        middleMouse = false;
        primaryShootPressed = false;
        secondaryShootPressed = false;

        weapon1Pressed = false;
        weapon2Pressed = false;

        commandPressed = false;

    }

    public void ChangePlayerMouseSensitivity(Slider slider)
    {
        mouseSensitivity = slider.value;
    }

}
