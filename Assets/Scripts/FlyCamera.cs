using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyCamera : MonoBehaviour
{
    [SerializeField]
    private float movementSpeed = 0.0f;
    [SerializeField]
    private float rotationSpeed = 0.0f;

    private Vector2 lastFrameMousePosition;

    private void Update()
    {
        // Move
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 translationDirection = new Vector3(horizontalInput, 0.0f, verticalInput);
        transform.Translate(translationDirection * movementSpeed);

        // Rotate Camera
        float mouseX = Input.GetAxis("Mouse X") * rotationSpeed;
        float mouseY = Input.GetAxis("Mouse Y") * rotationSpeed;
        // transform.y rotates relative to world so X axis do not influence it.
        transform.Rotate(new Vector3(0, mouseX, 0), relativeTo: Space.World);
        transform.Rotate(new Vector3(-mouseY, 0, 0), relativeTo: Space.Self);
    }
}
