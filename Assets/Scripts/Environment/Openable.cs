using System;
using UnityEngine;

public class Openable : Interactable, IOpenable
{
    [Header("Openable Config")]
    [SerializeField] private Animator openableAnimator;
    [SerializeField] private float cooldown = 0f;

    [Header("Keys Config")]
    [SerializeField] private KeysManager keysManager;
    [SerializeField] private Key[] requiredKeys;
    public event Action ungrabbedKey;

    private float lastInteractionTime = -Mathf.Infinity;

    public bool IsOpen { get; private set; } = false;
    public Sprite InteractMessage => CleaningManager.Instance.GetInteractMessage();
    public bool IsInteractable => /*IsOpen && */(Time.time - lastInteractionTime >= cooldown);

    private readonly string openableOpen = "Open";

    private bool isDoorOpenable = false;

    public void Interact(PlayerController playerController)
    {
        if (IsInteractable)
        {
            if (requiredKeys.Length > 0)
            {
                if (CheckGrabbedKeys())
                {
                    ToggleObjectState(playerController);
                    lastInteractionTime = Time.time;

                    return;
                }
                else
                {
                    ungrabbedKey?.Invoke();
                    return;
                }
            }

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
                        audioManager.PlaySound(soundEvent, gameObject);
                    }
                    else
                    {
                        audioManager.PlaySound(soundEvent2, gameObject);
                    }
                }
                else
                {
                    audioManager.PlaySound(soundEvent, gameObject);
                }
            }

            if (!IsOpen)
            {
                lastInteractionTime = Time.time;
            }
        }
    }

    private bool CheckGrabbedKeys()
    {
        if (isDoorOpenable) return true;

        int keysCounter = 0;

        foreach (var key in keysManager.GetKeysList())
        {
            for (int i = 0; i < requiredKeys.Length; i++)
            {
                if (key == requiredKeys[i])
                {
                    if (key.isKeyPickedUp)
                    {
                        keysCounter++;
                    }
                    else
                    {
                        Debug.Log("Missing Key: " + key.name);
                    }
                }
            }
        }

        if (keysCounter == requiredKeys.Length)
        {
            isDoorOpenable = true;
            return true;
        }

        return false;
    }
}