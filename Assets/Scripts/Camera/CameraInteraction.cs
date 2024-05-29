using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CameraInteraction : MonoBehaviour
{
    [SerializeField] private float interactionDistance;
    [SerializeField] private LayerMask interactableLayer;
    [SerializeField] private TextMeshProUGUI interactionText;
    [SerializeField] private CleaningManager cleaningManager;

    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
        interactionText.enabled = false;
    }

    private void Update()
    {
        DetectInteractableObject();
    }

    private void DetectInteractableObject()
    {
        Ray ray = new(mainCamera.transform.position, mainCamera.transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, interactionDistance, interactableLayer))
        {
            if (hit.collider.TryGetComponent<InteractableObject>(out var interactableObject))
            {
                SetTextState(true);
                if (interactableObject.isObjectPickedUp)
                    interactionText.text = interactableObject.rotateMessage;
                else
                    interactionText.text = interactableObject.interactionMessage;
            }
        }
        else
        {
            SetTextState(false);
        }
    }

    private void SetTextState(bool state)
    {
        interactionText.enabled = state;
    }
}