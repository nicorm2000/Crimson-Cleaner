using UnityEngine;

public class LightFlickerManager : MonoBehaviour
{
    [SerializeField] private Light[] lights;
    [SerializeField] private float minIntensity;
    [SerializeField] private float maxIntensity;
    [SerializeField] private float flickerSpeed;

    private float[] targetIntensities;
    private float[] currentIntensities;
    private float[] nextFlickerTimes;

    private void Start()
    {
        targetIntensities = new float[lights.Length];
        currentIntensities = new float[lights.Length];
        nextFlickerTimes = new float[lights.Length];

        for (int i = 0; i < lights.Length; i++)
        {
            targetIntensities[i] = Random.Range(minIntensity, maxIntensity);
            nextFlickerTimes[i] = Time.time + Random.Range(0.1f, 0.5f);
        }
    }

    private void Update()
    {
        for (int i = 0; i < lights.Length; i++)
        {
            if (Time.time >= nextFlickerTimes[i])
            {
                nextFlickerTimes[i] = Time.time + flickerSpeed;

                targetIntensities[i] = Random.Range(minIntensity, maxIntensity);
            }

            currentIntensities[i] = Mathf.Lerp(currentIntensities[i], targetIntensities[i], Time.deltaTime * flickerSpeed);

            lights[i].intensity = currentIntensities[i];
        }
    }
}