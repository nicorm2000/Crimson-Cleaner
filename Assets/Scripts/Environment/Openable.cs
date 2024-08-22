using UnityEngine;

public class Openable : Interactable, IOpenable
{
    [Header("Openable Config")]
    [SerializeField] private Animator openableAnimator;
    [SerializeField] private Sprite interactMessage;
    [SerializeField] private float cooldown = 0f;

    private float lastInteractionTime = -Mathf.Infinity;

    public bool IsOpen { get; private set; } = false;
    public Sprite InteractMessage => interactMessage;
    public bool IsInteractable => IsOpen || (Time.time - lastInteractionTime >= cooldown);

    private readonly string openableOpen = "Open";

    public void Interact(PlayerController playerController)
    {
        if (IsInteractable)
        {
            ToggleObjectState(playerController);
            lastInteractionTime = Time.time;
        }
        else
        {
            Debug.Log("Cooldown active, cannot interact yet.");
        }
    }

    protected override void PerformInteraction(PlayerController playerController)
    {
        Interact(playerController);
    }

    private void ToggleObjectState(PlayerController playerController)
    {
        if (playerController.GetObjectGrabbable() == null)
        {
            IsOpen = !IsOpen;
            openableAnimator.SetBool(openableOpen, IsOpen);
            Debug.Log((IsOpen ? "Open" : "Close") + " Object: " + name);

            if (!string.IsNullOrEmpty(soundEvent))
            {
                if (!string.IsNullOrEmpty(soundEvent2))
                {
                    if (!IsOpen)
                    {
                        audioManager.PlaySound(soundEvent);
                    }
                    else
                    {
                        audioManager.PlaySound(soundEvent2);
                    }
                }
                else
                {
                    audioManager.PlaySound(soundEvent);
                }
            }

            if (!IsOpen)
            {
                lastInteractionTime = Time.time;
            }
        }
    }
}