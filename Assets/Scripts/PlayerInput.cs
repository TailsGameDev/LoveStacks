using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IDialogInput 
{
}

public class PlayerInput : MonoBehaviour, IDialogInput
{
    float keyboardHorizontalInput;
    float keyboardVerticalInput;

    private float mouseX;
    private float mouseY;

    private bool jumpInput;

    private IDialogInput iDialogInput = null;

    public float KeyboardHorizontalInput 
    { 
        get => keyboardHorizontalInput; 
    }
    public float KeyboardVerticalInput 
    { 
        get => keyboardVerticalInput;
    }

    public float MouseX 
    { 
        get => mouseX; 
    }
    public float MouseY 
    { 
        get => mouseY;
    }

    public bool JumpInput 
    { 
        get => jumpInput; 
    }

    private void Awake()
    {
        if (iDialogInput == null)
        {
            iDialogInput = this;
        }
    }

    // Update is called once per frame
    private void Update()
    {
        // Move
        this.keyboardHorizontalInput = Input.GetAxis("Horizontal");
        this.keyboardVerticalInput = Input.GetAxis("Vertical");

        this.mouseX = Input.GetAxis("Mouse X");
        this.mouseY = Input.GetAxis("Mouse Y");

        this.jumpInput = Input.GetButtonDown("Jump");
    }
}
