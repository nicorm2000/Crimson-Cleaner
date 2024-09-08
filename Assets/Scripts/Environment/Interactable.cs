using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private EnvironmentRaycaster environmentRaycaster;

    [Header("Audio Config")]
    [SerializeField] protected AudioManager audioManager;
    [SerializeField] protected string soundEvent;
    [SerializeField] protected string soundEvent2;

    protected virtual void OnEnable()
    {
        environmentRaycaster.InteractableObjectEvent += TriggerInteraction;
    }

    protected virtual void OnDisable()
    {
        environmentRaycaster.InteractableObjectEvent -= TriggerInteraction;
    }

    protected virtual void TriggerInteraction(GameObject go)
    {
        if (go == this.gameObject)
        {
            PlayerController playerController = GetPlayerController();

            PerformInteraction(playerController);
        }
    }

    protected abstract void PerformInteraction(PlayerController playerController);

    private PlayerController GetPlayerController()
    {
        return FindObjectOfType<PlayerController>();
    }
}
