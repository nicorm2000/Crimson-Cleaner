using UnityEngine;
using UnityEngine.InputSystem;

public class WaterBucket : MonoBehaviour
{
    [SerializeField] private InputManager inputManager;
    [SerializeField] private CleaningManager cleaningManager;
    [SerializeField] private float raycastDistance;

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

        if (Physics.Raycast(ray, out RaycastHit hit, raycastDistance))
        {
            if (hit.transform != gameObject.transform)
            {
                return;
            }

            int currentToolIndex = cleaningManager.GetToolSelector().CurrentToolIndex;
            cleaningManager.GetToolSelector().ResetDirtyPercentage(currentToolIndex);
            cleaningManager.GetToolSelector().ChangeToolMaterial(currentToolIndex, cleaningManager.GetToolSelector().GetOriginalMaterial());
        }
    }
}
