using UnityEngine;
using UnityEngine.UI;

public class CameraInteraction : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private Image[] interactionImages;
    [SerializeField] private CleaningManager cleaningManager;
    [SerializeField] private PlayerController playerController;

    private Camera mainCamera;
    private IPickable currentPickableObject;

    private void Start()
    {
        mainCamera = Camera.main;
        SetImageState(false);
    }

    private void Update()
    {
        DetectInteractableObject();
    }

    private void DetectInteractableObject()
    {
        Ray ray = new(mainCamera.transform.position, mainCamera.transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, cleaningManager.GetInteractionDistance()))
        {
            var activeSprites = new Sprite[interactionImages.Length];

            ObjectGrabbable pickableObject = hit.collider.gameObject.GetComponent<ObjectGrabbable>();
            StealableObject objectRetrievable = hit.collider.gameObject.GetComponent<StealableObject>();
            Openable openableObject = hit.collider.gameObject.GetComponent<Openable>();
            //ICleanable cleanableObject = hit.collider.gameObject.GetComponent<Clean>() as ICleanable;
            WaterBucket cleanableToolObject = hit.collider.gameObject.GetComponent<WaterBucket>();
            UVLight toggableObject = hit.collider.gameObject.GetComponent<UVLight>();
            WaterFaucetSystem toggableObject2 = hit.collider.gameObject.GetComponent<WaterFaucetSystem>();
            InmersiveObject inmersiveObject = hit.collider.gameObject.GetComponent<InmersiveObject>();

            RetrievableObject objectRetrievable2 = hit.collider.gameObject.GetComponent<RetrievableObject>();

            if (pickableObject != null && !pickableObject.IsObjectSnapped)
            {
                currentPickableObject = pickableObject;
                if (cleaningManager.GetToolSelector().CurrentToolIndex == cleaningManager.GetToolSelector().CleaningToolsLength - 1)
                {
                    AppendPickUpSprites(pickableObject, ref activeSprites);
                }
            }

            if (objectRetrievable != null && cleaningManager.GetToolSelector().CurrentToolIndex == cleaningManager.GetToolSelector().CleaningToolsLength - 1 && playerController.GetObjectGrabbable() == null)
            {
                AppendRetrievableSprites(objectRetrievable, ref activeSprites);
            }

            if (openableObject != null && playerController.GetObjectGrabbable() == null)
            {
                AppendOpenableSprites(openableObject, ref activeSprites);
            }

            if (cleanableToolObject != null && cleaningManager.GetToolSelector().CurrentToolIndex != cleaningManager.GetToolSelector().CleaningToolsLength - 1 && cleaningManager.GetToolSelector().GetDirtyPercentage(cleaningManager.GetToolSelector().CurrentToolIndex) > 0 && hit.collider.GetComponent<WaterBucket>().GetWaterState())
            {
                AppendCleanableSprites(cleanableToolObject, ref activeSprites);
            }

            if (toggableObject != null)
            {
                AppendToggableSprites(toggableObject, ref activeSprites);
            }

            if (toggableObject2 != null)
            {
                AppendToggableSprites(toggableObject2, ref activeSprites);
            }

            if (inmersiveObject != null && cleaningManager.GetToolSelector().CurrentToolIndex == cleaningManager.GetToolSelector().CleaningToolsLength - 1)
            {
                AppendInteractableSprites(inmersiveObject, ref activeSprites);
            }

            if (objectRetrievable2 != null && cleaningManager.GetToolSelector().CurrentToolIndex == cleaningManager.GetToolSelector().CleaningToolsLength - 1 && playerController.GetObjectGrabbable() == null)
            {
                AppendRetrievableSprites(objectRetrievable2, ref activeSprites);
            }

            UpdateUI(activeSprites);
        }
        else if (currentPickableObject != null && currentPickableObject.IsObjectPickedUp)
        {
            var activeSprites = new Sprite[interactionImages.Length];
            AppendPickUpSprites(currentPickableObject, ref activeSprites);
            UpdateUI(activeSprites);
        }
        else
        {
            SetImageState(false);
        }
    }

    private void SetImageState(bool state)
    {
        foreach (var image in interactionImages)
        {
            image.enabled = state;
        }
    }

    private void AppendPickUpSprites(IPickable pickableObject, ref Sprite[] activeSprites)
    {
        SetImageState(true);
        int index = GetNextAvailableSlot(activeSprites);
        if (pickableObject.IsObjectPickedUp)
        {
            activeSprites[1] = pickableObject.ThrowMessage;
            activeSprites[2] = pickableObject.RotateMessage;
            activeSprites[3] = pickableObject.DropMessage;
        }
        else
        {
            activeSprites[index] = pickableObject.PickUpMessage;
        }
    }

    private void AppendRetrievableSprites(IRetrievable retrievableObject, ref Sprite[] activeSprites)
    {
        SetImageState(true);
        int index = GetNextAvailableSlot(activeSprites);
        activeSprites[index] = retrievableObject.InteractMessage;
    }

    private void AppendOpenableSprites(IOpenable openableObject, ref Sprite[] activeSprites)
    {
        SetImageState(true);
        int index = GetNextAvailableSlot(activeSprites);
        activeSprites[index] = openableObject.InteractMessage;
    }

    private void AppendCleanableSprites(ICleanable cleanableObject, ref Sprite[] activeSprites)
    {
        SetImageState(true);
        int index = GetNextAvailableSlot(activeSprites);
        activeSprites[index] = cleanableObject.InteractMessage;
    }

    private void AppendToggableSprites(IToggable toggableObject, ref Sprite[] activeSprites)
    {
        SetImageState(true);
        int index = GetNextAvailableSlot(activeSprites);
        activeSprites[index] = toggableObject.InteractMessage;
    }

    private void AppendInteractableSprites(IInteractable inmersiveObject, ref Sprite[] activeSprites)
    {
        SetImageState(true);
        int index = GetNextAvailableSlot(activeSprites);
        activeSprites[index] = inmersiveObject.InteractMessage;
    }

    private int GetNextAvailableSlot(Sprite[] activeSprites)
    {
        for (int i = 0; i < activeSprites.Length; i++)
        {
            if (activeSprites[i] == null)
            {
                return i;
            }
        }
        return activeSprites.Length - 1;
    }

    private void UpdateUI(Sprite[] activeSprites)
    {
        for (int i = 0; i < interactionImages.Length; i++)
        {
            interactionImages[i].sprite = activeSprites[i];
            interactionImages[i].enabled = activeSprites[i] != null;
        }
    }
}