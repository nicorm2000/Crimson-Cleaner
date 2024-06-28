using UnityEngine;
using UnityEngine.InputSystem;

public abstract class Interactable : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] protected InputManager inputManager;
    [SerializeField] protected float raycastDistance = 3f;
    [SerializeField] protected LayerMask interactableLayerMask = ~0;

    [Header("Audio Config")]
    [SerializeField] protected AudioManager audioManager;
    [SerializeField] protected string soundEvent;

    protected virtual void OnEnable()
    {
        inputManager.InteractEvent += TriggerInteraction;
    }

    protected virtual void OnDisable()
    {
        inputManager.InteractEvent -= TriggerInteraction;
    }

    protected virtual void TriggerInteraction()
    {
        if (IsMouseLookingAtObject())
        {
            PlayerController playerController = GetPlayerController();

            PerformInteraction(playerController);
            if (!string.IsNullOrEmpty(soundEvent))
            {
                audioManager.PlaySound(soundEvent);
            }
        }
    }

    protected bool IsMouseLookingAtObject()
    {
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());

        if (Physics.Raycast(ray, out RaycastHit hit, raycastDistance, interactableLayerMask))
        {
            return hit.transform == transform;
        }

        return false;
    }

    protected abstract void PerformInteraction(PlayerController playerController);

    private PlayerController GetPlayerController()
    {
        return FindObjectOfType<PlayerController>();
    }
}
