using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Clean : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private Material cleanMaterial;
    [SerializeField] private float raycastDistance;

    private bool _isCleaning = false;

    private float _alphaPercentage = 1.0f;

    private Coroutine _coroutine = null;

    private void OnEnable()
    {
        CleaningManager.Instance.GetInputManager().CleanEvent += HandleCleanEvent;
    }

    private void OnDisable()
    {
        CleaningManager.Instance.GetInputManager().CleanEvent -= HandleCleanEvent;
        StopCleaning();
    }

    private void OnDestroy()
    {
        CleaningManager.Instance.GetInputManager().CleanEvent -= HandleCleanEvent;
        StopCleaning();
    }

    private void Start()
    {
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
            CleaningManager.Instance.GetPlayerAnimator().SetBool("Cleaning", true);
        }
        else
        {
            StopCleaning();
            CleaningManager.Instance.GetPlayerAnimator().SetBool("Cleaning", false);
        }
    }

    private void StartCleaning()
    {
        _isCleaning = true;
    }

    private void StopCleaning()
    {
        _isCleaning = false;
    }

    private IEnumerator CleaningCoroutine()
    {
        Debug.Log("Cleaning.");
        _alphaPercentage = 0.66f;

        while (_isCleaning) //Evil While WARNING
        {
            switch (_alphaPercentage)
            {
                case 0.66f:
                    Debug.Log("Updating Alpha 1");
                    _alphaPercentage = 0.66f;
                    UpdateAlpha(_alphaPercentage);
                    yield return new WaitForSeconds(1.0f);
                    if (_isCleaning)
                    {
                        Debug.Log("Cleaning..");
                        _alphaPercentage = 0.33f;
                    }
                    break;
                case 0.33f:
                    Debug.Log("Updating Alpha 2");
                    _alphaPercentage = 0.33f;
                    UpdateAlpha(_alphaPercentage);
                    yield return new WaitForSeconds(1.0f);
                    if (_isCleaning)
                    {
                        Debug.Log("Cleaning...");
                        _alphaPercentage = 0.0f;
                    }
                    break;
                case 0:
                    Debug.Log("Updating Alpha 3");
                    Debug.Log("Cleaned");
                    StopCleaning();
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
        
        if (Physics.Raycast(CleaningManager.Instance.GetCamera().ScreenPointToRay(mousePosition), out RaycastHit hit, raycastDistance))
        {
            if (hit.transform != gameObject.transform)
            {
                return;
            }

            _coroutine ??= StartCoroutine(CleaningCoroutine());
        }
    }

    private void UpdateAlpha(float alphaPercentage)
    {
        Color color = cleanMaterial.color;
        color.a = alphaPercentage;
        cleanMaterial.color = color;
    }
}