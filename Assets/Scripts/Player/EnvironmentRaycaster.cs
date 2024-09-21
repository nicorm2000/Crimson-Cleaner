using System;
using UnityEngine;

public class EnvironmentRaycaster : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private InputManager inputManager;
    [SerializeField] private float raycastDistance = 3f;
    [SerializeField] private Transform cameraTransform;

    public event Action<GameObject> InteractableObjectEvent;

    private void OnEnable()
    {
        inputManager.InteractEvent += OnInteract;
    }

    private void OnDisable()
    {
        inputManager.InteractEvent -= OnInteract;
    }

    private void OnInteract()
    {
        IsMouseLookingAtObject();
    }

    protected void IsMouseLookingAtObject()
    {
        RaycastHit hit;

        if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out hit, raycastDistance))
        {
            Interactable[] interactables = hit.collider.gameObject.GetComponents<Interactable>();

            if (interactables.Length > 0)
            {
                InteractableObjectEvent?.Invoke(hit.collider.gameObject);
            }
        }
    }
}
