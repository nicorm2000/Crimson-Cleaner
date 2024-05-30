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
    public bool IsLookInputMouse { get; private set; }
    public bool Crouch { get; private set; }
    public bool RotateObject { get; private set; }
    public float Scroll { get; private set; }

    private InputActionMap currentMap;
    private InputAction moveAction;
    private InputAction lookAction;
    private InputAction interactAction;
    private InputAction cleanAction;
    private InputAction crouchAction;
    private InputAction rotateObejctAction;
    private InputAction pickUpAction;
    private InputAction throwAction;
    private InputAction cleaningListAction;
    private InputAction displayControlsAction;
    private InputAction selectFirstToolAction;
    private InputAction selectSecondToolAction;
    private InputAction selectThirdToolAction;
    private InputAction mouseScrollAction;

    public event Action PickUpEvent;
    public event Action InteractEvent;
    public event Action CleaningListEvent;
    public event Action DisplayControlsEvent;
    public event Action SelectFirstToolEvent;
    public event Action SelectSecondToolEvent;
    public event Action SelectThirdToolEvent;
    public event Action<bool> CleanEvent;
    public event Action ThrowStartEvent;
    public event Action ThrowEndEvent;

    private bool isCursorVisible = true;

    private void Awake()
    {
        playerinput = GetComponent<PlayerInput>();
        currentMap = playerinput.currentActionMap;
        moveAction = currentMap.FindAction("Move");
        lookAction = currentMap.FindAction("Look");
        interactAction = currentMap.FindAction("Interact");
        cleanAction = currentMap.FindAction("Clean");
        crouchAction = currentMap.FindAction("Crouch");
        rotateObejctAction = currentMap.FindAction("RotateObejct");
        pickUpAction = currentMap.FindAction("PickUp");
        throwAction = currentMap.FindAction("Throw");
        cleaningListAction = currentMap.FindAction("CleaningList");
        displayControlsAction = currentMap.FindAction("DisplayControls");
        selectFirstToolAction = currentMap.FindAction("SelectFirstTool");
        selectSecondToolAction = currentMap.FindAction("SelectSecondTool");
        selectThirdToolAction = currentMap.FindAction("SelectThirdTool");
        mouseScrollAction = currentMap.FindAction("MouseScroll");

        moveAction.performed += OnMove;
        lookAction.performed += OnLook;
        crouchAction.performed += OnCrouch;
        rotateObejctAction.performed += OnRotateObject;
        interactAction.performed += OnInteract;
        pickUpAction.performed += OnPickUp;
        throwAction.started += OnThrowStart; // New throw action started
        throwAction.canceled += OnThrowEnd;
        cleanAction.started += ctx => OnClean(true);
        cleaningListAction.performed += OnCleaningList;
        displayControlsAction.performed += OnDisplayControls;
        selectFirstToolAction.performed += OnSelectFirstTool;
        selectSecondToolAction.performed += OnSelectSecondTool;
        selectThirdToolAction.performed += OnSelectThirdTool;
        mouseScrollAction.performed += OnMouseScroll;

        moveAction.canceled += OnMove;
        lookAction.canceled += OnLook;
        crouchAction.canceled += OnCrouch;
        rotateObejctAction.canceled += OnRotateObject;
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

    private void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            isCursorVisible = !isCursorVisible;
            if (isCursorVisible)
                ShowCursor();
            else
                HideCursor();
        }
    }

    private void HideCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void ShowCursor()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        Move = context.ReadValue<Vector2>();
    }

    private void OnLook(InputAction.CallbackContext context)
    {
        Look = context.ReadValue<Vector2>();
        IsLookInputMouse = context.control.device is Mouse;
    }

    private void OnCrouch(InputAction.CallbackContext context)
    {
        Crouch = context.ReadValueAsButton();
    }

    private void OnRotateObject(InputAction.CallbackContext context)
    {
        RotateObject = context.ReadValueAsButton();
    }

    private void OnInteract(InputAction.CallbackContext context)
    {
        InteractEvent?.Invoke();
    }

    private void OnPickUp(InputAction.CallbackContext context)
    {
        PickUpEvent?.Invoke();
    }

    private void OnThrowStart(InputAction.CallbackContext context)
    {
        ThrowStartEvent?.Invoke();
    }
    private void OnThrowEnd(InputAction.CallbackContext context)
    {
        ThrowEndEvent?.Invoke();
    }

    private void OnClean(bool isCleaning)
    {
        CleanEvent?.Invoke(isCleaning);
    }

    public bool IsUsingController()
    {
        return Input.GetJoystickNames().Length > 0;
    }

    private void OnCleaningList(InputAction.CallbackContext context)
    {
        CleaningListEvent?.Invoke();
    }

    private void OnDisplayControls(InputAction.CallbackContext context)
    {
        DisplayControlsEvent?.Invoke();
    }

    private void OnSelectFirstTool(InputAction.CallbackContext context)
    {
        SelectFirstToolEvent?.Invoke();
    }

    private void OnSelectSecondTool(InputAction.CallbackContext context)
    {
        SelectSecondToolEvent?.Invoke();
    }

    private void OnSelectThirdTool(InputAction.CallbackContext context)
    {
        SelectThirdToolEvent?.Invoke();
    }

    private void OnMouseScroll(InputAction.CallbackContext context)
    {
        Scroll = context.ReadValue<float>();
    }
}