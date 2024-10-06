using System;
using UnityEngine;

public class RetrievableObject : Interactable, IRetrievable
{
    private bool isObjectPickedUp = false;
    public bool IsObjectPickedUp => isObjectPickedUp;

    public Sprite InteractMessage => CleaningManager.Instance.GetInteractMessage();

    public event Action ObjectRetrievedEvent;
    public event Action<GameObject> ObjectRetrievedEventGO;

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
        if (SanityManager.Instance.isRageActive) return;

        if (playerController.GetObjectGrabbable() == null)
        {
            Debug.Log("Retrieve Object: " + name);

            if (soundEvent != null && audioManager != null)
                audioManager.PlaySound(soundEvent);

            isObjectPickedUp = true;
            ObjectRetrievedEvent?.Invoke();
            ObjectRetrievedEventGO?.Invoke(gameObject);

            gameObject.SetActive(false);
            // Destroy(gameObject); // Example: Destroy the object after retrieval
        }
    }


}
