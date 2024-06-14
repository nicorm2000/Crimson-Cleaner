using UnityEngine;

public class WaterBehavior : MonoBehaviour
{
    public Transform bucket;  // Reference to the bucket
    public float spillThresholdAngle = 45.0f;  // Angle threshold for spilling
    public GameObject spillEffectPrefab;  // Reference to the particle effect prefab
    public float continuousSpillTime = 5.0f;  // Time in seconds for continuous spill to destroy water

    private Vector3 initialLocalPosition;
    private float initialLocalRotationY;  // Initial local Y rotation
    private bool hasPlayedSpillEffect = false;  // To ensure the effect plays only once
    private float spillTimer = 0.0f;  // Timer for continuous spilling

    void Start()
    {
        initialLocalPosition = transform.localPosition;
        initialLocalRotationY = transform.localEulerAngles.y;
    }

    void Update()
    {
        // Keep the water plane level relative to gravity while maintaining initial Y rotation
        transform.rotation = Quaternion.Euler(-bucket.eulerAngles.x, initialLocalRotationY, -bucket.eulerAngles.z);

        // Check if the water is spilling
        CheckSpill();
    }

    void CheckSpill()
    {
        // Determine if the water plane is tilted beyond the spill threshold angle
        float spillAngle = Quaternion.Angle(transform.rotation, Quaternion.Euler(-bucket.eulerAngles.x, initialLocalRotationY, -bucket.eulerAngles.z));

        // Immediate destruction if spilling significantly (e.g., rotated beyond threshold)
        if (spillAngle > spillThresholdAngle)
        {
            Destroy(gameObject);
            return;
        }

        // If the spill amount exceeds the threshold, handle spill effects and timer
        if (spillAngle > spillThresholdAngle * 0.1f)
        {
            if (!hasPlayedSpillEffect)
            {
                PlaySpillEffect();
                hasPlayedSpillEffect = true;
            }

            // Increment spill timer
            spillTimer += Time.deltaTime;

            // If continuous spill exceeds the allowed time, destroy the water plane
            if (spillTimer >= continuousSpillTime)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            // Reset spill timer if the spill angle drops below the threshold
            spillTimer = 0.0f;
        }
    }

    void PlaySpillEffect()
    {
        if (spillEffectPrefab != null)
        {
            Instantiate(spillEffectPrefab, transform.position, Quaternion.identity);
        }
    }
}
