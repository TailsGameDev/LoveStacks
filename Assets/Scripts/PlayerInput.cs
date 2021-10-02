using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField]
    private FlyCamera flyCamera = null;

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

    // Update is called once per frame
    private void Update()
    {
        // Move
        float keyboardHorizontalInput = Input.GetAxis("Horizontal");
        float keyboardVerticalInput = Input.GetAxis("Vertical");
        flyCamera.Move(keyboardHorizontalInput, keyboardVerticalInput);

        this.mouseX = Input.GetAxis("Mouse X");
        this.mouseY = Input.GetAxis("Mouse Y");

        flyCamera.RotateCamera(mouseX, mouseY);
    }
}
