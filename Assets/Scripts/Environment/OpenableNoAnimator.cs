using UnityEngine;

public class OpenableNoAnimator : Interactable, IOpenable
{
    [Header("Openable Config")]
    [SerializeField] private Transform startTransform;
    [SerializeField] private Transform endTransform;
    [SerializeField] private float animationDuration = 1f;
    [SerializeField] private float cooldown = 0f;

    private float lastInteractionTime = -Mathf.Infinity;
    private bool isAnimating = false;
    private float animationTime = 0f;

    public bool IsOpen { get; private set; } = false;
    public Sprite InteractMessage => CleaningManager.Instance.GetInteractMessage();
    public bool IsInteractable => !isAnimating && (Time.time - lastInteractionTime >= cooldown);

    private Vector3 initialLocalPosition;
    private Quaternion initialLocalRotation;

    private void Start()
    {
        initialLocalPosition = transform.localPosition;
        initialLocalRotation = transform.localRotation;
    }

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
        if (playerController.GetObjectGrabbable() == null && !isAnimating)
        {
            IsOpen = !IsOpen;
            StartCoroutine(AnimateObject(IsOpen));

            if (!string.IsNullOrEmpty(soundEvent))
            {
                if (!string.IsNullOrEmpty(soundEvent2))
                {
                    audioManager.PlaySound(IsOpen ? soundEvent2 : soundEvent);
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

    private System.Collections.IEnumerator AnimateObject(bool opening)
    {
        isAnimating = true;
        animationTime = 0f;

        Vector3 startPos = opening ? initialLocalPosition : endTransform.localPosition;
        Vector3 endPos = opening ? endTransform.localPosition : initialLocalPosition;

        Quaternion startRot = opening ? initialLocalRotation : endTransform.localRotation;
        Quaternion endRot = opening ? endTransform.localRotation : initialLocalRotation;

        while (animationTime < animationDuration)
        {
            animationTime += Time.deltaTime;
            float t = Mathf.Clamp01(animationTime / animationDuration);

            transform.localPosition = Vector3.Lerp(startPos, endPos, t);
            transform.localRotation = Quaternion.Lerp(startRot, endRot, t);

            yield return null;
        }

        transform.localPosition = endPos;
        transform.localRotation = endRot;

        isAnimating = false;
    }
}