using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpDrop : MonoBehaviour
{
    [SerializeField] private InputManager inputManager;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private Transform mainCamera;
    [SerializeField] private Transform objectGrabPointTransform;
    [SerializeField] private LayerMask pickupLayerMask;

    private ObjectGrabbable objectGrabbable;

    private void OnEnable()
    {
        inputManager.PickUpEvent += PickUpAndDrop;
    }

    private void OnDisable()
    {
        inputManager.PickUpEvent -= PickUpAndDrop;
    }

    private void PickUpAndDrop()
    {
        if (objectGrabbable == null)
        {
            float pickUpDistance = 10f;
            if (Physics.Raycast(mainCamera.position, mainCamera.forward, out RaycastHit raycastHit, pickUpDistance))
            {
                if (raycastHit.transform.TryGetComponent(out objectGrabbable))
                {
                    objectGrabbable.Grab(objectGrabPointTransform);
                    playerController.SetObjectGrabbable(objectGrabbable);
                }
            }
        }
        else
        {
            objectGrabbable.Drop();
            objectGrabbable = null;
            playerController.ClearObjectGrabbable();
        }
    }
}
