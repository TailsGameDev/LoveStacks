using UnityEngine;

public class MovableObject : Interactable
{
    private enum MoveState
    {
        IN_GROUND,
        MOVING_BY_PLAYER,
        IN_THE_STACK,
    }

    [SerializeField]
    private Rigidbody rb = null;

    [SerializeField]
    private MoveState moveState = MoveState.IN_GROUND;

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

    private void Update()
    {
        // if ()
    }

    public override void StartInteraction()
    {
        isMoving = true;

        SetState(isMoving);

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

        SetState(isMoving);
    }
    private void SetState(bool interacting)
    {
        if (interacting)
        {
            rb.useGravity = false;
            rb.drag = 10.0f;
            rb.angularDrag = 1.0f;
        }
        else
        {
            rb.useGravity = true;
            rb.drag = 0.0f;
            rb.angularDrag = 0.05f;
        }
    }

    public void UpadateInteraction(float right, float up, float forward)
    {
        // Does the movement controlled by the player
        Vector3 scaledRightComponent = movementRightAxis * right;
        Vector3 scaledUpComponent = Vector3.up * up;
        Vector3 scaledForwardComponent = movementForwardAxis * forward;
        Vector3 movementDirection = scaledRightComponent + scaledUpComponent + scaledForwardComponent;
        Vector3 force = movementDirection * movementSpeed;
        rb.AddForce(force);
    }
}
