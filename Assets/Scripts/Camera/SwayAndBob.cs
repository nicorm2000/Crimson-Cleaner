using System;
using System.Collections;
using UnityEngine;

public class SwayAndBob : MonoBehaviour
{
    [SerializeField] PlayerController playerController;

    [Header("Sway Settings")]
    [SerializeField] private bool sway = true;
    [SerializeField] private float step = 0.01f;
    [SerializeField] private float maxStepDistance = 0.06f;
    private Vector3 swayPos;

    [Header("Sway Rotation")]
    [SerializeField] private bool swayRotation = true;
    [SerializeField] private float rotationStep = 4f;
    [SerializeField] private float maxRotationStep = 5f;
    private Vector3 swayEulerRot;

    [Header("Bob Settings")]
    [SerializeField] private bool bobOffset = true;
    [SerializeField] private Vector3 travelLimit = Vector3.one * 0.025f;
    [SerializeField] private Vector3 bobLimit = Vector3.one * 0.01f;
    private Vector3 bobPos;

    [Header("Bob Rotation")]
    [SerializeField] private bool bobSway = true;
    [SerializeField] private Vector3 multiplier;
    private Vector3 bobEulerRotation;

    [SerializeField] private float smooth = 10f;
    [SerializeField] private float smoothRot = 12f;
    [SerializeField] private float bobReturnSmooth = 8f;
    [SerializeField] private float bobRotReturnSmooth = 8f;

    private float speedCurve;
    private float curveSin => Mathf.Sin(speedCurve);
    private float curveCos => Mathf.Cos(speedCurve);

    private Vector3 targetBobPos = Vector3.zero;
    private Vector3 targetBobEulerRotation = Vector3.zero;

    private Coroutine returnToIdleCoroutine;

    private void Update()
    {
        Sway();
        Boboffset();
        SwayRotation();
        BobRotation();
        CompositePositionRotation();
    }

    private void Sway()
    {
        if (!sway) { swayPos = Vector3.zero; return; }

        Vector3 invertLook = playerController.GetMousePos() * -step;
        invertLook.x = Mathf.Clamp(invertLook.x, -maxStepDistance, maxStepDistance);
        invertLook.y = Mathf.Clamp(invertLook.y, -maxStepDistance, maxStepDistance);

        swayPos = invertLook;
    }

    private void SwayRotation()
    {
        if (!swayRotation) { swayEulerRot = Vector3.zero; return; }

        Vector2 invertLook = playerController.GetMousePos() * -rotationStep;
        invertLook.x = Mathf.Clamp(invertLook.x, -maxRotationStep, maxRotationStep);
        invertLook.y = Mathf.Clamp(invertLook.y, -maxRotationStep, maxRotationStep);

        swayEulerRot = new Vector3(invertLook.y, invertLook.x, invertLook.x);
    }

    private void Boboffset()
    {
        if (!bobOffset) { bobPos = Vector3.zero; return; }

        if (playerController.GetCurrentVelocity() != Vector2.zero)
        {
            if (returnToIdleCoroutine != null)
            {
                StopCoroutine(returnToIdleCoroutine);
                returnToIdleCoroutine = null;
            }

            speedCurve += Time.deltaTime * (playerController.GetCrouchState() ? playerController.GetRigidbody().velocity.magnitude : 1f) + 0.01f;

            bobPos.x = (curveCos * bobLimit.x * (playerController.GetCrouchState() ? 1 : 0))
                        - (playerController.GetCurrentVelocity().x * travelLimit.x);

            bobPos.y = (curveSin * bobLimit.y)
                        - (playerController.GetRigidbody().velocity.y * travelLimit.y);

            bobPos.z = -(playerController.GetCurrentVelocity().y * travelLimit.z);
        }
        else
        {
            if (returnToIdleCoroutine == null)
            {
                returnToIdleCoroutine = StartCoroutine(SmoothReturnToIdle());
            }
        }
    }

    private IEnumerator SmoothReturnToIdle()
    {
        float epsilon = 0.01f;
        while (bobPos.sqrMagnitude > epsilon || bobEulerRotation.sqrMagnitude > epsilon)
        {
            bobPos = Vector3.Lerp(bobPos, Vector3.zero, Time.deltaTime * bobReturnSmooth);
            bobEulerRotation = Vector3.Lerp(bobEulerRotation, Vector3.zero, Time.deltaTime * bobRotReturnSmooth);
            yield return new WaitForEndOfFrame();
        }
        bobPos = Vector3.zero;
        bobEulerRotation = Vector3.zero;
    }

    private void BobRotation()
    {
        if (!bobSway) { bobEulerRotation = Vector3.zero; return; }

        if (playerController.GetCurrentVelocity() != Vector2.zero)
        {
            bobEulerRotation.x = multiplier.x * Mathf.Sin(2 * speedCurve);
            bobEulerRotation.y = multiplier.y * curveCos;
            bobEulerRotation.z = multiplier.z * curveCos * playerController.GetCurrentVelocity().x;
        }
        else
        {
            if (returnToIdleCoroutine == null)
            {
                returnToIdleCoroutine = StartCoroutine(SmoothReturnToIdle());
            }
        }
    }

    private void CompositePositionRotation()
    {
        float positionSmoothFactor = playerController.GetCurrentVelocity() != Vector2.zero ? smooth : bobReturnSmooth;
        float rotationSmoothFactor = playerController.GetCurrentVelocity() != Vector2.zero ? smoothRot : bobRotReturnSmooth;

        transform.localPosition = Vector3.Lerp(transform.localPosition, swayPos + bobPos, Time.deltaTime * positionSmoothFactor);
        transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.Euler(swayEulerRot) * Quaternion.Euler(bobEulerRotation), Time.deltaTime * rotationSmoothFactor);
    }
}
