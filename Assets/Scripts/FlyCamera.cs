using UnityEngine;

public interface FlyCameraInstance
{
    public Vector3 GetForward();
}

public class FlyCamera : MonoBehaviour, FlyCameraInstance
{
    [SerializeField]
    private float movementSpeed = 0.0f;
    [SerializeField]
    private float rotationSpeed = 0.0f;

    private static FlyCameraInstance flyCameraInstance;

    private Vector2 lastFrameMousePosition;

    public static FlyCameraInstance FlyCameraInstance 
    { 
        get => flyCameraInstance;
    }

    private void Awake()
    {
        // TODO: Find a better place for next lines if any
        // Lock cursos so it stays at the middle of the screen.
        Cursor.lockState = CursorLockMode.Locked;

        // Singleton
        if (flyCameraInstance == null)
        {
            flyCameraInstance = this;
        }
    }

    public void Move(float horizontalInput, float verticalInput)
    {
        Vector3 translationDirection = new Vector3(horizontalInput, 0.0f, verticalInput);
        transform.Translate(translationDirection * movementSpeed);
    }
    public void RotateCamera(float mouseX, float mouseY)
    {
        // transform.y rotates relative to world so X axis do not influence it.
        transform.Rotate(new Vector3(0, mouseX, 0) * rotationSpeed, relativeTo: Space.World);
        transform.Rotate(new Vector3(-mouseY, 0, 0) * rotationSpeed, relativeTo: Space.Self);
    }

    public Vector3 GetForward()
    {
        return transform.forward;
    }
}
