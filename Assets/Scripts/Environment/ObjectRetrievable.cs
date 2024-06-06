using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ObjectRetrievable : MonoBehaviour, IRetrievable
{
    [Header("Openable Config")]
    [SerializeField] private InputManager inputManager;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private float raycastDistance = 3f;
    [SerializeField] private LayerMask interactableLayerMask = ~0;

    [SerializeField] private Sprite pickUpMessage;
    public Sprite PickUpMessage => pickUpMessage;

    private void OnEnable()
    {
        inputManager.InteractEvent += OnRetrieveObject;
    }

    private void OnDisable()
    {
        inputManager.InteractEvent -= OnRetrieveObject;
    }

    private void OnRetrieveObject()
    {
        if (IsMouseLookingAtObject() && playerController.GetObjectGrabbable() == null)
        {
            RetrieveObject();
        }
    }

    private bool IsMouseLookingAtObject()
    {
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, raycastDistance, interactableLayerMask))
        {
            Debug.DrawRay(ray.origin, ray.direction * hit.distance, Color.green, 2f);
            return hit.collider.gameObject.GetComponent<ObjectRetrievable>() && hit.transform == transform;
        }
        else
        {
            Debug.DrawRay(ray.origin, ray.direction * 1000, Color.red, 2f);
        }
        return false;
    }

    private void RetrieveObject()
    {
        Destroy(gameObject);
    }
}
