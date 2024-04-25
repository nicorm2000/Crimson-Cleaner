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
        float forward = inputManager.inputMaster.Movement.Forward.ReadValue<float>();
        float right = inputManager.inputMaster.Movement.Right.ReadValue<float>();

        Vector3 move = transform.right * right + transform.forward * forward;

        move *= inputManager.inputMaster.Movement.Run.ReadValue<float>() == 0 ? speed : runSpeed;

        rb.velocity = new Vector3(move.x, rb.velocity.y, move.z);
    }



}
