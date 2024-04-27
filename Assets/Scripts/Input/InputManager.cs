using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class InputManager : MonoBehaviour
{
    [SerializeField] private bool shouldHideCursor;

    private PlayerInput playerinput;

    public Vector2 Move { get; private set; }
    public Vector2 Look { get; private set; }
    public bool Crouch { get; private set; }

    private InputActionMap currentMap;
    private InputAction moveAction;
    private InputAction lookAction;
    private InputAction lightSwitchAction;
    private InputAction openAction;
    private InputAction cleanAction;
    private InputAction crouchAction;

    public event Action OpenEvent;
    public event Action ToggleLightsEvent;
    public event Action<bool> CleanEvent;

    private void Awake()
    {
        playerinput = GetComponent<PlayerInput>();
        currentMap = playerinput.currentActionMap;
        moveAction = currentMap.FindAction("Move");
        lookAction = currentMap.FindAction("Look");
        lightSwitchAction = currentMap.FindAction("ToggleLights");
        openAction = currentMap.FindAction("Open");
        cleanAction = currentMap.FindAction("Clean");
        crouchAction = currentMap.FindAction("Crouch");

        moveAction.performed += OnMove;
        lookAction.performed += OnLook;
        crouchAction.performed += OnCrouch;
        openAction.performed += OnOpen;
        cleanAction.started += ctx => OnClean(true);
        lightSwitchAction.performed += ctx => OnToggleLights();

        moveAction.canceled += OnMove;
        lookAction.canceled += OnLook;
        crouchAction.canceled += OnCrouch;
        //lightSwitchAction.canceled += OnToggleLights;
        //openAction.canceled += OnOpen;
        cleanAction.canceled += ctx => OnClean(false);
        //lightSwitchAction.canceled += ctx => OnToggleLights(ctx);

        if (shouldHideCursor) HideCursor();
    }

    private void OnEnable()
    {
        currentMap.Enable();
    }

    private void OnDisable()
    {
        currentMap.Disable();
    }

    private void HideCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        Move = context.ReadValue<Vector2>();
    }

    private void OnLook(InputAction.CallbackContext context)
    {
        Look = context.ReadValue<Vector2>();
    }

    private void OnCrouch(InputAction.CallbackContext context)
    {
        Crouch = context.ReadValueAsButton();
    }

    private void OnToggleLights()
    {
        ToggleLightsEvent?.Invoke();
    }

    private void OnOpen(InputAction.CallbackContext context)
    {
        OpenEvent?.Invoke();
    }
    
    private void OnClean(bool isCleaning)
    {
        CleanEvent?.Invoke(isCleaning);
    }
}
