using UnityEngine;

public interface IFlyCameraInstance
{
    public Vector3 GetRightAxis();
    public Vector3 GetForwardAxis();
}

public class FlyCamera : MonoBehaviour, IFlyCameraInstance
{
    [SerializeField]
    private Rigidbody rb = null;
    [SerializeField]
    private PlayerInput playerInput = null;
    [SerializeField]
    private PlayerInteractionSubclass playerInteractionSubclass = null;

    [SerializeField]
    private float movementSpeed = 0.0f;
    [SerializeField]
    private float rotationSpeed = 0.0f;

    private static IFlyCameraInstance flyCameraInstance;

    private Vector2 lastFrameMousePosition;

    public static IFlyCameraInstance FlyCameraInstance 
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

    private void Update()
    {
        if (!playerInteractionSubclass.IsInteractionInProgress())
        {
            // Move
            float horizontalInput = playerInput.KeyboardHorizontalInput;
            float verticalInput = playerInput.KeyboardVerticalInput;
            Vector3 translationDirection = new Vector3(horizontalInput, 0.0f, verticalInput);
            float myFloat = movementSpeed * Time.deltaTime;
            rb.AddForce(horizontalInput * myFloat * transform.right);
            rb.AddForce(verticalInput * myFloat * transform.forward);

            // Rotate
            float mouseX = playerInput.MouseX;
            float mouseY = playerInput.MouseY;
            // transform.y rotates relative to world so X axis do not influence it.
            transform.Rotate(new Vector3(0, mouseX, 0) * rotationSpeed, relativeTo: Space.World);
            transform.Rotate(new Vector3(-mouseY, 0, 0) * rotationSpeed, relativeTo: Space.Self);
        }
    }

    public Vector3 GetRightAxis()
    {
        return transform.right;
    }
    public Vector3 GetForwardAxis()
    {
        return transform.forward;
    }
}
