using UnityEngine;

public class StairsChecker : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private Rigidbody playerRb;
    [SerializeField] private Transform stepRayUpper;
    [SerializeField] private Transform stepRayLower;
    [SerializeField] private float stepHeight = 0.3f;
    [SerializeField] private float stepSmooth = 0.05f; // Reducido para suavizar la subida
    [SerializeField] private float stepDetectionDistance = 0.2f; // Ajusta la distancia para mejor detecci�n

    void Start()
    {
        // Ajusta la posici�n inicial del ray upper seg�n el stepHeight
        stepRayUpper.localPosition = new Vector3(stepRayUpper.localPosition.x, stepHeight, stepRayUpper.localPosition.z);
    }

    void Update()
    {
        StepClimb();
    }

    private void StepClimb()
    {
        Vector3 rayDirection = transform.TransformDirection(Vector3.forward);

        RaycastHit hitLower;
        if (Physics.Raycast(stepRayLower.position, rayDirection, out hitLower, stepDetectionDistance))
        {
            RaycastHit hitUpper;
            if (!Physics.Raycast(stepRayUpper.position, rayDirection, out hitUpper, stepDetectionDistance))
            {
                playerRb.position += new Vector3(0f, stepSmooth, 0f); // Suaviza el movimiento
            }
        }

        // Repite el proceso para los rayos en 45 grados (derecha e izquierda)
        CheckStepClimbAtAngle(new Vector3(1.5f, 0f, 1f));
        CheckStepClimbAtAngle(new Vector3(-1.5f, 0f, 1f));
    }

    private void CheckStepClimbAtAngle(Vector3 direction)
    {
        Vector3 rayDirection = transform.TransformDirection(direction);

        RaycastHit hitLower;
        if (Physics.Raycast(stepRayLower.position, rayDirection, out hitLower, stepDetectionDistance))
        {
            RaycastHit hitUpper;
            if (!Physics.Raycast(stepRayUpper.position, rayDirection, out hitUpper, stepDetectionDistance))
            {
                playerRb.position += new Vector3(0f, stepSmooth, 0f); // Suaviza el movimiento
            }
        }
    }
}