using System;
using UnityEngine;

public class BloodPoolDetection : MonoBehaviour
{
    [SerializeField] private LayerMask raycastLayer;
    [SerializeField] private float raycastDistance = 0.3f;

    public event Action poolDetected;

    private void Update()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, -transform.up, out hit, raycastDistance, raycastLayer))
        {
            poolDetected?.Invoke();
        }
    }

}
