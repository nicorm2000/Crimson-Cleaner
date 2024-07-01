using UnityEngine;
using UnityEngine.InputSystem;

public class CleaningToolReceiver : MonoBehaviour
{
    [Header("Audio Config")]
    [SerializeField] private CleaningManager cleaningManager = null;
    [SerializeField] private AudioManager audioManager = null;

    private Clean currentCleanableObject;

    public void SetCurrentCleanableObject(Clean cleanableObject)
    {
        currentCleanableObject = cleanableObject;
    }

    public void MoveToolEvent()
    {
        int currentToolIndex = cleaningManager.GetToolSelector().CurrentToolIndex;
        if (currentToolIndex == 0)
        {
            audioManager.PlaySound(cleaningManager.GetMopWooshEvent());
        }
        else if (currentToolIndex == 1)
        {
            audioManager.PlaySound(cleaningManager.GetSpongeWooshEvent());
        }
    }

    public void CleaningEvent()
    {
        Vector3 mousePosition = Mouse.current.position.ReadValue();
        Ray ray = cleaningManager.GetCamera().ScreenPointToRay(mousePosition);
        int currentToolIndex = cleaningManager.GetToolSelector().CurrentToolIndex;

        if (Physics.Raycast(ray, out RaycastHit hit, cleaningManager.GetInteractionDistance()))
        {
            if (cleaningManager.GetToolSelector().GetDirtyPercentage(currentToolIndex) < cleaningManager.DirtyMaxValue)
            {
                if (hit.transform.gameObject.GetComponent<Clean>() != null)
                {
                    if (currentToolIndex == 0)
                    {
                        currentCleanableObject = hit.transform.gameObject.GetComponent<Clean>();
                        cleaningManager.GetMopToolReceiver().SetCurrentCleanableObject(currentCleanableObject);
                    }
                    else if (currentToolIndex == 1)
                    {
                        currentCleanableObject = hit.transform.gameObject.GetComponent<Clean>();
                        cleaningManager.GetSpongeToolReceiver().SetCurrentCleanableObject(currentCleanableObject);
                    }
                    currentCleanableObject.CleanSurface();
                }
            }
        }
        else
        {
            Debug.LogWarning("No cleanable object targeted.");
        }
    }
}