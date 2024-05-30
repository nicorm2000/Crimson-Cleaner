using UnityEngine;

public class PickUpDrop : MonoBehaviour
{
    [SerializeField] private InputManager inputManager;
    [SerializeField] private CleaningManager cleaningManager;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private Transform mainCamera;
    [SerializeField] private Transform objectGrabPointTransform;
    [SerializeField] private LayerMask pickupLayerMask;
    [SerializeField] float pickUpDistance = 3f;

    private ObjectGrabbable objectGrabbable;

    private void OnEnable()
    {
        inputManager.PickUpEvent += PickUpAndDropObject;
        cleaningManager.GetToolSelector().OnToolSwitched += HandleToolSwitched;
    }

    private void OnDisable()
    {
        inputManager.PickUpEvent -= PickUpAndDropObject;
        cleaningManager.GetToolSelector().OnToolSwitched -= HandleToolSwitched;
    }

    private void PickUpAndDropObject()
    {
        if (objectGrabbable == null && cleaningManager.GetToolSelector().CurrentToolIndex == 2)
        {
            if (Physics.Raycast(mainCamera.position, mainCamera.forward, out RaycastHit raycastHit, pickUpDistance))
            {
                if (raycastHit.transform.TryGetComponent(out objectGrabbable))
                {
                    objectGrabbable.Grab(objectGrabPointTransform);
                    playerController.SetObjectGrabbable(objectGrabbable);
                }
            }
        }
        else if (objectGrabbable != null)
        {
            DropObject();
        }
    }

    private void DropObject()
    {
        if (objectGrabbable != null)
        {
            objectGrabbable.Drop();
            objectGrabbable = null;
        }

        playerController.ClearObjectGrabbable();
    }

    private void HandleToolSwitched(int newToolIndex)
    {
        if (newToolIndex != 2 && objectGrabbable != null)
        {
            DropObject();
        }
    }
}