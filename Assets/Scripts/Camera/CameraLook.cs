using UnityEngine;

public class CameraLook : MonoBehaviour
{
    [SerializeField] private InputManager inputManager;
    [SerializeField] private Transform orientation;

    [SerializeField] float mouseSensitivity = 25f;
    [SerializeField] float joystickSensitivity = 100f;

    private float xRotation = 0f;
    private float yRotation = 0f;

    private void OnEnable()
    {
        InputManager.LookEvent += OnLook;
    }

    private void OnDisable()
    {
        InputManager.LookEvent -= OnLook;
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void OnLook(Vector2 cameraInput)
    {
        ProcessLook(cameraInput);
    }

    public void ProcessLook(Vector2 cameraInput)
    {
        Vector2 joystick = inputManager.inputMaster.Player.LookController.ReadValue<Vector2>() * joystickSensitivity * Time.deltaTime;

        bool isJoystickInput = Mathf.Abs(joystick.x) > 0.1f || Mathf.Abs(joystick.y) > 0.1f;

        float sensX = isJoystickInput ? joystickSensitivity : mouseSensitivity;
        float sensY = isJoystickInput ? joystickSensitivity : mouseSensitivity;


        float mouseX = cameraInput.x * Time.deltaTime * sensX;
        float mouseY = cameraInput.y * Time.deltaTime * sensY;

        yRotation += mouseX;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);
    }
}
