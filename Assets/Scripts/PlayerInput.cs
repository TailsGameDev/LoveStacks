using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    float keyboardHorizontalInput;
    float keyboardVerticalInput;

    private float mouseX;
    private float mouseY;

    public float MouseX 
    { 
        get => mouseX; 
    }
    public float MouseY 
    { 
        get => mouseY;
    }
    public float KeyboardHorizontalInput 
    { 
        get => keyboardHorizontalInput; 
    }
    public float KeyboardVerticalInput 
    { 
        get => keyboardVerticalInput;
    }

    // Update is called once per frame
    private void Update()
    {
        // Move
        this.keyboardHorizontalInput = Input.GetAxis("Horizontal");
        this.keyboardVerticalInput = Input.GetAxis("Vertical");

        this.mouseX = Input.GetAxis("Mouse X");
        this.mouseY = Input.GetAxis("Mouse Y");
    }
}
