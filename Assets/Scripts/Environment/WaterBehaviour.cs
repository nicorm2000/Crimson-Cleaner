using UnityEngine;

public class WaterBehaviour : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private WaterBucket waterBucket;
    [SerializeField] private GameObject water;
    [SerializeField] private float spillThreshold;
    [SerializeField] private ParticleSystem splashParticles;

    [Header("Audio Config")]
    [SerializeField] private AudioManager audioManager = null;
    [SerializeField] private string waterDropEvent = null;

    void Update()
    {
        if (waterBucket.GetWaterState())
            CheckBucketRotation();
    }

    private void CheckBucketRotation()
    {
        float rotationX = transform.eulerAngles.x;
        float rotationZ = transform.eulerAngles.z;

        rotationX = (rotationX > 180) ? rotationX - 360 : rotationX;
        rotationZ = (rotationZ > 180) ? rotationZ - 360 : rotationZ;

        if (Mathf.Abs(rotationX) > spillThreshold || Mathf.Abs(rotationZ) > spillThreshold)
        {
            DestroyWater();
        }
    }

    private void DestroyWater()
    {
        if (water != null)
        {
            splashParticles.transform.rotation = transform.rotation;
            splashParticles.Play();
            audioManager.PlaySound(waterDropEvent, gameObject);
            water.SetActive(false);
            waterBucket.SetWaterPercentage(0.0f);
            waterBucket.SetWaterState(false);
        }
    }
}