using UnityEngine;

public class PickUpDrop : MonoBehaviour
{
    [SerializeField] private CleaningManager cleaningManager;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private Transform mainCamera;
    [SerializeField] private Transform objectGrabPointTransform;
    [SerializeField] private LayerMask pickupLayerMask;
    [SerializeField] float pickUpDistance = 3f;
    [SerializeField] private float maxThrowingForce = 20f;
    [SerializeField] private float forceChargeRate = 5f;
    public InputManager inputManager;

    private ObjectGrabbable objectGrabbable;
    private float currentThrowingForce;
    private bool isChargingThrow;

    private void OnEnable()
    {
        inputManager.PickUpEvent += PickUpAndDropObject;
        inputManager.ThrowStartEvent += StartChargingThrow;
        inputManager.ThrowEndEvent += ThrowObject;
        cleaningManager.GetToolSelector().OnToolSwitched += HandleToolSwitched;
    }

    private void OnDisable()
    {
        inputManager.PickUpEvent -= PickUpAndDropObject;
        inputManager.ThrowStartEvent -= StartChargingThrow;
        inputManager.ThrowEndEvent -= ThrowObject;
        cleaningManager.GetToolSelector().OnToolSwitched -= HandleToolSwitched;
    }

    private void Update()
    {
        if (isChargingThrow)
        {
            currentThrowingForce += forceChargeRate * Time.deltaTime;
            currentThrowingForce = Mathf.Min(currentThrowingForce, maxThrowingForce);
        }
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

    private void StartChargingThrow()
    {
        if (objectGrabbable != null)
        {
            isChargingThrow = true;
            currentThrowingForce = 0f;
        }
    }

    public float GetCurrentThrowingForce()
    {
        return currentThrowingForce;
    }

    public float GetMaxThrowingForce()
    {
        return maxThrowingForce;
    }

    private void ThrowObject()
    {
        if (objectGrabbable != null)
        {
            Vector3 throwDirection = mainCamera.forward;
            objectGrabbable.Throw(currentThrowingForce, throwDirection);

            objectGrabbable = null;
            isChargingThrow = false;
        }

        playerController.ClearObjectGrabbable();
    }
}