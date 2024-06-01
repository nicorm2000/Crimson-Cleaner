using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class WaterBucket : MonoBehaviour, ICleanable
{
    [SerializeField] private InputManager inputManager;
    [SerializeField] private CleaningManager cleaningManager;
    [SerializeField] private ParticleSystem washParticles;

    public string CleanMessage => "Press F click to clean tool";
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
            ActivateWashing();
            cleaningManager.GetToolSelector().ResetDirtyPercentage(currentToolIndex);
            cleaningManager.GetToolSelector().ChangeToolMaterial(currentToolIndex, cleaningManager.GetToolSelector().GetOriginalMaterial());
        }
    }

    public void ActivateWashing() => washParticles.Play();
}
