using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] InputManager inputManager;
    [SerializeField] Rigidbody rb;
    [SerializeField] float speed = 10f;
    [SerializeField] float runSpeed = 15f;

    private InputAction _crouchAction;

    private void OnDisable()
    {
        _crouchAction.Disable();
    }

    private void Start()
    {
        _crouchAction = inputManager.inputMaster.Player.Crouch;
        _crouchAction.started += ctx => StartCrouching();
        _crouchAction.canceled += ctx => StopCrouching();
        _crouchAction.Enable();
    }

    private void StartCrouching()
    {
        transform.localScale = new Vector3(1, 0.5f, 1);
    }

    private void StopCrouching()
    {
        transform.localScale = new Vector3(1, 1f, 1);
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        Vector3 move = GetMoveVector3() * speed;
        rb.velocity = new Vector3(move.x, rb.velocity.y, move.z);
    }

    Vector3 GetMoveVector3()
    {
        Vector2 forward = inputManager.inputMaster.Player.Movement.ReadValue<Vector2>();
        return transform.right * forward.x + transform.forward * forward.y;
    }
}
