using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Clean : MonoBehaviour, ICleanable
{
    [Header("Config")]
    [SerializeField] private CleaningManager cleaningManager;
    [SerializeField] private Material[] cleaningMaterials;

    private Renderer _renderer;

    private bool _isCleaning = false;
    private float _cleaningInterval = 1.0f;
    private int _currentMaterialIndex = 0;
    private float _currentUIIndex = 1.0f;

    private Coroutine _coroutine = null;

    public event Action Cleaned;
    public event Action<GameObject> CleanedGO;
    public bool IsCleaned => isCleaned;

    private Sprite cleanMessage;
    public Sprite CleanMessage => cleanMessage;

    private bool isCleaned = false;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
    }

    private void OnEnable()
    {
        cleaningManager.GetInputManager().CleanEvent += HandleCleanEvent;
    }

    private void OnDisable()
    {
        cleaningManager.GetInputManager().CleanEvent -= HandleCleanEvent;
        StopCleaning();
    }

    private void OnDestroy()
    {
        cleaningManager.GetInputManager().CleanEvent -= HandleCleanEvent;
        StopCleaning();
    }

    private void Update()
    {
        if (_isCleaning && !isCleaned)
        {
            CleanSurface();
        }
    }

    private void HandleCleanEvent(bool startCleaning)
    {
        if (startCleaning)
        {
            StartCleaning();
            cleaningManager.GetPlayerAnimator().SetBool("Cleaning", true);
        }
        else
        {
            StopCleaning();
            cleaningManager.GetPlayerAnimator().SetBool("Cleaning", false);
        }
    }

    private void StartCleaning()
    {
        _isCleaning = true;
    }

    private void StopCleaning()
    {
        _isCleaning = false;
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
            _coroutine = null;
        }
    }

    private IEnumerator CleaningCoroutine(int toolIndex)
    {
        Debug.Log("Cleaning.");
        while (_isCleaning)
        {
            if (!CanContinueCleaning())
            {
                StopCleaning();
                yield break;
            }

            UpdateMaterialAndDirtyPercentage(toolIndex);

            switch (_currentMaterialIndex)
            {
                case 0:
                    _currentMaterialIndex = 1;
                    _currentUIIndex = 0.75f;
                    break;
                case 1:
                    _currentMaterialIndex = 2;
                    _currentUIIndex = 0.5f;
                    break;
                case 2:
                    _currentMaterialIndex = 3;
                    _currentUIIndex = 0.25f;
                    break;
                case 3:
                    FinishCleaning();
                    yield break;
            }

            yield return new WaitForSeconds(_cleaningInterval);

            if (!CanContinueCleaning())
            {
                StopCleaning();
                yield break;
            }
        }
    }

    private bool CanContinueCleaning()
    {
        return cleaningManager.GetToolSelector().GetDirtyPercentage(cleaningManager.GetToolSelector().CurrentToolIndex) < cleaningManager.DirtyMaxValue;
    }

    private void UpdateMaterialAndDirtyPercentage(int toolIndex)
    {
        Debug.Log("Updating Material");
        UpdateMaterial(_currentMaterialIndex);
        cleaningManager.GetToolSelector().IncrementDirtyPercentage(toolIndex, cleaningManager.DirtyIncrementAmount);
    }

    private void FinishCleaning()
    {
        Debug.Log("Cleaned");
        _currentUIIndex = 0.0f;
        isCleaned = true;
        Cleaned?.Invoke();
        CleanedGO?.Invoke(gameObject);
        StopCleaning();
    }

    private void CleanSurface()
    {
        Vector3 mousePosition = Mouse.current.position.ReadValue();
        Ray ray = cleaningManager.GetCamera().ScreenPointToRay(mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, cleaningManager.GetInteractionDistance()))
        {
            if (hit.transform != gameObject.transform)
            {
                return;
            }

            int currentToolIndex = cleaningManager.GetToolSelector().CurrentToolIndex;

            if (cleaningManager.GetToolSelector().GetDirtyPercentage(currentToolIndex) < cleaningManager.DirtyMaxValue)
            {
                LayerMask mopLayer = cleaningManager.GetMopLayerMask();
                LayerMask spongeLayer = cleaningManager.GetSpongeLayerMask();

                if (currentToolIndex == 0 && (mopLayer.value & (1 << hit.transform.gameObject.layer)) != 0) // Mop
                {
                    _coroutine ??= StartCoroutine(CleaningCoroutine(currentToolIndex));
                }
                else if (currentToolIndex == 1 && (spongeLayer.value & (1 << hit.transform.gameObject.layer)) != 0) // Sponge
                {
                    _coroutine ??= StartCoroutine(CleaningCoroutine(currentToolIndex));
                }
                else
                {
                    Debug.Log("Tool is too dirty to clean or wrong tool for this surface!");
                }
            }
        }
    }

    private void UpdateMaterial(int materialIndex)
    {
        if (materialIndex >= 0 && materialIndex < cleaningMaterials.Length)
        {
            _renderer.material = cleaningMaterials[materialIndex];
        }
    }

    public int GetMaterialIndex()
    {
        return _currentMaterialIndex;
    }

    public float GetCleanUIIndex()
    {
        return _currentUIIndex;
    }
}