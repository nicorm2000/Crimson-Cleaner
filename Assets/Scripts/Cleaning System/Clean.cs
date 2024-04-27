using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Clean : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private Animator animator;

    [SerializeField] private Texture2D _dirtMaskBase;
    [SerializeField] private Texture2D _brush;

    [SerializeField] private Material _material;

    [SerializeField] private InputManager inputManager;
    [SerializeField] private float raycastDistance;

    private Texture2D _templateDirtMask;

    private bool isCleaning = false;

    private void OnEnable()
    {
        inputManager.CleanEvent += HandleCleanEvent;
    }

    private void OnDisable()
    {
        inputManager.CleanEvent -= HandleCleanEvent;
        StopCleaning();
    }

    private void Start()
    {
        CreateTexture();
    }

    private void Update()
    {
        if (isCleaning)
        {
            CleanSurface();
        }
    }

    private void CreateTexture()
    {
        _templateDirtMask = new Texture2D(_dirtMaskBase.width, _dirtMaskBase.height);
        _templateDirtMask.SetPixels(_dirtMaskBase.GetPixels());
        _templateDirtMask.Apply();

        _material.SetTexture("DirtMask", _templateDirtMask);
    }

    private void HandleCleanEvent(bool startCleaning)
    {
        if (startCleaning)
        {
            StartCleaning();
            animator.SetBool("Cleaning", true);
        }
        else
        {
            StopCleaning();
            animator.SetBool("Cleaning", false);
        }
    }

    private void StartCleaning()
    {
        isCleaning = true;
    }

    private void StopCleaning()
    {
        isCleaning = false;
    }

    private void CleanSurface()
    {
        Vector3 mousePosition = Mouse.current.position.ReadValue();
        if (Physics.Raycast(_camera.ScreenPointToRay(mousePosition), out RaycastHit hit, raycastDistance))
        {
            if (hit.transform != gameObject.transform)
            {
                return;
            }

            Vector2 textureCoord = hit.textureCoord;
            int pixelX = (int)(textureCoord.x * _templateDirtMask.width);
            int pixelY = (int)(textureCoord.y * _templateDirtMask.height);


            for (int x = 0; x < _brush.width; x++)
            {
                for (int y = 0; y < _brush.height; y++)
                {
                    Color pixelDirt = _brush.GetPixel(x, y);
                    Color pixelDirtMask = _templateDirtMask.GetPixel(pixelX + x, pixelY + y);

                    if (pixelX + x < _templateDirtMask.width && pixelX + x > 0 &&
                        pixelY + y < _templateDirtMask.height && pixelY + y > 0)
                    {
                        _templateDirtMask.SetPixel(pixelX + x, pixelY + y, new Color(0, pixelDirtMask.g * pixelDirt.g, 0));
                    }
                }
            }
            _templateDirtMask.Apply();
        }
    }
}