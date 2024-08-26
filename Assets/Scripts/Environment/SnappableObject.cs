using System;
using UnityEngine;

public class SnappableObject : MonoBehaviour
{
    public SnapPoint snapPoint;
    public float distance = 0.2f;
    public float angle = 10f;

    private ObjectGrabbable objectGrabbable;

    public event Action Snapped;

    private bool isObjectSnapped = false;

    private void Awake()
    {
        objectGrabbable = GetComponent<ObjectGrabbable>();
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

        Snapped?.Invoke();
    }
}
