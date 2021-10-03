using UnityEngine;

public class MovableObject : Interactable
{
    [SerializeField]
    private Rigidbody rb = null;

    [SerializeField]
    private float movementSpeed = 0.0f;

    private bool isMoving;
    private IFlyCameraInstance flyCameraInstance;

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
        
        // Get RightAxis from camera but project it to the horizontal plane
        movementRightAxis = this.flyCameraInstance.GetRightAxis();
        movementRightAxis.y = 0.0f;
        movementRightAxis.Normalize();

        // Get ForwardAxis from camera but project it to the horizontal plane
        movementForwardAxis = this.flyCameraInstance.GetForwardAxis();
        movementForwardAxis.y = 0.0f;
        movementForwardAxis.Normalize();
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

        // TODO: test AddForce instead of transform.Translate
    }
}
