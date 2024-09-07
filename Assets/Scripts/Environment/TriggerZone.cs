using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TriggerZone : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private InputManager inputManager;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private LayerMask triggerMask;
    [SerializeField] private int movementRestrictionDuration;
    [SerializeField] private int cameraRestrictionDuration;
    [SerializeField] private int executionTimes;

    [Header("Events")]
    [SerializeField] private bool isMovementAvailable;
    [SerializeField] private bool isCameraAvailable;
    [SerializeField] private int eventID;

    private int executionCounter = 0;
    private Collider collider;

    private void Awake()
    {
        collider = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (((1 << other.gameObject.layer) & triggerMask) != 0)
        {
            Debug.Log("Player entered");

            if (isMovementAvailable)
                StartCoroutine(ToggleMovement(movementRestrictionDuration));
            
            if (isCameraAvailable)
                StartCoroutine(ToggleCamera(cameraRestrictionDuration));

            //if (sanityAmmount > sanityThreshold)
            // ExecuteEvent(eventID);
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

    private IEnumerator ToggleMovement(float duration)
    {
        playerController.isMovable = false;

        yield return new WaitForSeconds(duration);

        playerController.isMovable = true;
    }

    private IEnumerator ToggleCamera(float duration)
    {
        playerController.isCameraMovable = false;

        yield return new WaitForSeconds(duration);

        playerController.isCameraMovable = true;
    }

    private void ExecuteEvent(int eventID)
    {
        // Get Event 
    }


}
