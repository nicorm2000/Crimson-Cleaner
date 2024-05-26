using UnityEngine;

public class PickUpDrop : MonoBehaviour
{
    [SerializeField] private InputManager inputManager;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private Transform mainCamera;
    [SerializeField] private Transform objectGrabPointTransform;
    [SerializeField] private LayerMask pickupLayerMask;
    [SerializeField] float pickUpDistance = 3f;

    private ObjectGrabbable objectGrabbable;
    private InteractableObject interactableObject;


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
            if (Physics.Raycast(mainCamera.position, mainCamera.forward, out RaycastHit raycastHit, pickUpDistance))
            {
                if (raycastHit.transform.TryGetComponent(out objectGrabbable))
                {
                    objectGrabbable.Grab(objectGrabPointTransform);
                    playerController.SetObjectGrabbable(objectGrabbable);
                }

                if (raycastHit.transform.TryGetComponent(out interactableObject))
                {
                    interactableObject.isObjectPickedUp = true;
                }
            }
        }
        else
        {
            objectGrabbable.Drop();
            objectGrabbable = null;
            interactableObject.isObjectPickedUp = false;
            playerController.ClearObjectGrabbable();
        }
    }
}
