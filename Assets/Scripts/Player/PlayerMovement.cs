using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Playables;

public class PlayerMovement : MonoBehaviour
{
    [Header("SetUp")]
    [SerializeField] private Rigidbody rigidBody;
    [SerializeField] private Transform orientation;

    [Header("Movement")]
    [SerializeField] private float speed;

    private Vector3 moveDir;

    private Vector2 _movementDirection;

    private void OnEnable()
    {
        InputManager.MoveEvent += OnMove;
    }

    private void OnDisable()
    {
        InputManager.MoveEvent -= OnMove;
    }

    private void OnValidate()
    {
        rigidBody ??= GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        moveDir = orientation.forward * _movementDirection.y + orientation.right * _movementDirection.x;

        Vector3 velocity = new Vector3(moveDir.x * speed, rigidBody.velocity.y, moveDir.z * speed);

        rigidBody.velocity = velocity;
    }

    private void OnMove(Vector2 movementInput)
    {
        SetMovementDir(movementInput);
    }

    public void SetMovementDir(Vector2 movementDirection)
    {
        _movementDirection = movementDirection;
    }
}
