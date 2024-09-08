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

    private bool isObjectSnapped = false;
    private MeshRenderer meshRenderer;

    private void Awake()
    {
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

        return newDistance < distance && newAngle < angle; 
    }

    private void SnapObject()
    {
        transform.position = snapPoint.snapTransform.position;
        transform.rotation = snapPoint.snapTransform.rotation;

        objectGrabbable.SetObjectSnapped(true);
        
        isObjectSnapped = true;
        objectGrabbable.enabled = false;
        meshRenderer.material = completeSnapMaterial;
        Snapped?.Invoke();
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
