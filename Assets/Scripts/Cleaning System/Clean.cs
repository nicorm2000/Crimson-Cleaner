using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Clean : MonoBehaviour, ICleanable
{
    [Header("Config")]
    [SerializeField] private CleaningManager cleaningManager;
    [SerializeField] private Material[] cleaningMaterials;

    [Header("Audio Config")]
    [SerializeField] private AudioManager audioManager = null;


    private Renderer _renderer;

    private bool _isCleaning = false;
    private float _cleaningInterval = 1.0f;
    private int _currentMaterialIndex = 0;
    private float _currentUIIndex = 1.0f;

    private Coroutine _coroutine = null;

    public event Action Cleaned;
    public event Action WrongTool;
    public event Action WrongToolMop;
    public event Action WrongToolSponge;
    public event Action<GameObject> CleanedGO;
    public bool IsCleaned => isCleaned;

    private Sprite cleanMessage;
    public Sprite InteractMessage => CleaningManager.Instance.GetInteractMessage();

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
        SetToolAnimator(true);
    }

    private void StopCleaning()
    {
        _isCleaning = false;
        SetToolAnimator(false);
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
            _coroutine = null;
        }
        ResetCurrentCleanableObject();
    }

    private void ResetCurrentCleanableObject()
    {
        int currentToolIndex = cleaningManager.GetToolSelector().CurrentToolIndex;
        if (currentToolIndex == 0)
        {
            cleaningManager.GetMopToolReceiver().SetCurrentCleanableObject(null);
        }
        else if (currentToolIndex == 1)
        {
            cleaningManager.GetSpongeToolReceiver().SetCurrentCleanableObject(null);
        }
    }

    private void SetToolAnimator(bool isCleaning)
    {
        int toolIndex = cleaningManager.GetToolSelector().CurrentToolIndex;
        Animator toolAnimator = null;

        if (toolIndex == 0)
        {
            toolAnimator = cleaningManager.GetMopAnimator();
        }
        else if (toolIndex == 1)
        {
            toolAnimator = cleaningManager.GetSpongeAnimator();
        }

        if (toolAnimator != null)
        {
            toolAnimator.SetBool("Cleaning", isCleaning);
        }
    }

    public void PerformCleaning()
    {
        if (!_isCleaning || isCleaned) return;

        if (!CanContinueCleaning())
        {
            StopCleaning();
            return;
        }

        UpdateMaterialAndDirtyPercentage(cleaningManager.GetToolSelector().CurrentToolIndex);

        if (cleaningManager.GetToolSelector().CurrentToolIndex == 0)
        {
            audioManager.PlaySound(cleaningManager.GetMopEvent());
            cleaningManager.GetMopParticles().Play();
        }
        else if (cleaningManager.GetToolSelector().CurrentToolIndex == 1)
        {
            audioManager.PlaySound(cleaningManager.GetSpongeEvent());
            cleaningManager.GetSpongeParticles().Play();
        }


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
                return;
        }

    }

    private bool CanContinueCleaning()
    {
        return cleaningManager.GetToolSelector().GetDirtyPercentage(cleaningManager.GetToolSelector().CurrentToolIndex) < cleaningManager.DirtyMaxValue;
    }

    private void UpdateMaterialAndDirtyPercentage(int toolIndex)
    {
        UpdateMaterial(_currentMaterialIndex);
        cleaningManager.GetToolSelector().IncrementDirtyPercentage(toolIndex, cleaningManager.DirtyIncrementAmount);
        SanityManager.Instance.ModifySanityScalar(SanityManager.Instance.CleanScaler);
    }

    private void FinishCleaning()
    {
        audioManager.PlaySound(cleaningManager.GetCleanedEvent());
        _currentUIIndex = 0.0f;
        isCleaned = true;
        Cleaned?.Invoke();
        CleanedGO?.Invoke(gameObject);
        StopCleaning();
    }

    public void CleanSurface()
    {
        AssignCleanableObject();
    }

    private void AssignCleanableObject()
    {
        int currentToolIndex = cleaningManager.GetToolSelector().CurrentToolIndex;

        LayerMask mopLayer = cleaningManager.GetMopLayerMask();
        LayerMask spongeLayer = cleaningManager.GetSpongeLayerMask();

        if (currentToolIndex == 0) // Mop
        {
            if ((mopLayer.value & (1 << gameObject.layer)) != 0)
            {
                PerformCleaning();
            }
            else
            {
                WrongToolSponge?.Invoke();
                ResetCurrentCleanableObject();
            }
        }
        else if (currentToolIndex == 1) // Sponge
        {
            if ((spongeLayer.value & (1 << gameObject.layer)) != 0)
            {
                PerformCleaning();
            }
            else
            {
                WrongToolMop?.Invoke();
                ResetCurrentCleanableObject();
            }
        }
        else if (currentToolIndex >= 2)//CHECK
        {
            if ((mopLayer.value & (1 << gameObject.layer)) != 0)
                WrongToolMop?.Invoke();
            else if ((spongeLayer.value & (1 << gameObject.layer)) != 0)
                WrongToolSponge?.Invoke();
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

    public void Interact(PlayerController playerController)
    {
        throw new NotImplementedException();
    }
}