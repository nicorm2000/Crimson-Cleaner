using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private InputManager inputManager;
    [SerializeField] private PlayerSensitivitySettings playerSensitivitySettings;
    [SerializeField] private float animationBlendSpeed;
    [SerializeField] private Transform cameraRoot;
    [SerializeField] private Transform camera;

    [Header("Controller Config")]
    [SerializeField] private float walkSpeed = 10f;
    [SerializeField] private float footstepInterval = 0.5f;
    [SerializeField] private float rotationUpperLimit = -40f;
    [SerializeField] private float rotationBottomLimit = 70f;
    [SerializeField] private float mouseSensitivity = 22f;
    [SerializeField] private float controllerSensitivity = 22f;
    [SerializeField] private float distanceToGroundRaycast = 2f;

    [Header("Audio Config")]
    [SerializeField] private AudioManager audioManager = null;
    [SerializeField] private string footStepsPlayEvent = null;
    [SerializeField] private string footStepsStopEvent = null;

    [NonSerialized] public bool isCameraMovable = true;

    private Rigidbody playerRigidBody;
    private Animator animator;
    private bool hasAnimator;

    private int xVelHash;
    private int yVelHash;
    
    private float xRotation;
    private Vector2 currentVelocity;
    private Vector2 mousePos;

    private float footstepTimer;
    private bool isMoving;

    private ObjectGrabbable objectGrabbable;

    private void OnEnable()
    {
        inputManager.ChangeRotationAxisEvent += OnChangeRotationAxis;
    }

    private void OnDisable()
    {
        inputManager.ChangeRotationAxisEvent -= OnChangeRotationAxis;
    }

    private void Start()
    {
        hasAnimator = TryGetComponent(out animator);
        playerRigidBody = GetComponent<Rigidbody>();

        xVelHash = Animator.StringToHash("X_Velocity");
        yVelHash = Animator.StringToHash("Y_Velocity");

        footstepTimer = footstepInterval;
    }

    private void FixedUpdate()
    {
        HandleGravity();
        Move();
        HandleFootsteps();
    }

    private void Update()
    {
        if (isCameraMovable)
            CameraMovements();

        if (objectGrabbable != null)
        {
            if (inputManager.RotatePos)
                objectGrabbable.RotateObject(1);

            if (inputManager.RotateNeg)
                objectGrabbable.RotateObject(-1);
        }
    }

    private void Move()
    {
        if (!hasAnimator) return;

        float targetSpeed = walkSpeed;

        if (inputManager.Move == Vector2.zero) targetSpeed = 0.1f;

        // Update current velocity based on input
        currentVelocity.x = Mathf.Lerp(currentVelocity.x, inputManager.Move.x * targetSpeed, animationBlendSpeed * Time.fixedDeltaTime);
        currentVelocity.y = Mathf.Lerp(currentVelocity.y, inputManager.Move.y * targetSpeed, animationBlendSpeed * Time.fixedDeltaTime);

        // Create a movement vector using the correct axes
        Vector3 movement = new Vector3(currentVelocity.x, 0, currentVelocity.y); // X for horizontal (left/right), Z for forward/backward

        // Apply the movement relative to the player's forward direction
        Vector3 targetVelocity = transform.TransformDirection(movement);
        Vector3 velocityDifference = targetVelocity - playerRigidBody.velocity;

        // Apply force to the Rigidbody
        playerRigidBody.AddForce(velocityDifference, ForceMode.VelocityChange);

        // Update animator with movement values
        animator.SetFloat(xVelHash, currentVelocity.x);
        animator.SetFloat(yVelHash, currentVelocity.y);

        isMoving = inputManager.Move != Vector2.zero;
    }

    private void HandleFootsteps()
    {
        if (isMoving)
        {
            footstepTimer -= Time.fixedDeltaTime;
            if (footstepTimer <= 0)
            {
                audioManager.PlaySound(footStepsPlayEvent);
                footstepTimer = footstepInterval;
            }
        }
        else
        {
            audioManager.PlaySound(footStepsStopEvent);
        }
    }

    private void CameraMovements()
    {
        if (!hasAnimator) return;

        mousePos.x = inputManager.Look.x * playerSensitivitySettings.sensitivityX * Time.deltaTime;
        mousePos.y = inputManager.Look.y * playerSensitivitySettings.sensitivityY * Time.deltaTime;
        camera.position = cameraRoot.position;

        xRotation -= mousePos.y;
        xRotation = Mathf.Clamp(xRotation, rotationUpperLimit, rotationBottomLimit);

        camera.localRotation = Quaternion.Euler(xRotation, 0, 0);
        transform.Rotate(Vector3.up, mousePos.x);
    }

    private void HandleGravity()
    {
        distanceToGroundRaycast = 2.0f;

        // Crea un raycast desde el centro del jugador hacia abajo
        RaycastHit hit;
        if (!Physics.Raycast(transform.position, Vector3.down, out hit, distanceToGroundRaycast))
        {
            // Si el jugador est� en el aire (el raycast no toca el suelo)
            Vector3 gravityForce = new Vector3(0, -9.81f, 0);
            playerRigidBody.AddForce(gravityForce, ForceMode.Acceleration);
        }
        else
        {
            if (hit.point.y < transform.position.y)
            {
                Vector3 targetPosition = new Vector3(transform.position.x, hit.point.y + 0.1f, transform.position.z);
                transform.position = Vector3.Lerp(transform.position, targetPosition, 0.1f);
            }
        }
    }

    private void OnChangeRotationAxis()
    {
        if (objectGrabbable != null)
            objectGrabbable.ChangeRotationAxis();
    }

    public void SetObjectGrabbable(ObjectGrabbable grabbable)
    {
        objectGrabbable = grabbable;
    }

    public ObjectGrabbable GetObjectGrabbable()
    {
        return objectGrabbable;
    }

    public void ClearObjectGrabbable()
    {
        objectGrabbable = null;
    }

    public Vector2 GetMousePos()
    {
        return mousePos;
    }
    
    public Rigidbody GetRigidbody()
    {
        return GetComponent<Rigidbody>();
    }
    
    public Vector2 GetCurrentVelocity()
    {
        return currentVelocity;
    }
}
