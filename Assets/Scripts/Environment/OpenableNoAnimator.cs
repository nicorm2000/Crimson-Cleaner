using System.Collections;
using UnityEngine;

public class OpenableNoAnimator : Interactable, IOpenable
{
    [Header("Openable Config")]
    [SerializeField] private Transform openableTransform; // The transform that moves/rotates.
    [SerializeField] private Vector3 openPosition; // Position when fully open.
    [SerializeField] private Vector3 closedPosition; // Position when fully closed.
    [SerializeField] private Vector3 openRotation; // Rotation when fully open.
    [SerializeField] private Vector3 closedRotation; // Rotation when fully closed.
    [SerializeField] private float animationDuration = 1f;
    [SerializeField] private float cooldown = 0f;

    private float lastInteractionTime = -Mathf.Infinity;
    private bool isAnimating = false;

    public bool IsOpen { get; private set; } = false;
    public Sprite InteractMessage => CleaningManager.Instance.GetInteractMessage();
    public bool IsInteractable => !isAnimating && (IsOpen || (Time.time - lastInteractionTime >= cooldown));

    private Coroutine animationCoroutine;

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
            if (animationCoroutine != null)
            {
                StopCoroutine(animationCoroutine);
            }
            animationCoroutine = StartCoroutine(AnimateObject(IsOpen));

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

    private IEnumerator AnimateObject(bool open)
    {
        isAnimating = true;

        Vector3 startPosition = openableTransform.localPosition;
        Vector3 endPosition = open ? openPosition : closedPosition;

        Quaternion startRotation = openableTransform.localRotation;
        Quaternion endRotation = Quaternion.Euler(open ? openRotation : closedRotation);

        float timeElapsed = 0f;

        while (timeElapsed < animationDuration)
        {
            openableTransform.localPosition = Vector3.Lerp(startPosition, endPosition, timeElapsed / animationDuration);
            openableTransform.localRotation = Quaternion.Slerp(startRotation, endRotation, timeElapsed / animationDuration);

            timeElapsed += Time.deltaTime;
            yield return null;
        }

        openableTransform.localPosition = endPosition;
        openableTransform.localRotation = endRotation;

        isAnimating = false;
    }
}