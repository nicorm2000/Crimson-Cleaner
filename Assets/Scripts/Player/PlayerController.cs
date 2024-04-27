using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private InputManager inputManager;
    [SerializeField] private float animationBlendSpeed;

    [SerializeField] private Transform cameraRoot;
    [SerializeField] private Transform camera;

    [SerializeField] private float walkSpeed = 10f;
    [SerializeField] private float rotationUpperLimit = -40f;
    [SerializeField] private float rotationBottomLimit = 70f;
    [SerializeField] private float mouseSensitivity = 22f;

    private Rigidbody playerRigidBody;
    private Animator animator;
    private bool hasAnimator;

    private int xVelHash;
    private int yVelHash;
    private int crouchlHash;
    

    private float xRotation;


    private Vector2 currentVelocity;

    private void Start()
    {
        hasAnimator = TryGetComponent<Animator>(out animator);
        playerRigidBody = GetComponent<Rigidbody>();

        xVelHash = Animator.StringToHash("X_Velocity");
        yVelHash = Animator.StringToHash("Y_Velocity");
        crouchlHash = Animator.StringToHash("Crouch");
    }

    private void FixedUpdate()
    {
        Move();
        HandleCrouch();
    }

    private void LateUpdate()
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
    }

    private void HandleCrouch() => animator.SetBool(crouchlHash, inputManager.Crouch);

    private void CameraMovements()
    {
        if (!hasAnimator) return;

        var mouseX = inputManager.Look.x;
        var mouseY = inputManager.Look.y;

        camera.position = cameraRoot.position;

        xRotation -= mouseY * mouseSensitivity * Time.deltaTime;
        xRotation = Mathf.Clamp(xRotation, rotationUpperLimit, rotationBottomLimit);

        camera.localRotation = Quaternion.Euler(xRotation, 0, 0);
        transform.Rotate(Vector3.up, mouseX * mouseSensitivity * Time.deltaTime);
    }
}
