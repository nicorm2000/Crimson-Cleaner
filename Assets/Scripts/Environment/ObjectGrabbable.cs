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

    [Header("Object Placement")]
    [SerializeField] private GameObject hologramObject;

    [Header("Audio Config")]
    [SerializeField] private AudioManager audioManager = null;
    [SerializeField] private string breakBottleEvent = null;

    private Rigidbody objectRigidBody;
    private Transform objectGrabPointTransform;
    private Transform playerTransform;
    private Vector3 initialLocalUp;
    private Vector3 initialLocalRight;
    private Vector3 newPosition;
    private Vector3 lastPosition;
    private bool isObjectSnapped;

    private DisposableObject disposableObject;
    private float lastCollisionTime = -Mathf.Infinity;
    private float initialHeight;

    private int currentAxisIndex = 0; // 0 -> X, 1 -> Y, 2 -> Z
    private Vector3[] rotationAxes;

    public bool isObjectSanityModifier;
    public bool IsObjectPickedUp { get; private set; }
    public bool isObjectBreakable = true;
    public bool IsObjectSnapped => isObjectSnapped;
    public Sprite PickUpMessage => CleaningManager.Instance.GetPickUpMessage();
    public Sprite DropMessage => CleaningManager.Instance.GetDropMessage();
    public Sprite ThrowMessage => CleaningManager.Instance.GetThrowMessage();
    public Sprite RotateMessage => CleaningManager.Instance.GetRotateMessage();
    public Sprite StoreMessage => CleaningManager.Instance.GetStoreMessage();

    private void Awake()
    {
        objectRigidBody = GetComponent<Rigidbody>();
        disposableObject = GetComponent<DisposableObject>();
    }

    private void Start()
    {
        rotationAxes = new Vector3[] { Vector3.right, Vector3.up, Vector3.forward };
        isObjectSnapped = false;
    }

    private void Update()
    {
        if (isObjectSanityModifier && IsObjectPickedUp)
        {
            SanityManager.Instance.ModifySanityScalar(SanityManager.Instance.GrabObjectScaler);
        }
    }

    public void Grab(Transform ObjectGrabPointTransform, Transform playerTransform)
    {
        this.objectGrabPointTransform = ObjectGrabPointTransform;
        objectRigidBody.useGravity = false;
        objectRigidBody.freezeRotation = true;
        this.playerTransform = playerTransform;

        initialLocalUp = transform.up;
        initialLocalRight = transform.right;

        IsObjectPickedUp = true;

        initialHeight = transform.position.y;

        ToggleHologram(true);
    }

    public void Drop()
    {
        objectRigidBody.freezeRotation = false;
        this.objectGrabPointTransform = null;
        this.playerTransform = null;
        objectRigidBody.useGravity = true;
        objectRigidBody.velocity = (newPosition - lastPosition) * throwingForce;

        IsObjectPickedUp = false;

        initialHeight = transform.position.y;

        ToggleHologram(false);
    }

    public void Throw(float throwingForce, Vector3 throwDirection)
    {
        objectRigidBody.freezeRotation = false;
        this.objectGrabPointTransform = null;
        this.playerTransform = null;
        objectRigidBody.useGravity = true;

        objectRigidBody.AddForce(throwDirection * throwingForce, ForceMode.Impulse);

        IsObjectPickedUp = false;

        initialHeight = transform.position.y;

        ToggleHologram(false);
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

    public void RotateObject(float sign)
    {
        //Vector3 rotationAxis = rotationAxes[currentAxisIndex];
        //float rotationAmount = sign * rotationSpeed * Time.deltaTime;
        //Quaternion rotation = Quaternion.AngleAxis(rotationAmount, rotationAxis);
        //transform.rotation = rotation * transform.rotation;

        if (playerTransform == null)
            return;

        Vector3 playerRight = playerTransform.right;
        Vector3 playerUp = playerTransform.up;
        Vector3 playerForward = playerTransform.forward;

        Vector3 rotationAxis;
        switch (currentAxisIndex)
        {
            case 0: // Eje X
                rotationAxis = playerRight;
                break;
            case 1: // Eje Y
                rotationAxis = playerUp;
                break;
            case 2: // Eje Z
                rotationAxis = playerForward;
                break;
            default:
                rotationAxis = playerUp;
                break;
        }

        float rotationAmount = sign * rotationSpeed * Time.deltaTime;
        Quaternion rotation = Quaternion.AngleAxis(rotationAmount, rotationAxis);
        transform.rotation = rotation * transform.rotation;
    }

    public void ChangeRotationAxis()
    {
        currentAxisIndex = (currentAxisIndex + 1) % 3;
    }

    public void SetObjectSnapped(bool active)
    {
        isObjectSnapped = active;
    }

    public void ToggleHologram(bool active)
    {
        if (hologramObject)
            hologramObject.SetActive(active);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (Time.time - lastCollisionTime < collisionCooldown) return;

        float currentHeight = transform.position.y;
        float heightDifference = initialHeight - currentHeight;

        if (collision.relativeVelocity.magnitude > breakForceThreshold || heightDifference > fallHeightThreshold)
        {
            if (breakBottleEvent != null)
                audioManager.PlaySound(breakBottleEvent);
            lastCollisionTime = Time.time;
            if (!isObjectBreakable)
                return;

            if (disposableObject != null)
            {
                disposableObject.TriggerBreaking();
            }
        }
    }

    public void Interact(PlayerController playerController)
    {
        throw new System.NotImplementedException();
    }
}