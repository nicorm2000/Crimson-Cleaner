using System;
using System.Collections;
using Unity.VisualScripting;
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
    [NonSerialized] public bool isMovable = true;

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


    private void Update()
    {
        Move();
        HandleFootsteps();

        if (objectGrabbable != null)
        {
            if (inputManager.RotatePos)
                objectGrabbable.RotateObject(1);

            if (inputManager.RotateNeg)
                objectGrabbable.RotateObject(-1);
        }
    }

    private void FixedUpdate()
    {
        if (isCameraMovable)
            CameraMovements();
    }

    private void Move()
    {
        if (!hasAnimator) return;

        if (!isMovable)
        {
            animator.SetFloat(xVelHash, 0f);
            animator.SetFloat(yVelHash, 0f);

            isMoving = false;

            return;
        }

        float targetSpeed = walkSpeed;

        if (inputManager.Move == Vector2.zero) targetSpeed = 0.1f;

        Vector3 forward = camera.forward;
        Vector3 right = camera.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        Vector3 desiredMoveDirection = forward * inputManager.Move.y + right * inputManager.Move.x;

        playerRigidBody.MovePosition(transform.position + desiredMoveDirection * targetSpeed * Time.deltaTime);

        // Actualizar el animator con los valores de movimiento
        currentVelocity.x = Mathf.Lerp(currentVelocity.x, inputManager.Move.x * targetSpeed, animationBlendSpeed * Time.deltaTime);
        currentVelocity.y = Mathf.Lerp(currentVelocity.y, inputManager.Move.y * targetSpeed, animationBlendSpeed * Time.deltaTime);

        animator.SetFloat(xVelHash, currentVelocity.x);
        animator.SetFloat(yVelHash, currentVelocity.y);

        isMoving = inputManager.Move != Vector2.zero;
    }

    public void ModifyPlayerSpeed(float newSpeed)
    {
        walkSpeed = newSpeed;
    }

    public void ModifyPlayerFootstepAudioSpeed(float newSpeed)
    {
        footstepInterval = newSpeed;
    }

    private void HandleFootsteps()
    {
        if (isMoving)
        {
            footstepTimer -= Time.deltaTime;
            if (footstepTimer <= 0)
            {
                audioManager.PlaySound(footStepsPlayEvent);
                footstepTimer += footstepInterval;
            }
        }
        else
        {
            footstepTimer = footstepInterval;
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

    public void BlockMovement(float duration)
    {
        StartCoroutine(ToggleMovement(duration));
    }

    private IEnumerator ToggleMovement(float duration)
    {
        isMovable = false;

        yield return new WaitForSeconds(duration);

        isMovable = true;
    }

    public void BlockCamera(float duration)
    {
        StartCoroutine(ToggleCameraMovement(duration));
    }

    private IEnumerator ToggleCameraMovement(float duration)
    {
        isCameraMovable = false;

        yield return new WaitForSeconds(duration);

        isCameraMovable = true;
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
