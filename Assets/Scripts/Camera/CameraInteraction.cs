using UnityEngine;
using UnityEngine.UI;

public class CameraInteraction : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private Image[] interactionImages;
    [SerializeField] private CleaningManager cleaningManager;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private string defaultLayerName;
    [SerializeField] private string outlineLayerName;

    private Camera mainCamera;
    private IPickable currentPickableObject;

    //private ObjectGrabbable lastObjectGrabbableHighlighted = null;
    //private StealableObject lastObjectStealableHighlighted = null;
    //private Openable lastOpenableObjectHighlighted = null;
    //private WaterFaucetSystem lastWaterFaucetSystemObjectHighlighted = null;
    //private InmersiveObject lastInmersiveObjectHighlighted = null;
    private RetrievableObject lastRetrievableObjectHighlighted = null;

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
            OpenableNoAnimator openableNoAnimatorObject = hit.collider.gameObject.GetComponent<OpenableNoAnimator>();
            //ICleanable cleanableObject = hit.collider.gameObject.GetComponent<Clean>() as ICleanable;
            WaterBucket cleanableToolObject = hit.collider.gameObject.GetComponent<WaterBucket>();
            UVLight toggableObject = hit.collider.gameObject.GetComponent<UVLight>();
            WaterFaucetSystem toggableObject2 = hit.collider.gameObject.GetComponent<WaterFaucetSystem>();
            InmersiveObject inmersiveObject = hit.collider.gameObject.GetComponent<InmersiveObject>();

            RetrievableObject objectRetrievable2 = hit.collider.gameObject.GetComponent<RetrievableObject>();

            ResetHighlightedObjects();

            if (pickableObject != null && !pickableObject.IsObjectSnapped)
            {
                //if (pickableObject.gameObject.layer != LayerMask.NameToLayer(outlineLayerName))
                //    pickableObject.gameObject.layer = LayerMask.NameToLayer(outlineLayerName);

                currentPickableObject = pickableObject;
                //lastObjectGrabbableHighlighted = pickableObject;

                if (cleaningManager.GetToolSelector().CurrentToolIndex == cleaningManager.GetHands())
                {
                    if (SanityManager.Instance.isRageActive)
                    {
                        if (pickableObject.gameObject.GetComponent<SnappableObject>() == null)
                            AppendPickUpSprites(pickableObject, ref activeSprites);
                    }
                    else
                        AppendPickUpSprites(pickableObject, ref activeSprites);
                }
            }

            if (objectRetrievable != null && cleaningManager.GetToolSelector().CurrentToolIndex == cleaningManager.GetHands() && playerController.GetObjectGrabbable() == null && !SanityManager.Instance.isRageActive)
            {
                AppendRetrievableSprites(objectRetrievable, ref activeSprites);
                //lastObjectStealableHighlighted = objectRetrievable;
            }

            if (openableObject != null && playerController.GetObjectGrabbable() == null)
            {
                //if (openableObject.transform.GetChild(0) != null)
                //{
                //    Transform childTransform = openableObject.transform.GetChild(0);

                //    if (childTransform.gameObject.layer != LayerMask.NameToLayer(outlineLayerName))
                //        childTransform.gameObject.layer = LayerMask.NameToLayer(outlineLayerName);
                //}

                AppendOpenableSprites(openableObject, ref activeSprites);
                //lastOpenableObjectHighlighted = openableObject;
            }

            if (openableNoAnimatorObject != null && playerController.GetObjectGrabbable() == null)
            {
                //if (openableObject.transform.GetChild(0) != null)
                //{
                //    Transform childTransform = openableObject.transform.GetChild(0);

                //    if (childTransform.gameObject.layer != LayerMask.NameToLayer(outlineLayerName))
                //        childTransform.gameObject.layer = LayerMask.NameToLayer(outlineLayerName);
                //}

                AppendOpenableSprites(openableNoAnimatorObject, ref activeSprites);
                //lastOpenableObjectHighlighted = openableObject;
            }

            if (cleanableToolObject != null && cleaningManager.GetToolSelector().CurrentToolIndex != cleaningManager.GetHands() && cleaningManager.GetToolSelector().GetDirtyPercentage(cleaningManager.GetToolSelector().CurrentToolIndex) > 0 && hit.collider.GetComponent<WaterBucket>().GetWaterState())
            {
                AppendCleanableSprites(cleanableToolObject, ref activeSprites);
            }

            if (toggableObject != null)
            {
                AppendToggableSprites(toggableObject, ref activeSprites);
            }

            if (toggableObject2 != null)
            {
                //if (toggableObject2.gameObject.layer != LayerMask.NameToLayer(outlineLayerName))
                //    toggableObject2.gameObject.layer = LayerMask.NameToLayer(outlineLayerName);

                AppendToggableSprites(toggableObject2, ref activeSprites);
                //lastWaterFaucetSystemObjectHighlighted = toggableObject2;
            }

            if (inmersiveObject != null && cleaningManager.GetToolSelector().CurrentToolIndex == cleaningManager.GetHands())
            {
                //if (inmersiveObject.gameObject.layer != LayerMask.NameToLayer(outlineLayerName))
                //    inmersiveObject.gameObject.layer = LayerMask.NameToLayer(outlineLayerName);

                AppendInteractableSprites(inmersiveObject, ref activeSprites);
                //lastInmersiveObjectHighlighted = inmersiveObject;
            }

            if (objectRetrievable2 != null && cleaningManager.GetToolSelector().CurrentToolIndex == cleaningManager.GetHands() && playerController.GetObjectGrabbable() == null && !SanityManager.Instance.isRageActive)
            {
                if (objectRetrievable2.gameObject.layer != LayerMask.NameToLayer(outlineLayerName))
                    objectRetrievable2.gameObject.layer = LayerMask.NameToLayer(outlineLayerName);

                AppendRetrievableSprites(objectRetrievable2, ref activeSprites);
                lastRetrievableObjectHighlighted = objectRetrievable2;
            }

            //if (pickableObject == null && lastObjectGrabbableHighlighted != null)
            //    lastObjectGrabbableHighlighted.gameObject.layer = LayerMask.NameToLayer(defaultLayerName); 
            //if (objectRetrievable == null && lastObjectStealableHighlighted != null)
            //    lastObjectStealableHighlighted.gameObject.layer = LayerMask.NameToLayer(defaultLayerName);
            //if (openableObject == null && lastOpenableObjectHighlighted != null)
            //{
            //    if (lastOpenableObjectHighlighted.transform.GetChild(0) != null)
            //    {
            //        Transform childTransform = lastOpenableObjectHighlighted.transform.GetChild(0);

            //        if (childTransform.gameObject.layer != LayerMask.NameToLayer(defaultLayerName))
            //            childTransform.gameObject.layer = LayerMask.NameToLayer(defaultLayerName);
            //    }
            //}

            //if (toggableObject2 == null && lastWaterFaucetSystemObjectHighlighted != null)
            //    lastWaterFaucetSystemObjectHighlighted.gameObject.layer = LayerMask.NameToLayer(defaultLayerName);
            //if (inmersiveObject == null && lastInmersiveObjectHighlighted != null)
            //    lastInmersiveObjectHighlighted.gameObject.layer = LayerMask.NameToLayer(defaultLayerName);
            //if (objectRetrievable2 == null && lastRetrievableObjectHighlighted != null)
            //    lastRetrievableObjectHighlighted.gameObject.layer = LayerMask.NameToLayer(defaultLayerName);

            UpdateUI(activeSprites);
        }

        if (currentPickableObject != null && currentPickableObject.IsObjectPickedUp)
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
        activeSprites[2] = toggableObject.InteractMessage;
    }

    private void AppendInteractableSprites(IInteractable inmersiveObject, ref Sprite[] activeSprites)
    {
        SetImageState(true);
        int index = GetNextAvailableSlot(activeSprites);
        activeSprites[index] = inmersiveObject.InteractMessage;
    }

    private void ResetHighlightedObjects()
    {
        //if (lastObjectGrabbableHighlighted != null)
        //{
        //    lastObjectGrabbableHighlighted.gameObject.layer = LayerMask.NameToLayer(defaultLayerName);
        //    lastObjectGrabbableHighlighted = null;
        //}
        //if (lastObjectStealableHighlighted != null)
        //{
        //    lastObjectStealableHighlighted.gameObject.layer = LayerMask.NameToLayer(defaultLayerName);
        //    lastObjectStealableHighlighted = null;
        //}
        //if (lastWaterFaucetSystemObjectHighlighted != null)
        //{
        //    lastWaterFaucetSystemObjectHighlighted.gameObject.layer = LayerMask.NameToLayer(defaultLayerName);
        //    lastWaterFaucetSystemObjectHighlighted = null;
        //}
        //if (lastInmersiveObjectHighlighted != null)
        //{
        //    lastInmersiveObjectHighlighted.gameObject.layer = LayerMask.NameToLayer(defaultLayerName);
        //    lastInmersiveObjectHighlighted = null;
        //}
        if (lastRetrievableObjectHighlighted != null && !SanityManager.Instance.isConcentrationActive)
        {
            lastRetrievableObjectHighlighted.gameObject.layer = LayerMask.NameToLayer(defaultLayerName);
            lastRetrievableObjectHighlighted = null;
        }
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