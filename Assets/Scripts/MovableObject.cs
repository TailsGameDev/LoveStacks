using System.Collections.Generic;
using UnityEngine;

public class MovableObject : Interactable
{
    private enum MoveState
    {
        IN_GROUND,
        STACK_START,
        IN_AIR_MOVING_BY_PLAYER,
        FALLING_AFTER_PLAYER_RELEASE,
        IN_THE_STACK,
        FALLING_FROM_STACK,
    }

    [SerializeField]
    private Rigidbody rb = null;

    [SerializeField]
    private MoveState moveState = MoveState.IN_GROUND;

    [SerializeField]
    private float movementSpeed = 0.0f;

    [SerializeField]
    private MeshRenderer meshRenderer = null;
    [SerializeField]
    private Material[] debugMaterials = null;

    private bool isMoving;
    private IFlyCameraInstance flyCameraInstance;

    private Vector3 movementRightAxis;
    private Vector3 movementForwardAxis;

    private static float score;
    private Vector3 lastFramePosition;
    private bool collidingWithObjectInTheStack;
    private bool collidingWithTheGround;

    // NOTE: the list is serialized only for debugging
    [SerializeField]
    private List<MovableObject> inStackCollisions = new List<MovableObject>();

    public bool IsMoving 
    { 
        get => isMoving; 
    }

    private bool IsInTheStack {
        get {
            return moveState == MoveState.IN_THE_STACK || moveState == MoveState.STACK_START;
        }
    }

    private void Start()
    {
        this.flyCameraInstance = FlyCamera.FlyCameraInstance;
    }
    private void Update()
    {
        DoFSMActionLogic();
        DoFSMNextStateAndOnTransitionChangeLogic();
    }
    private void DoFSMActionLogic()
    {
        // Action Logic
        switch (moveState)
        {
            case MoveState.FALLING_FROM_STACK:
                Vector3 thisFramePosition = transform.position;
                MovableObject.score += Mathf.Abs((thisFramePosition - lastFramePosition).sqrMagnitude);
                this.lastFramePosition = thisFramePosition;
                break;
        }
        if (meshRenderer.material.color != debugMaterials[(int)moveState].color)
        {
            meshRenderer.material.color = debugMaterials[(int)moveState].color;
        }
    }
    private void DoFSMNextStateAndOnTransitionChangeLogic()
    {
        // Next State Logic
        MoveState nextMoveState = this.moveState;
        switch (this.moveState)
        {
            case MoveState.IN_GROUND:
                if (this.isMoving)
                {
                    nextMoveState = MoveState.IN_AIR_MOVING_BY_PLAYER;
                }
                break;
            case MoveState.IN_AIR_MOVING_BY_PLAYER:
                if (!this.isMoving)
                {
                    nextMoveState = MoveState.FALLING_AFTER_PLAYER_RELEASE;
                }
                break;
            case MoveState.FALLING_AFTER_PLAYER_RELEASE:
                if (this.isMoving)
                {
                    nextMoveState = MoveState.IN_AIR_MOVING_BY_PLAYER;
                }
                else
                {
                    if (this.collidingWithTheGround)
                    {
                        nextMoveState = MoveState.IN_GROUND;
                    }
                    else if (this.collidingWithObjectInTheStack)
                    {
                        nextMoveState = MoveState.IN_THE_STACK;
                    }
                }
                break;
            case MoveState.IN_THE_STACK:
                if (!this.collidingWithObjectInTheStack)
                {
                    nextMoveState = MoveState.FALLING_FROM_STACK;
                }
                // Theorically the next lines are not needed, but I'm trying to fix
                // bugs in the game by adding them!
                else if (this.collidingWithTheGround)
                {
                    nextMoveState = MoveState.IN_GROUND;
                }
                break;
            case MoveState.FALLING_FROM_STACK:
                if (this.IsMoving)
                {
                    nextMoveState = MoveState.IN_AIR_MOVING_BY_PLAYER;
                }
                else if (this.collidingWithTheGround)
                {
                    nextMoveState = MoveState.IN_GROUND;
                }
                break;
        }

        // On State Change Logic
        if (this.moveState != nextMoveState)
        {
            switch (nextMoveState)
            {
                case MoveState.IN_GROUND:
                    // Remove this object from collections that belong to objects on the stack exclusevely
                    for (int o = inStackCollisions.Count-1; o >= 0; o--)
                    {
                        inStackCollisions[o].RemoveMovableObjectFromInStackCollisions(this);
                        inStackCollisions.RemoveAt(o);
                    }
                    this.collidingWithObjectInTheStack = false;
                    break;
            }
        }

        this.moveState = nextMoveState;
    }

    public override void StartInteraction()
    {
        if (!IsInTheStack)
        {
            SetMovingState(interacting: true);

            // Get RightAxis from camera but project it to the horizontal plane
            movementRightAxis = this.flyCameraInstance.GetRightAxis();
            movementRightAxis.y = 0.0f;
            movementRightAxis.Normalize();

            // Get ForwardAxis from camera but project it to the horizontal plane
            movementForwardAxis = this.flyCameraInstance.GetForwardAxis();
            movementForwardAxis.y = 0.0f;
            movementForwardAxis.Normalize();
        }
    }
    public void StopInteraction()
    {
        SetMovingState(interacting: false);
    }
    private void SetMovingState(bool interacting)
    {
        if (interacting)
        {
            isMoving = true;
            rb.useGravity = false;
            rb.drag = 10.0f;
            rb.angularDrag = 1.5f;
        }
        else
        {
            isMoving = false;
            rb.useGravity = true;
            rb.drag = 1.0f;
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
    
    private void OnCollisionEnter(Collision collision)
    {
        Collider other = collision.collider;
        MovableObject otherMovableObject = other.GetComponent<MovableObject>();
        bool isAMovableObject = otherMovableObject != null;
        if (isAMovableObject)
        {
            if (otherMovableObject.IsInTheStack && !inStackCollisions.Contains(otherMovableObject))
            {
                inStackCollisions.Add(otherMovableObject);
                this.collidingWithObjectInTheStack = true;
            }
        }
        else if (other.tag == "Ground")
        {
            this.collidingWithTheGround = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        Collider other = collision.collider;
        MovableObject otherMovableObject = other.GetComponent<MovableObject>();
        if (otherMovableObject != null)
        {
            if (inStackCollisions.Contains(otherMovableObject))
            {
                RemoveMovableObjectFromInStackCollisions(otherMovableObject);
            }
        }
        else if (other.tag == "Ground")
        {
            this.collidingWithTheGround = false;
        }
    }
    private void RemoveMovableObjectFromInStackCollisions(MovableObject movableObject)
    {
        inStackCollisions.Remove(movableObject);
        if (inStackCollisions.Count == 0)
        {
            this.collidingWithObjectInTheStack = false;
        }
    }
}
