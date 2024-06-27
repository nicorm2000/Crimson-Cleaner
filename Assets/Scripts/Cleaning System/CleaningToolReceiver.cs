using UnityEngine;

public class CleaningToolReceiver : MonoBehaviour
{
    [Header("Audio Config")]
    [SerializeField] private CleaningManager cleaningManager = null;
    [SerializeField] private AudioManager audioManager = null;

    public void MoveToolEvent()
    {
        if (cleaningManager.GetToolSelector().CurrentToolIndex == 0)
        {
            audioManager.PlaySound(cleaningManager.GetMopWooshEvent());
        }
        else if (cleaningManager.GetToolSelector().CurrentToolIndex == 1)
        {
            audioManager.PlaySound(cleaningManager.GetSpongeWooshEvent());
        }
    }
    public void CleaningEvent()
    {
        Debug.Log("Clean Object");
    }
}