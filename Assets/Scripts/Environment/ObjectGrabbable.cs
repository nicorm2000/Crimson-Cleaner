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
    [SerializeField] private bool isObjectMulitple = false;

    [Header("Object Placement")]
    [SerializeField] private GameObject hologramObject;

    [Header("Audio Config")]
    [SerializeField] private AudioManager audioManager = null;
    [SerializeField] private string breakBottleEvent = null;

    private ToolAnimatorController toolAnimatorController;
    private Rigidbody objectRigidBody;
    private Transform[] objectTransforms;
    private Transform objectGrabPointTransform;
    private Transform playerTransform;
    private Vector3 newPosition;
    private Vector3 lastPosition;
    private Vector3[] grabOffsets;
    private Quaternion[] originalRotations;
    private bool isObjectSnapped;

    private DisposableObject disposableObject;
    private float lastCollisionTime = -Mathf.Infinity;
    private float initialHeight;

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
        if (isObjectMulitple)
        {
            objectTransforms = GetComponentsInChildren<Transform>();
            grabOffsets = new Vector3[objectTransforms.Length];
            originalRotations = new Quaternion[objectTransforms.Length];

            for (int i = 0; i < objectTransforms.Length; i++)
            {
                originalRotations[i] = objectTransforms[i].rotation;
            }
        }
        objectRigidBody = GetComponent<Rigidbody>();
        disposableObject = GetComponent<DisposableObject>();

    }

    private void Start()
    {
        isObjectSnapped = false;
        toolAnimatorController = CleaningManager.Instance.GetToolSelector().toolAnimatorController;
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
        toolAnimatorController.TriggerParticularAction(toolAnimatorController.GetHandsGrabName(), true);

        this.objectGrabPointTransform = ObjectGrabPointTransform;
        this.playerTransform = playerTransform;

        if (isObjectMulitple)
        {
            for (int i = 0; i < objectTransforms.Length; i++)
            {
                Rigidbody rb = objectTransforms[i].gameObject.GetComponent<Rigidbody>();

                if (rb != null)
                {
                    rb.useGravity = false;
                    rb.freezeRotation = true;
                }
            }
        }
        else
        {
            objectRigidBody.useGravity = false;
            objectRigidBody.freezeRotation = true;
        }

        initialHeight = transform.position.y;

        IsObjectPickedUp = true;
        ToggleHologram(true);
    }

    public void Drop()
    {
        toolAnimatorController.TriggerParticularAction(toolAnimatorController.GetHandsGrabName(), false);
        toolAnimatorController.TriggerParticularAction(toolAnimatorController.GetHandsDropName());

        this.objectGrabPointTransform = null;
        this.playerTransform = null;

        if (isObjectMulitple)
        {
            foreach (var item in objectTransforms)
            {
                Rigidbody rb = item.gameObject.GetComponent<Rigidbody>();

                if (rb != null)
                {
                    rb.useGravity = true;
                    rb.freezeRotation = false;
                    rb.velocity = (newPosition - lastPosition) * throwingForce;
                }
            }
        }
        else
        {
            objectRigidBody.useGravity = true;
            objectRigidBody.freezeRotation = false;
            objectRigidBody.velocity = (newPosition - lastPosition) * throwingForce;
        }


        IsObjectPickedUp = false;

        initialHeight = transform.position.y;
        ToggleHologram(false);
    }

    public void Throw(float throwingForce, Vector3 throwDirection)
    {
        toolAnimatorController.TriggerParticularAction(toolAnimatorController.GetHandsGrabName(), false);
        toolAnimatorController.TriggerParticularAction(toolAnimatorController.GetHandsThrowName());

        this.objectGrabPointTransform = null;
        this.playerTransform = null;

        if (isObjectMulitple)
        {
            foreach (var item in objectTransforms)
            {
                Rigidbody rb = item.gameObject.GetComponent<Rigidbody>();

                if (rb != null)
                {
                    rb.freezeRotation = false;
                    rb.useGravity = true;
                    rb.AddForce(throwDirection * throwingForce, ForceMode.Impulse);
                }
            }
        }
        else
        {
            objectRigidBody.freezeRotation = false;
            objectRigidBody.useGravity = true;

            objectRigidBody.AddForce(throwDirection * throwingForce, ForceMode.Impulse);
        }


        initialHeight = transform.position.y;

        IsObjectPickedUp = false;
        ToggleHologram(false);
    }

    private void FixedUpdate()
    {
        if (isObjectSanityModifier && IsObjectPickedUp)
        {
            SanityManager.Instance.ModifySanityScalar(SanityManager.Instance.GrabObjectScaler);
        }

        if (objectGrabPointTransform != null)
        {
            if (isObjectMulitple)
            {
                for (int i = 0; i < objectTransforms.Length; i++)
                {
                    Rigidbody rb = objectTransforms[i].GetComponent<Rigidbody>();

                    if (rb != null)
                    {
                        Vector3 targetPosition = objectGrabPointTransform.position + grabOffsets[i];
                        rb.MovePosition(Vector3.Lerp(objectTransforms[i].position, targetPosition, Time.fixedDeltaTime * lerpSpeed));
                    }
                    else
                    {
                        transform.position = Vector3.MoveTowards(objectTransforms[i].position, objectGrabPointTransform.position, Time.fixedDeltaTime * lerpSpeed);
                    }
                }
            }
            else
            {
                lastPosition = newPosition;
                newPosition = Vector3.Lerp(transform.position, objectGrabPointTransform.position, Time.fixedDeltaTime * lerpSpeed);
                objectRigidBody.MovePosition(newPosition);
            }
        }
    }

    public void RotateObject(float mouseX, float mouseY, Transform cameraTransform)
    {
        transform.Rotate(cameraTransform.up, mouseX * rotationSpeed * Time.deltaTime, Space.World);
        transform.Rotate(cameraTransform.right, -mouseY * rotationSpeed * Time.deltaTime, Space.World);
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
            if (breakBottleEvent != null && audioManager != null)
                audioManager.PlaySound(breakBottleEvent, gameObject);
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