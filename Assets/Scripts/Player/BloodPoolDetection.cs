using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodPoolDetection : MonoBehaviour
{
    [SerializeField] private LayerMask raycastLayer;
    [SerializeField] private float raycastDistance = 0.3f;

    public bool isPlayerOnBlood = false;

    public event Action poolDetected;

    private void Update()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, -transform.up, out hit, raycastDistance, raycastLayer))
        {
            isPlayerOnBlood = true;
            Debug.Log("Blood Pool Detected");
            poolDetected?.Invoke();
        }
        else
        {
            isPlayerOnBlood = false;
        }
    }

}
