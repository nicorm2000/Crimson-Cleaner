using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLook : MonoBehaviour
{
    [SerializeField] InputManager inputManager;
    [SerializeField] float mouseSensitivity = 25f;
    [SerializeField] Transform body;

    private float xRotation = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;        
    }

    void Update()
    {
        float mouseX = inputManager.inputMaster.CameraLook.MouseX.ReadValue<float>() * mouseSensitivity * Time.deltaTime;
        float mouseY = inputManager.inputMaster.CameraLook.MouseY.ReadValue<float>() * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);   

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        body.Rotate(Vector3.up * mouseX);
    }
}
