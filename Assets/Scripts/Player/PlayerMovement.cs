using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] InputManager inputManager;

    [SerializeField] Rigidbody rb;

    [SerializeField] float speed = 10f;
    [SerializeField] float runSpeed = 15f;

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        Vector3 move = GetMoveVector3() * speed;

        //move *= inputManager.inputMaster.Player.Run.ReadValue<float>() == 0 ? speed : runSpeed;
        rb.velocity = new Vector3(move.x, rb.velocity.y, move.z);
    }

    Vector3 GetMoveVector3()
    {
        Vector2 forward = inputManager.inputMaster.Player.Movement.ReadValue<Vector2>();

        Vector3 move = transform.right * forward.x + transform.forward * forward.y;

        return move;
    }

}
