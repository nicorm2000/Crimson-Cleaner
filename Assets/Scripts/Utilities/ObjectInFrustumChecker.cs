using UnityEngine;

public class ObjectInFrustumChecker : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private Camera playerCamera;
    [SerializeField] private GameObject targetObject;

    private Renderer[] childRenderers;

    void Start()
    {
        if (!playerCamera)
        {
            Debug.LogError("Player Camera is not assigned.");
            enabled = false;
            return;
        }

        if (!targetObject)
        {
            Debug.LogError("Target Object is not assigned.");
            enabled = false;
            return;
        }

        childRenderers = targetObject.GetComponentsInChildren<Renderer>();

        if (childRenderers.Length == 0)
        {
            Debug.LogError("No renderers found on the target object or its children.");
            enabled = false;
        }
    }

    void Update()
    {
        if (IsObjectInFrustum())
        {
            Debug.Log($"{targetObject.name} or its children are within the camera's frustum.");
        }
    }

    private bool IsObjectInFrustum()
    {
        if (childRenderers == null || childRenderers.Length == 0)
            return false;

        Plane[] frustumPlanes = GeometryUtility.CalculateFrustumPlanes(playerCamera);

        foreach (var renderer in childRenderers)
        {
            if (GeometryUtility.TestPlanesAABB(frustumPlanes, renderer.bounds))
            {
                return true;
            }
        }

        return false;
    }
}