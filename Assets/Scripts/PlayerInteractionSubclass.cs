using UnityEngine;

public class PlayerInteractionSubclass : PlayerInteraction
{
    [SerializeField]
    private PlayerInput playerInput = null;

    private MovableObject movingObject;

    protected override void Update()
    {
        // Sets the currentInteractable, and is an important part of the proccess that configures MovableObject.IsMoving
        base.Update();

        // Handle movingObject
        {
            // Stop interaction if needed
            if (this.movingObject != null && this.movingObject.IsMoving && HasInputBeenPressed())
            {
                movingObject.StopInteraction();
                this.movingObject = null;
            }

            // Remember the moving object if some interaction has started
            if (CurrentInteractable != null)
            {
                MovableObject movableObject = CurrentInteractable as MovableObject;

                // NOTE: it's impossible to cache an object you just dropped (in this frame) because it
                // must be moving so this script caches the moving object, and the StopInteraction() should
                // set movableObject.IsMoving to false
                // NOTE 2: movableObject will be null if the cast failed
                if (movableObject != null && movableObject.IsMoving)
                {
                    this.movingObject = movableObject;
                }
            }

            // Calls the acctual movement logic on current movingObject if any
            if (this.movingObject != null)
            {
                float mouseY = playerInput.MouseY;
                float horizontalInput = playerInput.KeyboardHorizontalInput;
                float verticalInput = playerInput.KeyboardVerticalInput;
                movingObject.UpadateInteraction(right: horizontalInput, up: mouseY, forward: verticalInput);
            }
        }

        /*
        // Handle Cannon
        {
            if (CurrentInteractable != null)
            {
                CannonInteractable cannon = CurrentInteractable as CannonInteractable;
                if (cannon != null)
                {
                    if (HasInputBeenPressed())
                    {
                        cannon.Shoot();
                    }
                }
            }
        }
        */
    }

    public bool IsInteractionInProgress()
    {
        return this.movingObject != null && this.movingObject.IsMoving;
    }

    protected override bool ShouldDisplayRaycastHitPointImage(bool hasRaycastHitSomething)
    {
        bool shouldDisplay = hasRaycastHitSomething && !IsInteractionInProgress();
        return shouldDisplay;
    }
}
