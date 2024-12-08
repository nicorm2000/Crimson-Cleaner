using System.Collections;
using UnityEngine;

public class Cat : Interactable, IInteractable
{
    [Header("Config")]
    [SerializeField] private PlayerController playerController = null;
    [SerializeField] private ToolAnimatorController toolAnimatorController = null;
    [SerializeField] private Animator catAnimator = null;
    [SerializeField] private string catAnimatorTriggerString = null;
    [SerializeField] private float interactionCooldown = 1f;
    [SerializeField] private Sprite interactSprite = null;
    public Sprite InteractMessage => interactSprite;

    private bool isPettable = true;

    public void Interact(PlayerController playerController)
    {
        if (isPettable)
        {
            toolAnimatorController.TriggerParticularAction(toolAnimatorController.GetPetCatName());
            catAnimator.SetTrigger(catAnimatorTriggerString);
            audioManager.PlaySound(soundEvent);
            isPettable = false;
            StartCoroutine(PetCoroutine());
            StartCoroutine(CooldownCoroutine());
        }
    }

    private IEnumerator PetCoroutine()
    {
        playerController.ToggleMovement(false);
        playerController.ToggleCameraMovement(false);
        yield return new WaitForSeconds(catAnimator.GetCurrentAnimatorStateInfo(0).length);
        playerController.ToggleMovement(true);
        playerController.ToggleCameraMovement(true);

    }

    protected override void PerformInteraction(PlayerController playerController)
    {
        Interact(playerController);
    }

    private IEnumerator CooldownCoroutine()
    {
        yield return new WaitForSeconds(interactionCooldown);
        isPettable = true;
    }
}