using UnityEngine;

public class MovableObject : Interactable
{
    [SerializeField]
    private Rigidbody rb = null;

    [SerializeField]
    private float movementSpeed = 0.0f;

    private bool isMoving;
    private FlyCameraInstance flyCameraInstance;

    private Vector3 movementRightAxis;
    private Vector3 movementForwardAxis;

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
        movementRightAxis = this.flyCameraInstance.GetRightAxis();
        movementForwardAxis = this.flyCameraInstance.GetForwardAxis();
    }
    public void StopInteraction()
    {
        isMoving = false;
        rb.isKinematic = false;
    }

    public void UpadateInteraction(float right, float up, float forward)
    {
        Vector3 scaledRightComponent = movementRightAxis * right;
        Vector3 scaledUpComponent = Vector3.up * up;
        Vector3 scaledForwardComponent = movementForwardAxis * forward;
        Vector3 movementDirection = scaledRightComponent + scaledUpComponent + scaledForwardComponent;
        transform.Translate(movementDirection * movementSpeed * Time.deltaTime, Space.World);
    }
}
