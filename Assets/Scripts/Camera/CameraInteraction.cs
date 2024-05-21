using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CameraInteraction : MonoBehaviour
{
    [SerializeField] private float interactionDistance = 5f;
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
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactionDistance, interactableLayer))
        {
            InteractableObject interactableObject = hit.collider.GetComponent<InteractableObject>();

            if (interactableObject != null)
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
