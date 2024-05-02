using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Threading;

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

    private byte[][] _brushPixels;

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
        //_brushPixels = new byte[256][];
        //
        //for (int i = 0; i < _brushPixels.Length; i++)
        //{
        //    _brushPixels[i] = new byte[256];
        //}
        //
        //
        //for (int i = 0; i < _brush.width; i++)
        //{
        //    for (int j = 0; j < _brush.height; j++)
        //    {
        //        _brushPixels[i][j] = (byte)_brush.GetPixel(i,j).grayscale;
        //    }
        //}

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

    //private void CleanSurface()
    //{
    //    Vector3 mousePosition = Mouse.current.position.ReadValue();
    //    if (Physics.Raycast(_camera.ScreenPointToRay(mousePosition), out RaycastHit hit, raycastDistance))
    //    {
    //        if (hit.transform != gameObject.transform)
    //        {
    //            return;
    //        }
    //
    //        Vector2 textureCoord = hit.textureCoord;
    //        int pixelX = (int)(textureCoord.x * _templateDirtMask.width);
    //        int pixelY = (int)(textureCoord.y * _templateDirtMask.height);
    //
    //        // Define chunk size
    //        int chunkSizeX = _brush.width / System.Environment.ProcessorCount;
    //        int chunkSizeY = _brush.height / System.Environment.ProcessorCount;
    //
    //        // Clean chunks using ThreadPool
    //        for (int i = 0; i < System.Environment.ProcessorCount; i++)
    //        {
    //            int startX = i * chunkSizeX;
    //            int endX = Mathf.Min((i + 1) * chunkSizeX, _brush.width);
    //            int startY = 0;
    //            int endY = _brush.height;
    //
    //            ThreadPool.QueueUserWorkItem(state => CleanChunk(startX, endX, startY, endY, pixelX, pixelY));
    //        }
    //
    //        // Wait for all threads to finish (optional)
    //        // Thread.Sleep(1000); // Sleep for 1 second (or any other desired delay)
    //        // _templateDirtMask.Apply(); // Apply changes after threads complete (if necessary)
    //    }
    //}
    //
    //private void CleanChunk(int startX, int endX, int startY, int endY, int pixelX, int pixelY)
    //{
    //    for (int x = startX; x < endX; x++)
    //    {
    //        for (int y = startY; y < endY; y++)
    //        {
    //            Color pixelDirt = _brush.GetPixel(x, y);
    //            Color pixelDirtMask = _templateDirtMask.GetPixel(pixelX + x, pixelY + y);
    //
    //            if (pixelX + x < _templateDirtMask.width && pixelX + x > 0 &&
    //                pixelY + y < _templateDirtMask.height && pixelY + y > 0)
    //            {
    //                _templateDirtMask.SetPixel(pixelX + x, pixelY + y, new Color(0, pixelDirtMask.g * pixelDirt.g, 0));
    //            }
    //        }
    //    }
    //    _templateDirtMask.Apply();
    //}

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