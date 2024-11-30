using UnityEngine;

public class OpenableNoAnimator : Interactable, IOpenable
{
    [Header("Openable Config")]
    [SerializeField] private Transform startTransform;
    [SerializeField] private Transform endTransform;
    [SerializeField] private float animationDuration = 1f;
    [SerializeField] private float cooldown = 0f;
    [SerializeField] private Sprite interactable = null;

    [Header("Keys Config")]
    [SerializeField] private KeysManager keysManager;
    [SerializeField] private Key[] requiredKeys;
    public event System.Action ungrabbedKey;


    private float lastInteractionTime = -Mathf.Infinity;
    private bool isAnimating = false;
    private float animationTime = 0f;

    public bool IsOpen { get; private set; } = false;
    public Sprite InteractMessage => InteractSelecter();
    public bool IsInteractable => !isAnimating && (Time.time - lastInteractionTime >= cooldown);

    private Vector3 initialLocalPosition;
    private Quaternion initialLocalRotation;

    private bool isDoorOpenable = false;

    private void Start()
    {
        initialLocalPosition = transform.localPosition;
        initialLocalRotation = transform.localRotation;
    }

    private Sprite InteractSelecter()
    {
        if (interactable != null)
        {
            Debug.Log("a");
            return interactable;
        }
        else if (CleaningManager.Instance.GetInteractMessage() != null)
        {
            return CleaningManager.Instance.GetInteractMessage();
        }
        else
        {
            return null;
        }
    }

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
        if (!CanToggleState(playerController)) return;

        IsOpen = !IsOpen;
        StartCoroutine(AnimateObject(IsOpen));
        PlaySound(IsOpen);

        if (!IsOpen) lastInteractionTime = Time.time;
    }

    private bool CanToggleState(PlayerController playerController)
    {
        return playerController.GetObjectGrabbable() == null && !isAnimating;
    }

    private void PlaySound(bool isOpen)
    {
        if (string.IsNullOrEmpty(soundEvent)) return;
        string soundToPlay = isOpen && !string.IsNullOrEmpty(soundEvent2) ? soundEvent2 : soundEvent;
        audioManager.PlaySound(soundToPlay);
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