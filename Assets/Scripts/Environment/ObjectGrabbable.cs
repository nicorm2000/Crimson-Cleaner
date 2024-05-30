using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ObjectGrabbable : MonoBehaviour, IPick
{
    [SerializeField] private float lerpSpeed = 10f;
    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private float throwingForce = 10f;

    private Rigidbody objectRigidBody;
    private Transform ObjectGrabPointTransform;
    private Vector3 initialLocalUp;
    private Vector3 initialLocalRight;
    private Vector3 newPosition;
    private Vector3 lastPosition;

    public bool isObjectPickedUp { get; private set; }

    public string PickUpMessage => "Press E to pick up";
    public string DropMessage => "Press E to drop";
    public string ThrowMessage => "Hold E to throw";
    public string RotateMessage => "Hold G to rotate";

    private void Awake()
    {
        objectRigidBody = GetComponent<Rigidbody>();
    }

    public void Grab(Transform ObjectGrabPointTransform)
    {
        this.ObjectGrabPointTransform = ObjectGrabPointTransform;
        objectRigidBody.useGravity = false;
        //objectRigidBody.isKinematic = true;
        objectRigidBody.freezeRotation = true;

        initialLocalUp = transform.up;
        initialLocalRight = transform.right;

        isObjectPickedUp = true;
    }

    public void Drop()
    {
        objectRigidBody.freezeRotation = false;
        this.ObjectGrabPointTransform = null;
        objectRigidBody.useGravity = true;
        //objectRigidBody.isKinematic = false;
        objectRigidBody.velocity = (newPosition - lastPosition) * throwingForce;

        isObjectPickedUp = false;
    }

    private void FixedUpdate()
    {
        if (ObjectGrabPointTransform != null)
        {
            lastPosition = newPosition;
            newPosition = Vector3.Lerp(transform.position, ObjectGrabPointTransform.position, Time.deltaTime * lerpSpeed);
            objectRigidBody.MovePosition(newPosition);
        }
    }

    public void RotateObject(float mouseX, float mouseY)
    {
        transform.Rotate(initialLocalUp, mouseX * rotationSpeed * Time.deltaTime, Space.World);
        transform.Rotate(initialLocalRight, mouseY * rotationSpeed * Time.deltaTime, Space.World);
    }
}
