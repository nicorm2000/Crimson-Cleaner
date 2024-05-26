using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CameraInteraction : MonoBehaviour
{
    [SerializeField] private float interactionDistance;
    [SerializeField] private LayerMask interactableLayer;
    [SerializeField] private TextMeshProUGUI interactionText;

    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
        interactionText.enabled = false; 
    }

    void Update()
    {
        DetectInteractableObject();
    }

    void DetectInteractableObject()
    {
        Ray ray = new Ray(mainCamera.transform.position, mainCamera.transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, interactionDistance, interactableLayer))
        {
            if (hit.collider.TryGetComponent<InteractableObject>(out var interactableObject))
            {
                interactionText.enabled = true;

                if (interactableObject.isObjectPickedUp)
                    interactionText.text = interactableObject.rotateMessage;
                else
                    interactionText.text = interactableObject.interactionMessage;
            }
        }
        else
        {
            interactionText.enabled = false;
        }
    }
}
