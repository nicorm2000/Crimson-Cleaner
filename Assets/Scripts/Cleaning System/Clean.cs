using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Clean : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private CleaningManager cleaningManager;
    [SerializeField] private GameObject cleanObject;
    [SerializeField] private Material cleanMaterial;
    [SerializeField] private float raycastDistance;
    [SerializeField] private bool hasReplacement;

    private bool _isCleaning = false;

    private float _cleaningInterval = 1.0f;
    private float _alphaPercentage = 1.0f;

    private Coroutine _coroutine = null;

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

    private void Start()
    {
        if (hasReplacement)
        {
            if (cleanObject != null)
            {
                cleanObject.GetComponent<Collider>().enabled = false;
            }
        }
        UpdateAlpha(_alphaPercentage);
    }

    private void Update()
    {
        if (_isCleaning)
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
        _alphaPercentage = cleaningManager.CleaningPercentages[1];

        while (_isCleaning) //Evil While WARNING
        {
            switch (_alphaPercentage)
            {
                case 0.66f:
                    Debug.Log("Updating Alpha 1");
                    _alphaPercentage = cleaningManager.CleaningPercentages[1];
                    UpdateAlpha(_alphaPercentage);
                    cleaningManager.GetToolSelector().IncrementDirtyPercentage(toolIndex, cleaningManager.DirtyIncrementAmount);
                    yield return new WaitForSeconds(_cleaningInterval);
                    if (_isCleaning)
                    {
                        Debug.Log("Cleaning..");
                        _alphaPercentage = cleaningManager.CleaningPercentages[2];
                    }
                    break;
                case 0.33f:
                    Debug.Log("Updating Alpha 2");
                    _alphaPercentage = cleaningManager.CleaningPercentages[2];
                    UpdateAlpha(_alphaPercentage);
                    cleaningManager.GetToolSelector().IncrementDirtyPercentage(toolIndex, cleaningManager.DirtyIncrementAmount);
                    yield return new WaitForSeconds(_cleaningInterval);
                    if (_isCleaning)
                    {
                        Debug.Log("Cleaning...");
                        _alphaPercentage = cleaningManager.CleaningPercentages[3];
                    }
                    break;
                case 0:
                    cleaningManager.GetToolSelector().IncrementDirtyPercentage(toolIndex, cleaningManager.DirtyIncrementAmount);
                    Debug.Log("Updating Alpha 3");
                    Debug.Log("Cleaned");
                    StopCleaning();
                    if (hasReplacement)
                    {
                        cleanObject.GetComponent<Collider>().enabled = true;
                    }
                    Destroy(gameObject);
                    break;
                default:
                    StopCleaning();
                    break;
            }
        }
    }

    private void CleanSurface()
    {
        Vector3 mousePosition = Mouse.current.position.ReadValue();
        Ray ray = cleaningManager.GetCamera().ScreenPointToRay(mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, raycastDistance))
        {
            if (hit.transform != gameObject.transform)
            {
                return;
            }

            int currentToolIndex = cleaningManager.GetToolSelector().CurrentToolIndex;
            int dirtyPercentage = cleaningManager.GetToolSelector().GetDirtyPercentage(currentToolIndex);

            if (dirtyPercentage < cleaningManager.DirtyMaxValue)
            {
                LayerMask mopLayer = cleaningManager.GetMopLayerMask();
                LayerMask spongeLayer = cleaningManager.GetSpongeLayerMask();

                if (currentToolIndex == 0 && (mopLayer.value & (1 << hit.transform.gameObject.layer)) != 0) // Mop
                {
                    Debug.Log("Mop Cleaning");
                    _coroutine ??= StartCoroutine(CleaningCoroutine(currentToolIndex));
                }
                else if (currentToolIndex == 1 && (spongeLayer.value & (1 << hit.transform.gameObject.layer)) != 0) // Sponge
                {
                    Debug.Log("Sponge Cleaning");
                    _coroutine ??= StartCoroutine(CleaningCoroutine(currentToolIndex));
                }
                else
                {
                    Debug.Log("Tool is too dirty to clean or wrong tool for this surface!");
                }
            }
        }
    }

    private void UpdateAlpha(float alphaPercentage)
    {
        Color color = cleanMaterial.color;
        color.a = alphaPercentage;
        cleanMaterial.color = color;
    }
}