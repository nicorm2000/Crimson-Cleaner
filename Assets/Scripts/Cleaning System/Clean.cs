using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Clean : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private CleaningManager cleaningManager;
    [SerializeField] private Material cleanMaterial;
    [SerializeField] private float raycastDistance;

    private bool _isCleaning = false;

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
    }

    private IEnumerator CleaningCoroutine()
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
                    yield return new WaitForSeconds(1.0f);
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
                    yield return new WaitForSeconds(1.0f);
                    if (_isCleaning)
                    {
                        Debug.Log("Cleaning...");
                        _alphaPercentage = cleaningManager.CleaningPercentages[3];
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

    //private void CleanObject()
    //{
    //    Debug.Log("Cleaning.");
    //    _alphaPercentage = cleaningManager.CleaningPercentages[1];
    //
    //    switch (_alphaPercentage)
    //    {
    //        case 0.66f:
    //            Debug.Log("Updating Alpha 1");
    //            _alphaPercentage = cleaningManager.CleaningPercentages[1];
    //            UpdateAlpha(_alphaPercentage);
    //            if (_isCleaning)
    //            {
    //                Debug.Log("Cleaning..");
    //                _alphaPercentage = cleaningManager.CleaningPercentages[2];
    //            }
    //            break;
    //        case 0.33f:
    //            Debug.Log("Updating Alpha 2");
    //            _alphaPercentage = cleaningManager.CleaningPercentages[2];
    //            UpdateAlpha(_alphaPercentage);
    //            if (_isCleaning)
    //            {
    //                Debug.Log("Cleaning...");
    //                _alphaPercentage = cleaningManager.CleaningPercentages[3];
    //            }
    //            break;
    //        case 0:
    //            Debug.Log("Updating Alpha 3");
    //            Debug.Log("Cleaned");
    //            StopCleaning();
    //            Destroy(gameObject);
    //            break;
    //        default:
    //            StopCleaning();
    //            break;
    //    }
    //}

    private void CleanSurface()
    {
        Vector3 mousePosition = Mouse.current.position.ReadValue();

        if (Physics.Raycast(cleaningManager.GetCamera().ScreenPointToRay(mousePosition), out RaycastHit hit, raycastDistance))
        {
            if (hit.transform != gameObject.transform)
            {
                return;
            }

            _coroutine ??= StartCoroutine(CleaningCoroutine());
            //CleanObject();
        }
    }

    private void UpdateAlpha(float alphaPercentage)
    {
        Color color = cleanMaterial.color;
        color.a = alphaPercentage;
        cleanMaterial.color = color;
    }
}