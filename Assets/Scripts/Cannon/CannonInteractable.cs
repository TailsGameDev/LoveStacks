using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonInteractable : Interactable
{
    [SerializeField]
    private Camera cannonCamera = null;
    [SerializeField]
    private CannonController cannonController = null;
    [SerializeField]
    private Camera flyingCamera = null;
    [SerializeField]
    private LineRenderer cannonLineRenderer = null;

    private float intervalBewteenEnablingAndStartShooting = 0.2f;

    private float timeToEnableShooting;

    public override void StartInteraction()
    {
        timeToEnableShooting = Time.time + intervalBewteenEnablingAndStartShooting;
        SetInteractionState(interacting: true);
    }
    private void SetInteractionState(bool interacting)
    {
        this.enabled = interacting;
        cannonController.enabled = interacting;
        cannonCamera.gameObject.SetActive(interacting);
        flyingCamera.gameObject.SetActive(!interacting);
        cannonLineRenderer.enabled = interacting;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetMouseButtonDown(1))
        {
            SetInteractionState(interacting: false);
        }
        if (Input.GetMouseButtonDown(0))
        {
            if (cannonController.enabled && Time.time > timeToEnableShooting)
            {
                cannonController.Shoot();
            }
        }
    }
}
