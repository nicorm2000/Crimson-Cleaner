using UnityEngine;
using UnityEngine.InputSystem;

public class StealableObject : Interactable, IRetrievable
{
    [Header("Stealable Config")]
    [SerializeField] private Sprite interactMessage;
    [SerializeField] private StealableManager stealableManager;
    [SerializeField] private float moneyAmount = 10f; // Example money amount

    public Sprite InteractMessage => interactMessage;

    private readonly string stealableTrigger = "Retrieve";

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
            stealableManager.PlayMoneySFX();
            stealableManager.AddMoney(moneyAmount);

            Destroy(gameObject); // Example: Destroy the object after retrieval
        }
    }
}