using UnityEngine;
using UnityEngine.InputSystem;

public class WaterFaucetSystem : Interactable, IToggable
{
    [Header("Config")]
    [SerializeField] private WaterBucket waterBucket = null;

    [Header("Water Config")]
    [SerializeField] private ParticleSystem waterParticles;
    [SerializeField] private ParticleSystem splashParticles;
    [SerializeField] private Transform rayOrigin;
    [SerializeField] private Vector3 splashOffset;
    [SerializeField] private GameObject water;
    [SerializeField] private float spillThreshold;
    [SerializeField] private float fillWaterSpeed;

    [Header("UI")]
    [SerializeField] private Sprite toggleOnOffMessage;
    public Sprite InteractMessage => toggleOnOffMessage;

    [Header("Audio Config")]
    [SerializeField] private string interactFaucetEvent = null;
    [SerializeField] private string waterFlowEvent = null;
    [SerializeField] private string waterFlowStopEvent = null;

    private Animator animator;
    public bool _isOpen { get; private set; }
    private readonly string _openableOpen = "Open";

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Interact(PlayerController playerController)
    {
        ToggleObjectState(playerController);
    }

    protected override void PerformInteraction(PlayerController playerController)
    {
        Interact(playerController);
    }

    private void ToggleObjectState(PlayerController playerController)
    {
        if (playerController.GetObjectGrabbable() == null)
        {
            _isOpen = !_isOpen;
            audioManager.PlaySound(interactFaucetEvent);
            if (_isOpen)
            {
                audioManager.PlaySound(waterFlowEvent);
                waterParticles.Play();
            }
            else
            {
                audioManager.PlaySound(waterFlowStopEvent);
                waterParticles.Stop();
            }
            animator.SetBool(_openableOpen, _isOpen);
        }
    }

    private void Update()
    {
        IsWaterHittingSomething();
    }

    private bool IsWaterHittingSomething()
    {
        if (!_isOpen)
            return false;
        if (waterBucket.GetWaterState())
            return false;

        if (Physics.Raycast(rayOrigin.position, Vector3.down, out RaycastHit raycastHit, raycastDistance))
        {
            splashParticles.gameObject.transform.position = raycastHit.point + splashOffset;

            if (raycastHit.transform == waterBucket.transform)
            {
                float rotationX = waterBucket.transform.eulerAngles.x;
                float rotationZ = waterBucket.transform.eulerAngles.z;

                rotationX = (rotationX > 180) ? rotationX - 360 : rotationX;
                rotationZ = (rotationZ > 180) ? rotationZ - 360 : rotationZ;

                if (Mathf.Abs(rotationX) < spillThreshold && Mathf.Abs(rotationZ) < spillThreshold)
                {
                    if (waterBucket.GetWaterPercentage() > 1)
                    {
                        waterBucket.SetWaterState(true);
                        _isOpen = false;
                        animator.SetBool(_openableOpen, _isOpen);
                        //Add way to stop water when changing scene
                        audioManager.PlaySound(waterFlowStopEvent);
                        waterParticles.Stop();
                        return false;
                    }
                    waterBucket.SetBucketMaterialDefault();
                    water.SetActive(true);
                    waterBucket.WaterPercentageHandler(fillWaterSpeed);
                }
            }
        }
        return true;
    }
}