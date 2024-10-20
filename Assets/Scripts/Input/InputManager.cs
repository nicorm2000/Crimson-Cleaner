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

    public bool RotateObject { get; private set; }

    public float Scroll { get; private set; }

    private InputAction moveAction;
    private InputAction lookAction;
    private InputAction interactAction;
    private InputAction cleanAction;
    private InputAction rotateObejctAction;
    private InputAction pickUpAction;
    private InputAction retrieveAction;
    private InputAction throwAction;
    private InputAction cleaningListAction;
    private InputAction mouseScrollAction;
    private InputAction pauseAction;
    private InputAction tutorialUIAction;
    private InputAction toogleToolWheelAction;
    private InputAction DispatchBagAction;
    private InputAction StoreObjectAction;
    private InputAction firstToolAction;
    private InputAction secondToolAction;
    private InputAction thirdToolAction;
    private InputAction forthToolAction;
    private InputAction fifthToolAction;

    public event Action PickUpEvent;
    public event Action InteractEvent;
    public event Action DisplayTutorialEvent;
    public event Action SelectFirstToolEvent;
    public event Action SelectSecondToolEvent;
    public event Action SelectThirdToolEvent;
    public event Action<bool> CleanEvent;
    public event Action<bool> RetrieveEvent;
    public event Action ThrowStartEvent;
    public event Action ThrowEndEvent;
    public event Action PauseEvent;
    public event Action ToggleToolWheelStartEvent;
    public event Action ToggleToolWheelEndEvent;
    public event Action StoreObjectEvent;
    public event Action DispatchBagEvent;
    public event Action FirstToolEvent;
    public event Action SecondToolEvent;
    public event Action ThirdToolEvent;
    public event Action ForthToolEvent;
    public event Action FifthToolEvent;

    private bool isCursorVisible = true;
    private bool isCleaning = false;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();

        gameplayMap = playerInput.actions.FindActionMap("Player");
        pauseMap = playerInput.actions.FindActionMap("Pause");

        moveAction = gameplayMap.FindAction("Move");
        lookAction = gameplayMap.FindAction("Look");
        interactAction = gameplayMap.FindAction("Interact");
        cleanAction = gameplayMap.FindAction("Clean");
        retrieveAction = gameplayMap.FindAction("Retrieve");
        pickUpAction = gameplayMap.FindAction("PickUp");
        throwAction = gameplayMap.FindAction("Throw");
        cleaningListAction = gameplayMap.FindAction("CleaningList");
        mouseScrollAction = gameplayMap.FindAction("MouseScroll");
        rotateObejctAction = gameplayMap.FindAction("RotateObejct");
        pauseAction = playerInput.actions.FindAction("Pause");
        tutorialUIAction = playerInput.actions.FindAction("Tutorial");
        toogleToolWheelAction = playerInput.actions.FindAction("ToolWheel");
        DispatchBagAction = playerInput.actions.FindAction("DispatchBag");
        StoreObjectAction = playerInput.actions.FindAction("StoreObject");
        firstToolAction = playerInput.actions.FindAction("FirstTool");
        secondToolAction = playerInput.actions.FindAction("SecondTool");
        thirdToolAction = playerInput.actions.FindAction("ThirdTool");
        forthToolAction = playerInput.actions.FindAction("ForthTool");
        fifthToolAction = playerInput.actions.FindAction("FifthTool");

        moveAction.performed += OnMove;
        lookAction.performed += OnLook;
        rotateObejctAction.performed += OnRotateObject;
        interactAction.performed += OnInteract;
        pickUpAction.performed += OnPickUp;
        throwAction.started += OnThrowStart; // New throw action started
        throwAction.canceled += OnThrowEnd;
        cleanAction.started += ctx => OnClean(true);
        cleanAction.canceled += ctx => OnClean(false);
        retrieveAction.started += ctx => OnRetrieve(true);
        retrieveAction.canceled += ctx => OnRetrieve(false);
        mouseScrollAction.performed += OnMouseScroll;
        pauseAction.performed += OnPause;
        tutorialUIAction.performed += OnDisplayTutorial;
        DispatchBagAction.performed += OnDispatchBag;
        toogleToolWheelAction.started += OnToggleToolWheelStart;
        toogleToolWheelAction.canceled += OnToggleToolWheelEnd;
        StoreObjectAction.performed += OnStoreObject;
        firstToolAction.performed += OnFirstToolSelected;
        secondToolAction.performed += OnSecondToolSelected;
        thirdToolAction.performed += OnThirdToolSelected;
        forthToolAction.performed += OnForthToolSelected;
        fifthToolAction.performed += OnFifthToolSelected;

        moveAction.canceled += OnMove;
        lookAction.canceled += OnLook;
        rotateObejctAction.canceled += OnRotateObject;
        //lightSwitchAction.canceled += OnToggleLights;
        //openAction.canceled += OnOpen;
        cleanAction.canceled += ctx => OnClean(false);
        //retrieveAction.canceled += ctx => OnRetrieve(false);
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

    private void OnRetrieve(bool isRetrieving)
    {
        RetrieveEvent?.Invoke(isRetrieving);
    }

    public bool IsUsingController()
    {
        return Input.GetJoystickNames().Length > 0;
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
    
    private void OnDispatchBag(InputAction.CallbackContext context)
    {
        DispatchBagEvent?.Invoke();
    }
    
    private void OnStoreObject(InputAction.CallbackContext context)
    {
        StoreObjectEvent?.Invoke();
    }
    private void OnFirstToolSelected(InputAction.CallbackContext context)
    {
        FirstToolEvent?.Invoke();
    }
    private void OnSecondToolSelected(InputAction.CallbackContext context)
    {
        SecondToolEvent?.Invoke();
    }
    private void OnThirdToolSelected(InputAction.CallbackContext context)
    {
        ThirdToolEvent?.Invoke();
    }
    private void OnForthToolSelected(InputAction.CallbackContext context)
    {
        ForthToolEvent?.Invoke();
    }
    private void OnFifthToolSelected(InputAction.CallbackContext context)
    {
        FifthToolEvent?.Invoke();
    }

    public void ToggleGameplayMap(bool active)
    {
        if (active)
            gameplayMap.Enable();
        else
            gameplayMap.Disable();
    }

    private void OnToggleToolWheelStart(InputAction.CallbackContext context)
    {
        ToggleToolWheelStartEvent?.Invoke();
    }

    private void OnToggleToolWheelEnd(InputAction.CallbackContext context)
    {
        ToggleToolWheelEndEvent?.Invoke();
    }
}