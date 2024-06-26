using UnityEngine;

public class ObjectGrabbable : MonoBehaviour, IPickable
{
    [Header("Config")]
    [SerializeField] private float lerpSpeed = 10f;
    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private float throwingForce = 10f;
    [SerializeField] private float breakForceThreshold = 10f;
    [SerializeField] private float collisionCooldown = 0.1f;
    [SerializeField] private float fallHeightThreshold = 5f;

    [Header("UI Config")]
    [SerializeField] private Sprite pickUpMessage;
    [SerializeField] private Sprite dropMessage;
    [SerializeField] private Sprite throwMessage;
    [SerializeField] private Sprite rotateMessage;

    [Header("Audio Config")]
    [SerializeField] private AudioManager audioManager = null;
    [SerializeField] private string breakBottleEvent = null;

    private Rigidbody objectRigidBody;
    private Transform objectGrabPointTransform;
    private Vector3 initialLocalUp;
    private Vector3 initialLocalRight;
    private Vector3 newPosition;
    private Vector3 lastPosition;

    private DisposableObject disposableObject;
    private float lastCollisionTime = -Mathf.Infinity;
    private float initialHeight;

    public bool isObjectPickedUp { get; private set; }
    public bool isObjectBreakable = true;
    public Sprite PickUpMessage => pickUpMessage;
    public Sprite DropMessage => dropMessage;
    public Sprite ThrowMessage => throwMessage;
    public Sprite RotateMessage => rotateMessage;

    private void Awake()
    {
        objectRigidBody = GetComponent<Rigidbody>();
        disposableObject = GetComponent<DisposableObject>();
    }

    public void Grab(Transform ObjectGrabPointTransform)
    {
        this.objectGrabPointTransform = ObjectGrabPointTransform;
        objectRigidBody.useGravity = false;
        objectRigidBody.freezeRotation = true;

        initialLocalUp = transform.up;
        initialLocalRight = transform.right;

        isObjectPickedUp = true;

        initialHeight = transform.position.y;
    }

    public void Drop()
    {
        objectRigidBody.freezeRotation = false;
        this.objectGrabPointTransform = null;
        objectRigidBody.useGravity = true;
        objectRigidBody.velocity = (newPosition - lastPosition) * throwingForce;

        isObjectPickedUp = false;

        initialHeight = transform.position.y;
    }

    public void Throw(float throwingForce, Vector3 throwDirection)
    {
        objectRigidBody.freezeRotation = false;
        this.objectGrabPointTransform = null;
        objectRigidBody.useGravity = true;

        objectRigidBody.AddForce(throwDirection * throwingForce, ForceMode.Impulse);

        isObjectPickedUp = false;

        initialHeight = transform.position.y;
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

    private void OnCollisionEnter(Collision collision)
    {
        if (!isObjectBreakable)
            return;

        if (Time.time - lastCollisionTime < collisionCooldown) return;

        float currentHeight = transform.position.y;
        float heightDifference = initialHeight - currentHeight;

        if (collision.relativeVelocity.magnitude > breakForceThreshold || heightDifference > fallHeightThreshold)
        {
            if (disposableObject != null)
            {
                audioManager.PlaySound(breakBottleEvent);
                disposableObject.TriggerBreaking();
                lastCollisionTime = Time.time;
            }
        }
    }
}