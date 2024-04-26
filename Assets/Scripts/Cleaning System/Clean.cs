using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Clean : MonoBehaviour
{
    [SerializeField] private Camera _camera;

    [SerializeField] private Texture2D _dirtMaskBase;
    [SerializeField] private Texture2D _brush;

    [SerializeField] private Material _material;

    [SerializeField] private InputManager inputManager;

    private Texture2D _templateDirtMask;
    private InputAction _cleanAction;
    private Coroutine _cleanCoroutine;

    private void OnDisable()
    {
        _cleanAction.Disable();
    }

    private void Start()
    {
        CreateTexture();

        _cleanAction = inputManager.inputMaster.Player.Clean;
        _cleanAction.performed += ctx => StartCleaning();
        _cleanAction.canceled += ctx => StopCleaning();
        _cleanAction.Enable();
    }

    private void CreateTexture()
    {
        _templateDirtMask = new Texture2D(_dirtMaskBase.width, _dirtMaskBase.height);
        _templateDirtMask.SetPixels(_dirtMaskBase.GetPixels());
        _templateDirtMask.Apply();

        _material.SetTexture("DirtMask", _templateDirtMask);
    }

    private void StartCleaning()
    {
        if (_cleanCoroutine == null)
        {
            _cleanCoroutine = StartCoroutine(CleanSurfaceCoroutine());
        }
    }

    private void StopCleaning()
    {
        if (_cleanCoroutine != null)
        {
            StopCoroutine(_cleanCoroutine);
            _cleanCoroutine = null;
        }
    }

    private IEnumerator CleanSurfaceCoroutine()
    {
        while (true)
        {
            // Perform cleaning logic here
            Vector3 mousePosition = Mouse.current.position.ReadValue();
            if (Physics.Raycast(_camera.ScreenPointToRay(mousePosition), out RaycastHit hit))
            {
                if (hit.transform == gameObject.transform)
                {
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

            // Yield null to wait until the next frame
            yield return null;
        }
    }
}