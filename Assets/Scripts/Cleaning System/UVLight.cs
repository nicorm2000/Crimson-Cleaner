using UnityEngine;
using UnityEngine.InputSystem;

public class UVLight : Interactable, IToggable
{
    [Header("UV Light Config")]
    [SerializeField] private CleaningManager cleaningManager;
    [SerializeField] private float minCoverage = 0.7f;

    [SerializeField] private Sprite toggleOnOffMessage;
    [SerializeField] private Material onMaterial;
    [SerializeField] private Material offMaterial;
    [SerializeField] private Light[] uvLight;

    private bool isOn = false;
    private Renderer _renderer;

    public Sprite InteractMessage => toggleOnOffMessage;

    protected override void OnEnable()
    {
        base.OnEnable();
    }

    protected override void OnDisable()
    {
        base.OnDisable();
    }

    public void Interact(PlayerController playerController)
    {
        ToggleLight();
    }

    protected override void PerformInteraction(PlayerController playerController)
    {
        Interact(playerController);
    }

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
    }

    void Start()
    {
        for (int i = 0; i < uvLight.Length; i++)
        {
            uvLight[i].enabled = isOn;
        }
    }

    public void ToggleLight()
    {
        isOn = !isOn;
        SwapMaterial(isOn);
        audioManager.PlaySound(soundEvent);
        for (int i = 0; i < uvLight.Length; i++)
        {
            uvLight[i].enabled = isOn;
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
            if (Vector3.Distance(transform.position, pos) <= uvLight[0].range)
            {
                pointsInside++;
            }
        }

        float coverage = (float)pointsInside / samplePositions.Length;
        return coverage >= minCoverage;
    }

    private void SwapMaterial(bool state)
    {
        if (_renderer != null)
        {
            if (state)
            {
                _renderer.material = onMaterial;
            }
            else
            {
                _renderer.material = offMaterial;
            }
        }
    }
}
