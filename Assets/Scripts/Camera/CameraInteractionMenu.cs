using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraInteractionMenu : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;

    [SerializeField] private Image interactionImage;

    [SerializeField] private float raycastDistance = 2f;
    [SerializeField] private LayerMask interactLayer;

    private void Start()
    {
        SetImageState(false);
    }

    private void Update()
    {
        Ray ray = new(mainCamera.transform.position, mainCamera.transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, raycastDistance, interactLayer))
        {
            IInteractable inmersiveObject = hit.collider.gameObject.GetComponent<PCCanvasController>();

            Sprite interactionSprite = interactionImage.sprite;

            if (inmersiveObject != null)
            {
                AppendInteractableSprites(inmersiveObject, ref interactionSprite);
            }

            UpdateUI(interactionSprite);
        }
        else
        {
            SetImageState(false);
        }
    }

    private void SetImageState(bool state)
    {
        interactionImage.enabled = state;
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
