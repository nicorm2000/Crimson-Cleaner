using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public InputMaster inputMaster;

    public static event Action<Vector2> LookEvent;
    public static event Action<Vector2> MoveEvent;

    private Vector2 lookInput;

    private void Awake()
    {
        inputMaster = new InputMaster();
    }

    private void OnEnable()
    {
        inputMaster.Enable();
    }

    private void OnDisable()
    {
        inputMaster.Disable();
    }

    private void Update()
    {
        if (lookInput.magnitude > 0)
            LookEvent?.Invoke(lookInput);
    }

    public void OnMove(InputValue context)
    {
        var movementInput = context.Get<Vector2>();

        MoveEvent?.Invoke(movementInput);
    }

    public void OnLookMouse(InputValue context)
    {
        var lookInput = context.Get<Vector2>();
        LookEvent?.Invoke(lookInput);
    }

    public void OnLookController(InputValue context)
    {
        lookInput = context.Get<Vector2>();
    }


}
