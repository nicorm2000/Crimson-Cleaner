using UnityEngine;

public class Openable : Interactable, IOpenable
{
    [Header("Openable Config")]
    [SerializeField] private Animator openableAnimator;
    [SerializeField] private Sprite interactMessage;

    public bool IsOpen { get; private set; } = false;
    public Sprite InteractMessage => interactMessage;

    private readonly string openableOpen = "Open";

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
        }
    }
}
