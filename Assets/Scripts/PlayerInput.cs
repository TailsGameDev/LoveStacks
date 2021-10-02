using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField]
    private FlyCamera flyCamera = null;

    // Update is called once per frame
    private void Update()
    {
        // Move
        float keyboardHorizontalInput = Input.GetAxis("Horizontal");
        float keyboardVerticalInput = Input.GetAxis("Vertical");
        flyCamera.Move(keyboardHorizontalInput, keyboardVerticalInput);

        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");
        flyCamera.RotateCamera(mouseX, mouseY);
    }
}
