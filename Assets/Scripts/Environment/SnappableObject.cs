using System;
using System.Collections;
using UnityEngine;

public class SnappableObject : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private Material baseMaterial;
    [SerializeField] private Material completeSnapMaterial;
    [SerializeField] private SnapPoint snapPoint;
    [SerializeField] private float distance = 0.2f;
    [SerializeField] private float angle = 10f;
    [SerializeField] private float completionShaderDuration = 1.5f;

    [Header("Audio Config")]
    [SerializeField] private AudioManager audioManager = null;
    [SerializeField] private string snapObjectEvent = null;

    private ObjectGrabbable objectGrabbable;

    public event Action Snapped;
    public event Action<GameObject> SnappedGO;

    private bool isObjectSnapped = false;
    private MeshRenderer meshRenderer;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        objectGrabbable = GetComponent<ObjectGrabbable>();
        meshRenderer = GetComponent<MeshRenderer>();
        Debug.Log("Material is: " + baseMaterial);
    }

    void Update()
    {
        if (!isObjectSnapped)
        {
            if (IsNearSnapPoint())
            {
                 SnapObject();
            }
        }
    }

    private bool IsNearSnapPoint()
    {
        float newDistance = Vector3.Distance(transform.position, snapPoint.snapTransform.position);
        float newAngle = Quaternion.Angle(transform.rotation, snapPoint.snapTransform.rotation);

        float tolerance = Mathf.Lerp(angle, angle * 1.5f, newDistance / distance);

        return newDistance < distance && newAngle < tolerance; 
    }

    private void SnapObject()
    {
        if (rb != null)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.isKinematic = true; 
        }

        transform.position = snapPoint.snapTransform.position;
        transform.rotation = snapPoint.snapTransform.rotation;

        objectGrabbable.SetObjectSnapped(true);
        
        isObjectSnapped = true;
        objectGrabbable.enabled = false;
        baseMaterial = meshRenderer.material;
        meshRenderer.material = completeSnapMaterial;
        Snapped?.Invoke();
        SnappedGO?.Invoke(gameObject);
        if (snapObjectEvent != null)
            audioManager.PlaySound(snapObjectEvent);
        StartCoroutine(WaitToSwapMaterial(completionShaderDuration));
    }

    private IEnumerator WaitToSwapMaterial(float duration)
    {
        yield return new WaitForSeconds(duration);
        meshRenderer.material = baseMaterial;
    }
}
