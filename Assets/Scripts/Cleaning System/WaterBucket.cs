using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class WaterBucket : Interactable, ICleanable
{
    [Header("Config")]
    [SerializeField] private CleaningManager cleaningManager;
    [SerializeField] private ParticleSystem washParticles;
    [SerializeField] private Material[] bucketMaterials;

    [Header("Water Config")]
    [SerializeField] private GameObject water;
    [SerializeField] private Transform waterStartPoint;
    [SerializeField] private Transform waterFinishPoint;

    public Sprite InteractMessage => CleaningManager.Instance.GetWashMessage();

    public bool canModifySanity = true;
    public bool HasWater { get; private set; }
    public float WaterPercentage { get; private set; }

    public float forwardForce = 500f;

    public event Action Cleaned;
    public event Action WaterBucketUnavailable;

    private Renderer _renderer;
    private Renderer _rendererWater;
    private int _bucketDirtState;

    private void Awake()
    {
        _bucketDirtState = 0;
        _renderer = GetComponent<Renderer>();
        if (water != null)
            _rendererWater = water.GetComponent<Renderer>();
        UpdateMaterial(_bucketDirtState, _renderer);
        if (water != null)
            UpdateMaterial(_bucketDirtState, _rendererWater);
        HasWater = true;
        WaterPercentage = 1;
    }

    private void Update()
    {
        if (_bucketDirtState >= 1 && !HasWater && canModifySanity)
        {
            SanityManager.Instance.ModifySanityScalar(SanityManager.Instance.DropBucketScaler);
            canModifySanity = false;
        }
    }

    public void Interact(PlayerController playerController)
    {
        CleanTool();
    }

    protected override void PerformInteraction(PlayerController playerController)
    {
        Interact(playerController);
    }

    private void CleanTool()
    {
        Vector3 mousePosition = Mouse.current.position.ReadValue();
        Ray ray = cleaningManager.GetCamera().ScreenPointToRay(mousePosition);

        int currentToolIndex = cleaningManager.GetToolSelector().CurrentToolIndex;
        int dirtyPercentage = cleaningManager.GetToolSelector().GetDirtyPercentage(currentToolIndex);

        if (cleaningManager.GetToolSelector().CurrentToolIndex != cleaningManager.GetMop() && cleaningManager.GetToolSelector().CurrentToolIndex != cleaningManager.GetSponge())
        {
            WaterBucketUnavailable?.Invoke();
            return;
        }

        if (_bucketDirtState >= 4) return;

        if (dirtyPercentage == 0) return;

        if (!HasWater) return;

        if (Physics.Raycast(ray, out RaycastHit hit, cleaningManager.GetInteractionDistance()))
        {
            if (hit.transform != gameObject.transform) return;

            if (_bucketDirtState < 4)
            {
                _bucketDirtState++;
                UpdateMaterial(_bucketDirtState, _renderer);
                if (water != null)
                    UpdateMaterial(_bucketDirtState, _rendererWater);
            }
            if (cleaningManager.GetToolSelector().CurrentToolIndex == cleaningManager.GetMop())
                audioManager.PlaySound(soundEvent, gameObject);
            else if (cleaningManager.GetToolSelector().CurrentToolIndex == cleaningManager.GetSponge())
                audioManager.PlaySound(soundEvent2, gameObject);
            ActivateWashing();
            cleaningManager.GetToolSelector().ResetDirtyPercentage(currentToolIndex);
            cleaningManager.GetToolSelector().toolAnimatorController.TriggerParticularAction(cleaningManager.GetToolSelector().toolAnimatorController.GetBucketName());

            if (currentToolIndex == cleaningManager.GetMop())
            {
                cleaningManager.GetToolSelector().ChangeToolMaterial(cleaningManager.GetToolSelector().mopObject,currentToolIndex, cleaningManager.GetToolSelector().GetOriginalMaterial());
            }
            else if (currentToolIndex == cleaningManager.GetSponge())
            {
                cleaningManager.GetToolSelector().ChangeToolMaterial(cleaningManager.GetToolSelector().spongeObject, currentToolIndex, cleaningManager.GetToolSelector().GetOriginalMaterial());
            }


            SanityManager.Instance.ModifySanityScalar(SanityManager.Instance.WashToolScaler / 2f);
            if (cleaningManager.GetToolSelector().CurrentToolIndex == cleaningManager.GetMop())
                cleaningManager.GetMopDrippingParticles().Play();
            else if (cleaningManager.GetToolSelector().CurrentToolIndex == cleaningManager.GetSponge())
                cleaningManager.GetSpongeDrippingParticles().Play();
            SanityManager.Instance.ModifySanityScalar(SanityManager.Instance.WashToolScaler);

            if (SanityManager.Instance.isRageActive)
            {
                TriggerBucketEyection();
                return;
            }
        }
    }

    public void ActivateWashing() => washParticles.Play();

    public bool GetWaterState() => HasWater;

    public void SetWaterState(bool state) => HasWater = state;
    public float GetWaterPercentage() => WaterPercentage;

    public void SetWaterPercentage(float value) => WaterPercentage = value;

    public void WaterPercentageLerp(float value)
    {
        water.transform.position = Vector3.Lerp(waterStartPoint.position, waterFinishPoint.position, value);
    }

    public void WaterPercentageHandler(float value = 1)
    {
        SetWaterPercentage(GetWaterPercentage() + (Time.deltaTime / value));
        WaterPercentageLerp(GetWaterPercentage());
    }

    private void UpdateMaterial(int materialIndex, Renderer renderer)
    {
        if (materialIndex >= 0 && materialIndex < bucketMaterials.Length)
        {
            renderer.material = bucketMaterials[materialIndex];
        }
    }

    public void SetBucketMaterialDefault()
    {
        _bucketDirtState = 0;
        UpdateMaterial(_bucketDirtState, _renderer);
        if (water != null)
            UpdateMaterial(_bucketDirtState, _rendererWater);
    }

    private void TriggerBucketEyection()
    {
        Rigidbody bucketRigidbody = GetComponent<Rigidbody>();

        Vector3 forwardDirection = cleaningManager.GetToolSelector().gameObject.transform.forward;
        forwardDirection.y = 0;

        forwardDirection.Normalize();

        bucketRigidbody.AddForce(forwardDirection * forwardForce, ForceMode.Impulse);
    }
}
