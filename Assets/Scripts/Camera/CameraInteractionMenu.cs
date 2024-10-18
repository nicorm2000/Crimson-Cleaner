using UnityEngine;
using UnityEngine.UI;

public class CameraInteractionMenu : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Van van;
    [SerializeField] private Image interactionImage;

    [SerializeField] private float defaultRaycastDistance = 2f; // Distancia para objetos normales.
    [SerializeField] private float pcRaycastDistance = 1f; // Distancia más corta para la PC.
    [SerializeField] private LayerMask interactLayer;

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

        if (!CheckPCInteraction() && !CheckOtherInteractions())
        {
            SetImageState(false);
        }
    }

    private bool CheckPCInteraction()
    {
        Ray ray = new(mainCamera.transform.position, mainCamera.transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, pcRaycastDistance, interactLayer))
        {
            PCCanvasController pcObject = hit.collider.GetComponent<PCCanvasController>();
            if (pcObject != null)
            {
                UpdateUI(pcObject.InteractMessage);
                return true; 
            }
        }

        return false; 
    }

    private bool CheckOtherInteractions()
    {
        Ray ray = new(mainCamera.transform.position, mainCamera.transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, defaultRaycastDistance, interactLayer))
        {
            IInteractable catObject = hit.collider.gameObject.GetComponent<Cat>();
            IInteractable vanObject = hit.collider.gameObject.GetComponent<Van>();
            IOpenable openableObject = hit.collider.gameObject.GetComponent<OpenableNoAnimator>();

            Sprite interactionSprite = interactionImage.sprite;

            if (catObject != null)
            {
                AppendInteractableSprites(catObject, ref interactionSprite);
                UpdateUI(interactionSprite);
                return true;
            }
            if (vanObject != null)
            {
                AppendInteractableSprites(vanObject, ref interactionSprite);
                UpdateUI(interactionSprite);
                return true;
            }
            if (openableObject != null)
            {
                AppendInteractableSprites(openableObject, ref interactionSprite);
                return true;
            }

            return false;
        }
        else
        {
            SetImageState(false);
            return false;
        }
    }

    private void DisableImages()
    {
        SetImageState(false);
    }

    private void SetImageState(bool state)
    {
        interactionImage.enabled = state;
        if (!state) interactionImage.sprite = null;
    }

    private void AppendInteractableSprites(IInteractable interactable, ref Sprite activeSprite)
    {
        SetImageState(true);
        activeSprite = interactable.InteractMessage;
    }

    private void UpdateUI(Sprite activeSprite)
    {
        interactionImage.sprite = activeSprite;
        interactionImage.enabled = activeSprite != null;
    }
}
