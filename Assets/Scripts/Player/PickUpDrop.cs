using System;
using UnityEngine;

public class PickUpDrop : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private CleaningManager cleaningManager;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private Transform mainCamera;
    [SerializeField] private Transform objectGrabPointTransform;
    [SerializeField] private LayerMask pickupLayerMask;
    [SerializeField] private float maxThrowingForce = 20f;
    [SerializeField] private float forceChargeRate = 5f;
    [SerializeField] private InputManager inputManager;

    private ObjectGrabbable ObjectGrabbable;
    private float currentThrowingForce;
    private float scalarThrowingForce = 1f;
    private bool isChargingThrow;

    public event Action PickUpUnavailableEvent;

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
            currentThrowingForce = Mathf.Min(currentThrowingForce * scalarThrowingForce, maxThrowingForce);
        }

        if (ObjectGrabbable != null && SanityManager.Instance.isRageActive)
        {
            if (ObjectGrabbable.gameObject.GetComponent<SnappableObject>() != null)
                DropObject();
        }
    }

    private void PickUpAndDropObject()
    {
        if (ObjectGrabbable == null && cleaningManager.GetToolSelector().CurrentToolIndex == 2)
        {
            if (Physics.Raycast(mainCamera.position, mainCamera.forward, out RaycastHit raycastHit, cleaningManager.GetInteractionDistance()))
            {
                ObjectGrabbable newObjectGrabbable;

                if (raycastHit.transform.TryGetComponent(out newObjectGrabbable))
                {
                    if (!newObjectGrabbable.IsObjectSnapped)
                    {
                        if (newObjectGrabbable.gameObject.GetComponent<SnappableObject>() != null && SanityManager.Instance.isRageActive) return;

                        cleaningManager.GetAudioManager().PlaySound(cleaningManager.GetPickUpEvent());
                        newObjectGrabbable.Grab(objectGrabPointTransform, this.transform);
                        playerController.SetObjectGrabbable(newObjectGrabbable);
                        SetObjectGrabbable(newObjectGrabbable);

                    }
                }
            }
        }
        else if (ObjectGrabbable != null)
        {
            cleaningManager.GetAudioManager().PlaySound(cleaningManager.GetDropEvent());
            DropObject();
        }
        else
        {
            if (cleaningManager.GetToolSelector().CurrentToolIndex != 2)
            {
                if (Physics.Raycast(mainCamera.position, mainCamera.forward, out RaycastHit raycastHit, cleaningManager.GetInteractionDistance()))
                {
                    ObjectGrabbable newObjectGrabbable;

                    if (raycastHit.transform.TryGetComponent(out newObjectGrabbable))
                    {
                        PickUpUnavailableEvent?.Invoke();
                    }
                }
            }
        }
    }

    public void DropObject()
    {
        if (ObjectGrabbable != null)
        {
            ObjectGrabbable.Drop();
            ObjectGrabbable = null;
        }

        playerController.ClearObjectGrabbable();
    }

    private void HandleToolSwitched(int newToolIndex)
    {
        if (newToolIndex != 2 && ObjectGrabbable != null)
        {
            DropObject();
        }
    }

    private void StartChargingThrow()
    {
        if (ObjectGrabbable != null)
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

    public ObjectGrabbable GetObjectGrabbable()
    {
        return ObjectGrabbable;
    }

    public void SetObjectGrabbable(ObjectGrabbable objectGrabbable)
    {
        ObjectGrabbable = objectGrabbable;
    }

    private void ThrowObject()
    {
        if (ObjectGrabbable != null)
        {
            cleaningManager.GetAudioManager().PlaySound(cleaningManager.GetThrowEvent());
            Vector3 throwDirection = mainCamera.forward;
            ObjectGrabbable.Throw(currentThrowingForce, throwDirection);

            ObjectGrabbable = null;
            isChargingThrow = false;
        }

        playerController.ClearObjectGrabbable();
    }

    public void AlterCurrentThrowingForce(float scalar)
    {
        scalarThrowingForce = scalar;
        maxThrowingForce *= 2f;
    }

    public void ReduceCurrentThrowingForce(float scalar)
    {
        scalarThrowingForce = scalar;
        maxThrowingForce *= 1f;
    }
}