using System;
using UnityEngine;

public class PickUpDrop : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private CleaningManager cleaningManager;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private SanityManager sanityManager;
    [SerializeField] private Transform mainCamera;
    [SerializeField] private Transform objectGrabPointTransform;
    [SerializeField] private float maxThrowingForce = 20f;
    [SerializeField] private float forceChargeRateThrow = 5f;
    [SerializeField] private InputManager inputManager;
    [SerializeField] private float holdTime = 1.5f;

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
        if (cleaningManager.GetToolSelector())
            cleaningManager.GetToolSelector().OnToolSwitched += HandleToolSwitched;
    }

    private void OnDisable()
    {
        inputManager.PickUpEvent -= PickUpAndDropObject;
        inputManager.ThrowStartEvent -= StartChargingThrow;
        inputManager.ThrowEndEvent -= ThrowObject;
        if (cleaningManager.GetToolSelector())
            cleaningManager.GetToolSelector().OnToolSwitched -= HandleToolSwitched;
    }

    private void Update()
    {
        if (isChargingThrow)
        {
            currentThrowingForce += forceChargeRateThrow * Time.deltaTime;
            currentThrowingForce = Mathf.Min(currentThrowingForce * scalarThrowingForce, maxThrowingForce);
        }

        if (ObjectGrabbable != null)
        {
            if (sanityManager != null)
            {
                if (sanityManager.isRageActive)
                {
                    if (ObjectGrabbable.gameObject.GetComponent<SnappableObject>() != null)
                        DropObject();
                }
            }
        }
    }

    private void PickUpAndDropObject()
    {
        if (ObjectGrabbable == null)
        {
            if (cleaningManager.GetToolSelector())
            {
                if (cleaningManager.GetToolSelector().CurrentToolIndex == cleaningManager.GetHands())
                {
                    DetectPickUpDrop();
                }
                if (cleaningManager.GetToolSelector().CurrentToolIndex != cleaningManager.GetHands() && cleaningManager.GetToolSelector().CurrentToolIndex != cleaningManager.GetBin())
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
            else
            {
                DetectPickUpDrop();
            }
            
        }
        else if (ObjectGrabbable != null)
        {
            cleaningManager.GetAudioManager().PlaySound(cleaningManager.GetDropEvent());
            DropObject();
        }
    }

    private void DetectPickUpDrop()
    {
        if (Physics.Raycast(mainCamera.position, mainCamera.forward, out RaycastHit raycastHit, cleaningManager.GetInteractionDistance()))
        {
            ObjectGrabbable newObjectGrabbable;

            if (raycastHit.transform.TryGetComponent(out newObjectGrabbable))
            {
                if (!newObjectGrabbable.IsObjectSnapped)
                {
                    if (newObjectGrabbable.gameObject.GetComponent<SnappableObject>() != null && sanityManager != null && sanityManager.isRageActive) return;

                    cleaningManager.GetAudioManager().PlaySound(cleaningManager.GetPickUpEvent());
                    newObjectGrabbable.Grab(objectGrabPointTransform, this.transform);
                    playerController.SetObjectGrabbable(newObjectGrabbable);
                    SetObjectGrabbable(newObjectGrabbable);
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
    
    public float GetHoldTime()
    {
        return holdTime;
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