using UnityEngine;

public class UVLight : MonoBehaviour
{
    public bool isOn = false;
    private Light uvLight;

    void Start()
    {
        uvLight = GetComponentInChildren<Light>();
        uvLight.enabled = isOn;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            ToggleLight();
        }
    }

    public void ToggleLight()
    {
        isOn = !isOn;
        uvLight.enabled = isOn;
    }

    public bool IsObjectInLightRadius(GameObject obj, float minCoverage = 0.7f, int samplePoints = 5)
    {
        if (!isOn) return false;

        Renderer renderer = obj.GetComponent<Renderer>();
        if (renderer == null) return false;

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