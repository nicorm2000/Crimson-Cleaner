using UnityEngine;

public class InmersiveObject : Interactable, IInmersible
{
    [SerializeField] private Animator animator;
    [SerializeField] private string animatorTriggerName;

    public Sprite InteractMessage => CleaningManager.Instance.GetInteractMessage();

    public void Interact(PlayerController playerController)
    {
        if (playerController.GetObjectGrabbable() == null)
        {
            animator.SetTrigger(animatorTriggerName);
            audioManager.PlaySound(soundEvent);
        }
    }

    protected override void PerformInteraction(PlayerController playerController)
    {
        Interact(playerController);
    }
}
