using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    [SerializeField] private bool shouldHideCursor;

    private PlayerInput playerInput;

    private InputActionMap gameplayMap;
    private InputActionMap pauseMap;

    public Vector2 Move { get; private set; }
    public Vector2 Look { get; private set; }
    public bool IsLookInputMouse { get; private set; }
    public bool Crouch { get; private set; }
    public bool RotateObject { get; private set; }
    public float Scroll { get; private set; }

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
    private InputAction pauseAction;
    private InputAction tutorialUIAction;

    public event Action PickUpEvent;
    public event Action InteractEvent;
    public event Action CleaningListEvent;
    public event Action DisplayControlsEvent;
    public event Action DisplayTutorialEvent;
    public event Action SelectFirstToolEvent;
    public event Action SelectSecondToolEvent;
    public event Action SelectThirdToolEvent;
    public event Action<bool> CleanEvent;
    public event Action ThrowStartEvent;
    public event Action ThrowEndEvent;
    public event Action PauseEvent;

    private bool isCursorVisible = true;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();

        gameplayMap = playerInput.actions.FindActionMap("Player");
        pauseMap = playerInput.actions.FindActionMap("Pause");

        moveAction = gameplayMap.FindAction("Move");
        lookAction = gameplayMap.FindAction("Look");
        interactAction = gameplayMap.FindAction("Interact");
        cleanAction = gameplayMap.FindAction("Clean");
        crouchAction = gameplayMap.FindAction("Crouch");
        rotateObejctAction = gameplayMap.FindAction("RotateObejct");
        pickUpAction = gameplayMap.FindAction("PickUp");
        throwAction = gameplayMap.FindAction("Throw");
        cleaningListAction = gameplayMap.FindAction("CleaningList");
        displayControlsAction = gameplayMap.FindAction("DisplayControls");
        selectFirstToolAction = gameplayMap.FindAction("SelectFirstTool");
        selectSecondToolAction = gameplayMap.FindAction("SelectSecondTool");
        selectThirdToolAction = gameplayMap.FindAction("SelectThirdTool");
        mouseScrollAction = gameplayMap.FindAction("MouseScroll");
        pauseAction = playerInput.actions.FindAction("Pause");
        tutorialUIAction = playerInput.actions.FindAction("Tutorial");


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
        pauseAction.performed += OnPause;
        tutorialUIAction.performed += OnDisplayTutorial;

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
        gameplayMap.Enable();
        pauseMap.Enable();
    }

    private void OnDisable()
    {
        gameplayMap.Disable();
        pauseMap.Disable();
    }

    public void HideCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void ShowCursor()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
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

    private void OnPause(InputAction.CallbackContext context)
    {
        PauseEvent?.Invoke();
    }
    
    private void OnDisplayTutorial(InputAction.CallbackContext context)
    {
        DisplayTutorialEvent?.Invoke();
    }

    public void ToggleGameplayMap(bool active)
    {
        if (active)
            gameplayMap.Enable();
        else
            gameplayMap.Disable();
    }
}