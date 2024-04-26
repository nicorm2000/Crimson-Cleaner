using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLook : MonoBehaviour
{
    [SerializeField] InputManager inputManager;
    [SerializeField] float mouseSensitivity = 25f;
    [SerializeField] float joystickSensitivity = 100f;
    [SerializeField] Transform body;

    private float xRotation = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;        
    }

    void FixedUpdate()
    {
        float mouseX = inputManager.inputMaster.CameraLook.MouseX.ReadValue<float>() * mouseSensitivity * Time.deltaTime;
        float mouseY = inputManager.inputMaster.CameraLook.MouseY.ReadValue<float>() * mouseSensitivity * Time.deltaTime;

        float joystickX = inputManager.inputMaster.CameraLook.JoystickX.ReadValue<float>() * joystickSensitivity * Time.deltaTime;
        float joystickY = inputManager.inputMaster.CameraLook.JoystickY.ReadValue<float>() * joystickSensitivity * Time.deltaTime;

        bool isJoystickInput = Mathf.Abs(joystickX) > 0.1f || Mathf.Abs(joystickY) > 0.1f;

        float finalXInput = isJoystickInput ? joystickX : mouseX;
        float finalYInput = isJoystickInput ? joystickY : mouseY;

        if (Mathf.Abs(finalXInput) > 0.1f || Mathf.Abs(finalYInput) > 0.1f)
        {
            xRotation -= finalYInput;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            body.Rotate(Vector3.up * finalXInput);
        }
    }
}
