using UnityEngine;
using UnityEngine.UI;

public class CameraInteractionMenu : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private PCCanvasController pCCanvasController;
    [SerializeField] private Van van;
    [SerializeField] private Image[] interactionImages;

    [SerializeField] private float defaultRaycastDistance = 2f; // Distancia para objetos normales.
    [SerializeField] private LayerMask interactLayer;

    private IPickable currentPickableObject;

    private void OnEnable()
    {
        van.SceneTransitioned += DisableImages;
    }

    private void OnDisable()
    {
        van.SceneTransitioned -= DisableImages;
    }

    private void Start()
    {
        DisableImages();
    }

    private void Update()
    {
        if (van.isSceneTransitioned) return;

        DetectInteractableObject();
    }

    private void DetectInteractableObject()
    {
        if (currentPickableObject != null && currentPickableObject.IsObjectPickedUp)
        {
            var activeSprites = new Sprite[interactionImages.Length];
            AppendPickUpSprites(currentPickableObject, ref activeSprites);
            UpdateUI(activeSprites);
            return;
        }

        Ray ray = new(mainCamera.transform.position, mainCamera.transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, defaultRaycastDistance, interactLayer))
        {
            var activeSprites = new Sprite[interactionImages.Length];

            IInteractable catObject = hit.collider.gameObject.GetComponent<Cat>();
            IInteractable vanObject = hit.collider.gameObject.GetComponent<Van>();
            IInteractable pc = hit.collider.gameObject.GetComponent<PCCanvasController>();
            IPickable grabbableObject = hit.collider.gameObject.GetComponent<ObjectGrabbable>();
            IOpenable openableObject = hit.collider.gameObject.GetComponent<OpenableNoAnimator>();

            if (pc != null && !pCCanvasController.isLevelSelected)
            {
                AppendInteractableSprites(pc, ref activeSprites);
            }
            if (grabbableObject != null && !grabbableObject.IsObjectSnapped)
            {
                currentPickableObject = grabbableObject;
                AppendPickUpSprites(grabbableObject, ref activeSprites);
            }
            if (catObject != null)
            {
                AppendInteractableSprites(catObject, ref activeSprites);
            }
            if (vanObject != null)
            {
                AppendInteractableSprites(vanObject, ref activeSprites);
            }
            if (openableObject != null && playerController.GetObjectGrabbable() == null)
            {
                AppendOpenableSprites(openableObject, ref activeSprites);
            }

            UpdateUI(activeSprites);

            return;
        }
        SetImageState(false);
    }

    private void DisableImages()
    {
        SetImageState(false);
    }

    private void SetImageState(bool state)
    {
        foreach (var image in interactionImages)
        {
            image.enabled = state;
        }
    }

    private void AppendPickUpSprites(IPickable pickableObject, ref Sprite[] activeSprites)
    {
        SetImageState(true);
        int index = GetNextAvailableSlot(activeSprites);
        if (pickableObject.IsObjectPickedUp)
        {
            activeSprites[1] = pickableObject.ThrowMessage;
            activeSprites[2] = pickableObject.RotateMessage;
            activeSprites[3] = pickableObject.DropMessage;
        }
        else
        {
            activeSprites[index] = pickableObject.PickUpMessage;
        }
    }

    private void AppendOpenableSprites(IOpenable openableObject, ref Sprite[] activeSprites)
    {
        SetImageState(true);
        int index = GetNextAvailableSlot(activeSprites);
        activeSprites[index] = openableObject.InteractMessage;
    }

    private void AppendInteractableSprites(IInteractable interactable, ref Sprite[] activeSprites)
    {
        SetImageState(true);
        int index = GetNextAvailableSlot(activeSprites);
        activeSprites[index] = interactable.InteractMessage;
    }

    private int GetNextAvailableSlot(Sprite[] activeSprites)
    {
        for (int i = 0; i < activeSprites.Length; i++)
        {
            if (activeSprites[i] == null)
            {
                return i;
            }
        }
        return activeSprites.Length - 1;
    }

    private void UpdateUI(Sprite[] activeSprites)
    {
        for (int i = 0; i < interactionImages.Length; i++)
        {
            interactionImages[i].sprite = activeSprites[i];
            interactionImages[i].enabled = activeSprites[i] != null;
        }
    }
}
