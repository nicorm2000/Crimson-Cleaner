using UnityEngine;
using UnityEngine.Events;

public class TriggerZone : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private LayerMask triggerMask;
    [SerializeField] private int executionTimes;

    private int executionCounter = 0;
    private Collider collider;

    public UnityEvent Trigger; 

    private void Awake()
    {
        collider = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (((1 << other.gameObject.layer) & triggerMask) != 0)
        {
            Debug.Log("Player entered");

            Trigger?.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (((1 << other.gameObject.layer) & triggerMask) != 0)
        {
            executionCounter++;

            if (executionCounter >= executionTimes)
                collider.enabled = false;
        }
    }
}
