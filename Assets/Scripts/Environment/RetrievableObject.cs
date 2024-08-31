using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RetrievableObject : Interactable, IRetrievable
{
    private bool isObjectPickedUp = false;
    public bool IsObjectPickedUp => isObjectPickedUp;

    public Sprite InteractMessage => CleaningManager.Instance.GetInteractMessage();

    public event Action ObjectRetrievedEvent;

    public void Interact(PlayerController playerController)
    {
        ToggleObjectState(playerController);
    }

    protected override void PerformInteraction(PlayerController playerController)
    {
        Interact(playerController);
    }

    private void ToggleObjectState(PlayerController playerController)
    {
        if (playerController.GetObjectGrabbable() == null)
        {
            Debug.Log("Retrieve Object: " + name);

            // Perform additional logic (e.g., play sound, add money)
            //stealableManager.PlayMoneySFX();

            isObjectPickedUp = true;
            ObjectRetrievedEvent?.Invoke();

            gameObject.SetActive(false);
            //Destroy(gameObject); // Example: Destroy the object after retrieval
        }
    }


}
