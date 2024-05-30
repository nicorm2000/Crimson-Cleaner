using TMPro;
using UnityEngine;

public class CameraInteraction : MonoBehaviour
{
    [SerializeField] private float interactionDistance = 5f;
    [SerializeField] private LayerMask interactableLayers;
    [SerializeField] private TextMeshProUGUI[] interactionTexts;
    [SerializeField] private CleaningManager cleaningManager;

    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
        SetTextState(false);
    }

    private void Update()
    {
        DetectInteractableObject();
    }

    private void DetectInteractableObject()
    {
        Ray ray = new(mainCamera.transform.position, mainCamera.transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, interactionDistance, interactableLayers))
        {
            var activeTexts = new string[interactionTexts.Length];

            IPick pickableObject = hit.collider.gameObject.GetComponent<ObjectGrabbable>() as IPick;
            IOpenable openableObject = hit.collider.gameObject.GetComponent<Openable>() as IOpenable;
            IOpenable cartOpenableObject = hit.collider.gameObject.GetComponent<Cart>() as IOpenable;
            ICleanable cleanableObject = hit.collider.gameObject.GetComponent<Clean>() as ICleanable;
            ICleanable cleanableToolObject = hit.collider.gameObject.GetComponent<WaterBucket>() as ICleanable;
            IToggable toggableObject = hit.collider.gameObject.GetComponent<UVLight>() as IToggable;

            if (pickableObject != null && cleaningManager.GetToolSelector().CurrentToolIndex == cleaningManager.GetToolSelector().ToolsLength -1)
            {
                AppendPickUpTexts(pickableObject, ref activeTexts);
            }

            if (openableObject != null)
            {
                AppendOpenableTexts(openableObject, ref activeTexts);
            }

            if (cartOpenableObject != null)
            {
                AppendOpenableTexts(cartOpenableObject, ref activeTexts);
            }

            if (cleanableObject != null)
            {
                AppendCleanableTexts(cleanableObject, ref activeTexts);
            }

            if (cleanableToolObject != null && cleaningManager.GetToolSelector().CurrentToolIndex != cleaningManager.GetToolSelector().ToolsLength - 1)
            {
                AppendCleanableTexts(cleanableToolObject, ref activeTexts);
            }

            if (toggableObject != null) 
            {
                AppendToggableTexts(toggableObject, ref activeTexts);
            }

            UpdateUI(activeTexts);
        }
        else
        {
            SetTextState(false);
        }
    }

    private void SetTextState(bool state)
    {
        foreach (var text in interactionTexts)
        {
            text.enabled = state;
        }
    }

    private void AppendPickUpTexts(IPick pickableObject, ref string[] activeTexts)
    {
        SetTextState(true);
        if (pickableObject.isObjectPickedUp)
        {
            activeTexts[0] = pickableObject.DropMessage;
            activeTexts[1] = pickableObject.ThrowMessage;
            activeTexts[2] = pickableObject.RotateMessage;
        }
        else
        {
            activeTexts[0] = pickableObject.PickUpMessage;
        }
    }

    private void AppendOpenableTexts(IOpenable openableObject, ref string[] activeTexts)
    {
        SetTextState(true);
        activeTexts[0] = openableObject.OpenCloseMessage;
    }

    private void AppendCleanableTexts(ICleanable cleanableObject, ref string[] activeTexts)
    {
        SetTextState(true);
        activeTexts[1] = cleanableObject.CleanMessage;
    }

    private void AppendToggableTexts(IToggable toggableObject, ref string[] activeTexts)
    {
        SetTextState(true);
        activeTexts[3] = toggableObject.ToggleOnOffMessage;
    }

    private void UpdateUI(string[] activeTexts)
    {
        for (int i = 0; i < interactionTexts.Length; i++)
        {
            interactionTexts[i].text = activeTexts[i] ?? string.Empty;
        }
    }
}
