using UnityEngine;
using UnityEngine.UI;

public class CameraInteractionMenu : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Van van;

    [SerializeField] private Image interactionImage;

    [SerializeField] private float defaultRaycastDistance = 2f;
    [SerializeField] private float pcRaycastDistance = 1f;
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

        Ray ray = new(mainCamera.transform.position, mainCamera.transform.forward);
        float raycastDistance = defaultRaycastDistance;

        if (Physics.Raycast(ray, out RaycastHit hit, raycastDistance, interactLayer))
        {
            IInteractable pcObject = hit.collider.gameObject.GetComponent<PCCanvasController>();
            IInteractable catObject = hit.collider.gameObject.GetComponent<Cat>();
            IInteractable vanObject = hit.collider.gameObject.GetComponent<Van>();
            IOpenable openableObject = hit.collider.gameObject.GetComponent<OpenableNoAnimator>();

            Sprite interactionSprite = interactionImage.sprite;

            if (pcObject != null)
            {
                if (Physics.Raycast(ray, out hit, pcRaycastDistance, interactLayer))
                {
                    AppendInteractableSprites(pcObject, ref interactionSprite);
                }
            }
            if (catObject != null)
            {
                AppendInteractableSprites(catObject, ref interactionSprite);
            }
            if (vanObject != null)
            {
                AppendInteractableSprites(vanObject, ref interactionSprite);
            }
            if (openableObject != null)
            {
                AppendInteractableSprites(openableObject, ref interactionSprite);
            }

            UpdateUI(interactionSprite);
        }
        else
        {
            SetImageState(false);
        }
    }

    private void DisableImages()
    {
        SetImageState(false);
    }

    private void SetImageState(bool state)
    {
        interactionImage.enabled = state;
        interactionImage.sprite = null;
    }

    private void AppendInteractableSprites(IInteractable inmersiveObject, ref Sprite activeSprite)
    {
        SetImageState(true);
        activeSprite = inmersiveObject.InteractMessage;
    }

    private void UpdateUI(Sprite activeSprite)
    {
        interactionImage.sprite = activeSprite;
        interactionImage.enabled = activeSprite != null;
    }
}
