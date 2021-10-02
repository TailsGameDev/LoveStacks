using UnityEngine;

public class MovableObject : Interactable
{
    private bool isMoving;
    private FlyCameraInstance flyCameraInstance;

    private Vector3 horizontalMoveAxis;
    private Vector3 verticalMoveAxis;

    private void Awake()
    {
        this.verticalMoveAxis = Vector3.up;
    }

    private void Start()
    {
        this.flyCameraInstance = FlyCamera.FlyCameraInstance;
    }

    public override void Interact()
    {
        isMoving = !isMoving;
        Vector3 cameraForward = this.flyCameraInstance.GetForward();
        this.horizontalMoveAxis = Vector3.Cross(cameraForward, Vector3.up).normalized;
    }

    private void Update()
    {
        if (isMoving)
        {
            
            

            
        }
    }
}
