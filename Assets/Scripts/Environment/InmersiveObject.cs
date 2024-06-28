using UnityEngine;

public class InmersiveObject : Interactable, IInmersible
{
    [SerializeField] private Animator animator;
    [SerializeField] private string animatorTriggerName;
    [SerializeField] private Sprite interactMessage;

    public Sprite InteractMessage => interactMessage;

    public void Interact(PlayerController playerController)
    {
        if (playerController.GetObjectGrabbable() == null)
        {
            animator.SetTrigger(animatorTriggerName);
        }
    }

    protected override void PerformInteraction(PlayerController playerController)
    {
        Interact(playerController);
    }
}
