using UnityEngine;
using UnityEngine.InputSystem;

public class UVLight : MonoBehaviour, IToggable
{
    [Header("Config")]
    [SerializeField] private InputManager inputManager;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private CleaningManager cleaningManager;
    [SerializeField] private float minCoverage = 0.7f;
    private bool isOn = false;
    private Light uvLight;

    public bool _isOn;

    [SerializeField] private Sprite toggleOnOffMessage;
    public Sprite ToggleOnOffMessage => toggleOnOffMessage;

    private void OnEnable()
    {
        inputManager.InteractEvent += ToggleLight;
    }

    private void OnDisable()
    {
        inputManager.InteractEvent -= ToggleLight;
    }

    void Start()
    {
        uvLight = GetComponentInChildren<Light>();
        uvLight.enabled = isOn;
    }

    public void ToggleLight()
    {
        Vector3 mousePosition = Mouse.current.position.ReadValue();
        Ray ray = cleaningManager.GetCamera().ScreenPointToRay(mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, cleaningManager.GetInteractionDistance()))
        {
            if (hit.distance <= cleaningManager.GetInteractionDistance())
            {
                if (hit.transform != gameObject.transform)
                {
                    return;
                }
                isOn = !isOn;
                uvLight.enabled = isOn;
            }
        }
    }

    public bool IsObjectInLightRadius(GameObject obj, int samplePoints = 5)
    {
        if (!isOn) return false;

        if (!obj.TryGetComponent<Renderer>(out var renderer)) return false;

        Bounds bounds = renderer.bounds;
        Vector3[] samplePositions = new Vector3[samplePoints * samplePoints];

        int index = 0;
        for (int x = 0; x < samplePoints; x++)
        {
            for (int y = 0; y < samplePoints; y++)
            {
                float xPos = bounds.min.x + (bounds.size.x / (samplePoints - 1)) * x;
                float yPos = bounds.min.y + (bounds.size.y / (samplePoints - 1)) * y;
                samplePositions[index++] = new Vector3(xPos, yPos, bounds.center.z);
            }
        }

        int pointsInside = 0;
        foreach (var pos in samplePositions)
        {
            if (Vector3.Distance(transform.position, pos) <= uvLight.range)
            {
                pointsInside++;
            }
        }

        float coverage = (float)pointsInside / samplePositions.Length;
        return coverage >= minCoverage;
    }
}