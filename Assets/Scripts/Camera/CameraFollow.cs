using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform playerCameraPosition;

    private void LateUpdate()
    {
        transform.position = playerCameraPosition.position + playerCameraPosition.TransformDirection(Vector3.zero);
    }
}
