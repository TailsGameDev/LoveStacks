using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonInteractable : Interactable
{
    private enum ButtonType
    {
        OK,
        QUIT_TUTORIAL,
    }

    [SerializeField]
    private GameObject tutorialToQuit = null;

    [SerializeField]
    private ButtonType buttonType = 0;

    [SerializeField]
    private GameObject explosionVFX = null;

    [SerializeField]
    private Transform spawnPointForExplosionVFX = null;

    [SerializeField]
    private UnityEvent onClick = null;

    private static System.Action quitTutorial;
    
    private void OnEnable()
    {
        if (tutorialToQuit != null)
        {
            ButtonInteractable.quitTutorial += QuitTutorial;
        }
    }
    private void OnDisable()
    {
        if (tutorialToQuit != null)
        {
            ButtonInteractable.quitTutorial -= QuitTutorial;
        }
    }
    private void QuitTutorial()
    {
        tutorialToQuit.SetActive(false);
        SpawnExplosionVFX();
    }
    public void SpawnExplosionVFX()
    {
        if (explosionVFX != null)
        {
            Instantiate(explosionVFX, spawnPointForExplosionVFX.position, Quaternion.identity);
        }
    }

    public override void StartInteraction()
    {
        switch (buttonType)
        {
            case ButtonType.OK:
                onClick?.Invoke();
                break;
            case ButtonType.QUIT_TUTORIAL:
                ButtonInteractable.quitTutorial.Invoke();
                break;
        }
    }
}
