using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class WaterBucket : MonoBehaviour, ICleanable
{
    [Header("Config")]
    [SerializeField] private InputManager inputManager;
    [SerializeField] private CleaningManager cleaningManager;
    [SerializeField] private ParticleSystem washParticles;
    [SerializeField] private Sprite cleanMessage;

    [Header("Audio Config")]
    [SerializeField] private AudioManager audioManager = null;
    [SerializeField] private string washToolEvent = null;

    public Sprite CleanMessage => cleanMessage;

    public event Action Cleaned;

    private void OnEnable()
    {
        inputManager.InteractEvent += CleanTool;
    }

    private void OnDisable()
    {
        inputManager.InteractEvent -= CleanTool;
    }

    private void CleanTool()
    {
        Vector3 mousePosition = Mouse.current.position.ReadValue();
        Ray ray = cleaningManager.GetCamera().ScreenPointToRay(mousePosition);

        int currentToolIndex = cleaningManager.GetToolSelector().CurrentToolIndex;
        int dirtyPercentage = cleaningManager.GetToolSelector().GetDirtyPercentage(currentToolIndex);

        if (dirtyPercentage == 0)
        {
            return;
        }

        if (Physics.Raycast(ray, out RaycastHit hit, cleaningManager.GetInteractionDistance()))
        {
            if (hit.transform != gameObject.transform)
            {
                return;
            }
            Debug.Log("Play");
            audioManager.PlaySound(washToolEvent);
            ActivateWashing();
            cleaningManager.GetToolSelector().ResetDirtyPercentage(currentToolIndex);
            cleaningManager.GetToolSelector().ChangeToolMaterial(currentToolIndex, cleaningManager.GetToolSelector().GetOriginalMaterial());
        }
    }

    public void ActivateWashing() => washParticles.Play();
}
