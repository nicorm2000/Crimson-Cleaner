using System.Collections;
using UnityEngine;

public class Cat : Interactable, IInteractable
{
    [Header("Config")]
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
            catAnimator.SetTrigger(catAnimatorTriggerString);
            audioManager.PlaySound(soundEvent);
            isPettable = false;
            StartCoroutine(CooldownCoroutine());
        }
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