using UnityEngine;

public class ObjectGrabbable : MonoBehaviour, IPick
{
    [SerializeField] private float lerpSpeed = 10f;
    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private float throwingForce = 10f;

    private Rigidbody objectRigidBody;
    private Transform objectGrabPointTransform;
    private Vector3 initialLocalUp;
    private Vector3 initialLocalRight;
    private Vector3 newPosition;
    private Vector3 lastPosition;

    public bool isObjectPickedUp { get; private set; }

    [SerializeField] private Sprite pickUpMessage;
    [SerializeField] private Sprite dropMessage;
    [SerializeField] private Sprite throwMessage;
    [SerializeField] private Sprite rotateMessage;

    public Sprite PickUpMessage => pickUpMessage;
    public Sprite DropMessage => dropMessage;
    public Sprite ThrowMessage => throwMessage;
    public Sprite RotateMessage => rotateMessage;

    private void Awake()
    {
        objectRigidBody = GetComponent<Rigidbody>();
    }

    public void Grab(Transform ObjectGrabPointTransform)
    {
        this.objectGrabPointTransform = ObjectGrabPointTransform;
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
        this.objectGrabPointTransform = null;
        objectRigidBody.useGravity = true;
        //objectRigidBody.isKinematic = false;
        objectRigidBody.velocity = (newPosition - lastPosition) * throwingForce;

        isObjectPickedUp = false;
    }

    public void Throw(float throwingForce, Vector3 throwDirection)
    {
        objectRigidBody.freezeRotation = false;
        this.objectGrabPointTransform = null;
        objectRigidBody.useGravity = true;

        objectRigidBody.AddForce(throwDirection * throwingForce, ForceMode.Impulse);

        isObjectPickedUp = false;
    }

    private void FixedUpdate()
    {
        if (objectGrabPointTransform != null)
        {
            lastPosition = newPosition;
            newPosition = Vector3.Lerp(transform.position, objectGrabPointTransform.position, Time.deltaTime * lerpSpeed);
            objectRigidBody.MovePosition(newPosition);
        }
    }

    public void RotateObject(float mouseX, float mouseY)
    {
        transform.Rotate(initialLocalUp, mouseX * rotationSpeed * Time.deltaTime, Space.World);
        transform.Rotate(initialLocalRight, mouseY * rotationSpeed * Time.deltaTime, Space.World);
    }
}
