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

    [Header("Audio Config")]
    [SerializeField] private AudioManager audioManager = null;
    [SerializeField] private string footStepsPlayEvent = null;
    [SerializeField] private string footStepsStopEvent = null;

    private Rigidbody playerRigidBody;
    private Animator animator;
    private bool hasAnimator;

    private int xVelHash;
    private int yVelHash;
    private int crouchlHash;
    
    private float xRotation;
    private Vector2 currentVelocity;
    private Vector2 mousePos;

    private float footstepTimer;
    private bool isMoving;

    private ObjectGrabbable objectGrabbable;

    private void Start()
    {
        hasAnimator = TryGetComponent(out animator);
        playerRigidBody = GetComponent<Rigidbody>();

        xVelHash = Animator.StringToHash("X_Velocity");
        yVelHash = Animator.StringToHash("Y_Velocity");
        crouchlHash = Animator.StringToHash("Crouch");

        footstepTimer = footstepInterval;
    }

    private void FixedUpdate()
    {
        Move();
        HandleCrouch();
        HandleFootsteps();
    }

    private void Update()
    {
        CameraMovements();
    }

    private void Move()
    {
        if (!hasAnimator) return;

        float targetSpeed = walkSpeed;
        if (inputManager.Crouch) targetSpeed = 1.5f;

        if (inputManager.Move == Vector2.zero) targetSpeed = 0.1f;


        currentVelocity.x = Mathf.Lerp(currentVelocity.x, inputManager.Move.x * targetSpeed, animationBlendSpeed * Time.fixedDeltaTime);
        currentVelocity.y = Mathf.Lerp(currentVelocity.y, inputManager.Move.y * targetSpeed, animationBlendSpeed * Time.fixedDeltaTime);

        var xVelDifference = currentVelocity.x - playerRigidBody.velocity.x;
        var yVelDifference = currentVelocity.y - playerRigidBody.velocity.y;

        playerRigidBody.AddForce(transform.TransformVector(new Vector3(xVelDifference, 0, yVelDifference)), ForceMode.VelocityChange);

        animator.SetFloat(xVelHash, currentVelocity.x);
        animator.SetFloat(yVelHash, currentVelocity.y);

        isMoving = inputManager.Move != Vector2.zero;
    }

    private void HandleCrouch() => animator.SetBool(crouchlHash, inputManager.Crouch);

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

        if (objectGrabbable != null && inputManager.RotateObject)
        {
            RotateObject(mousePos.x, mousePos.y);
        }
        else
        {            
            camera.position = cameraRoot.position;

            xRotation -= mousePos.y;
            xRotation = Mathf.Clamp(xRotation, rotationUpperLimit, rotationBottomLimit);

            camera.localRotation = Quaternion.Euler(xRotation, 0, 0);
            transform.Rotate(Vector3.up, mousePos.x);
        }
    }

    private void RotateObject(float mouseX, float mouseY)
    {
        if (objectGrabbable != null)
        {
            if (mouseX != 0 || mouseY != 0)
            {
                objectGrabbable.RotateObject(mouseX, mouseY);
            }
        }
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
    
    public bool GetCrouchState()
    {
        return inputManager.Crouch;
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
