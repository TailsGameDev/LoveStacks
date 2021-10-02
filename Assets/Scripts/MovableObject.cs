using UnityEngine;

public class MovableObject : Interactable
{
    [SerializeField]
    private Rigidbody rb = null;

    [SerializeField]
    private float movementSpeed = 0.0f;

    private bool isMoving;
    private FlyCameraInstance flyCameraInstance;

    private Vector3 horizontalMovementAxis;

    public bool IsMoving 
    { 
        get => isMoving; 
    }

    private void Start()
    {
        this.flyCameraInstance = FlyCamera.FlyCameraInstance;
    }

    public override void StartInteraction()
    {
        isMoving = true;
        rb.isKinematic = true;
        horizontalMovementAxis = this.flyCameraInstance.GetRight();
    }
    public void StopInteraction()
    {
        isMoving = false;
        rb.isKinematic = false;
    }

    public void UpadateInteraction(float mouseX, float mouseY)
    {
        Vector3 scaledHorizontalComponent = (horizontalMovementAxis * mouseX);
        Vector3 scaledVerticalComponent = new Vector3(0.0f, mouseY, 0.0f);
        Vector3 movementDirection = scaledHorizontalComponent + scaledVerticalComponent;
        transform.Translate(movementDirection * movementSpeed * Time.deltaTime, Space.World);
    }
}
