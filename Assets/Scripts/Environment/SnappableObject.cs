using System;
using System.Collections;
using UnityEngine;

public class SnappableObject : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private SnapPoint snapPoint;
    [SerializeField] private PickUpDrop pickUpDrop;
    [SerializeField] private float distance = 0.2f;
    [SerializeField] private float angle = 10f;
    [SerializeField] private float completionShaderDuration = 1.5f;

    [Header("Materials")]
    [Header("Single Object")]
    [SerializeField] private Material completeSnapMaterial;
    [Header("If Cleanable")]
    [SerializeField] private Material[] cleaningCompleteSnapMaterials;

    [Header("Multiple Objects")]
    [SerializeField] private Material[] completeSnapMaterials;

    [Header("Audio Config")]
    [SerializeField] private AudioManager audioManager = null;
    [SerializeField] private string snapObjectEvent = null;

    private ObjectGrabbable objectGrabbable;
    private Material previousMaterial;
    private Material[] previousMaterials;

    public event Action Snapped;
    public event Action<GameObject> SnappedGO;

    private bool isObjectSnappable = false;
    private bool isObjectSnapped = false;
    private MeshRenderer meshRenderer;

    private Rigidbody rb;

    private bool isCleanable = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        objectGrabbable = GetComponent<ObjectGrabbable>();
        meshRenderer = GetComponent<MeshRenderer>();

        isCleanable = gameObject.GetComponent<Clean>() != null ? true : false;

        //Debug.Log("Material is: " + baseMaterial);
    }

    private void Start()
    {
        StartCoroutine(WaitForSnapDelay());
    }

    void Update()
    {
        if (!isObjectSnapped && isObjectSnappable && pickUpDrop.GetObjectGrabbable() != null)
        {
            if (pickUpDrop.GetObjectGrabbable() == objectGrabbable)
            {
                if (IsNearSnapPoint())
                {
                    SnapObject();
                }
            }
        }
    }

    private IEnumerator WaitForSnapDelay()
    {
        yield return new WaitForSeconds(1.5f);
        isObjectSnappable = true;
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
        
        if (completeSnapMaterial != null)
        {
            previousMaterial = meshRenderer.material;
            if (isCleanable)
                meshRenderer.material = cleaningCompleteSnapMaterials[gameObject.GetComponent<Clean>().GetMaterialIndex()];
            else
                meshRenderer.material = completeSnapMaterial;
        }
        else if (completeSnapMaterials.Length != 0)
        {
            var currentMaterials = meshRenderer.materials;

            previousMaterials = new Material[currentMaterials.Length];

            for (int i = 0; i < completeSnapMaterials.Length; i++)
            {
                previousMaterials[i] = currentMaterials[i];
            }
            
            meshRenderer.materials = completeSnapMaterials;
        }

        Snapped?.Invoke();
        SnappedGO?.Invoke(gameObject);
        if (snapObjectEvent != null && audioManager != null)
            audioManager.PlaySound(snapObjectEvent);
        StartCoroutine(WaitToSwapMaterial(completionShaderDuration));
    }

    private IEnumerator WaitToSwapMaterial(float duration)
    {
        yield return new WaitForSeconds(duration);

        if (completeSnapMaterial != null)
        {
            meshRenderer.material = previousMaterial;
        }
        else if (completeSnapMaterials.Length != 0 && previousMaterials != null)
        {
            meshRenderer.materials = previousMaterials;
        }
    }
}
