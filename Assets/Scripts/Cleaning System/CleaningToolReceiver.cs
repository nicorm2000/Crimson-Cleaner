using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class CleaningToolReceiver : MonoBehaviour
{
    [Header("Audio Config")]
    [SerializeField] private CleaningManager cleaningManager = null;

    private Clean currentCleanableObject;

    public event Action ToolDirty;

    public void SetCurrentCleanableObject(Clean cleanableObject)
    {
        currentCleanableObject = cleanableObject;
    }

    public void MoveToolEvent()
    {
        int currentToolIndex = cleaningManager.GetToolSelector().CurrentToolIndex;
        if (currentToolIndex == 1)
        {
            cleaningManager.GetAudioManager().PlaySound(cleaningManager.GetMopWooshEvent());
        }
        else if (currentToolIndex == 2)
        {
            cleaningManager.GetAudioManager().PlaySound(cleaningManager.GetSpongeWooshEvent());
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
                    if (currentToolIndex == cleaningManager.GetMop() || currentToolIndex == cleaningManager.GetSponge())
                    {
                        currentCleanableObject = hit.transform.gameObject.GetComponent<Clean>();
                        cleaningManager.GetToolReceiver().SetCurrentCleanableObject(currentCleanableObject);
                    }
                    currentCleanableObject.CleanSurface();
                }
            }
            else
            {
                if (cleaningManager.GetToolSelector().CurrentToolIndex == cleaningManager.GetMop())
                {
                    cleaningManager.GetAudioManager().PlaySound(cleaningManager.GetMopDirtyEvent());
                    cleaningManager.GetMopCleaningDirtyParticles().Play();
                }
                else if (cleaningManager.GetToolSelector().CurrentToolIndex == cleaningManager.GetSponge())
                {
                    cleaningManager.GetAudioManager().PlaySound(cleaningManager.GetSpongeDirtyEvent());
                    cleaningManager.GetSpongeCleaningDirtyParticles().Play();
                }
                ToolDirty?.Invoke();
            }
        }
        else
        {
            Debug.LogWarning("No cleanable object targeted.");
        }
    }
}