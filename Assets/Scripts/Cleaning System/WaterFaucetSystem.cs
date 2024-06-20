using UnityEngine;
using UnityEngine.InputSystem;

public class WaterFaucetSystem : MonoBehaviour, IToggable
{
    [Header("Config")]
    [SerializeField] private InputManager inputManager;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private WaterBucket waterBucket = null;
    [SerializeField] private LayerMask interactableLayerMask = ~0;
    [SerializeField] private float raycastDistance = 3f;

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
    public Sprite ToggleOnOffMessage => toggleOnOffMessage;

    [Header("Audio Config")]
    [SerializeField] private AudioManager audioManager = null;
    [SerializeField] private string interactFaucetEvent = null;
    [SerializeField] private string waterFlowEvent = null;
    [SerializeField] private string waterFlowStopEvent = null;

    private Animator animator;
    public bool _isOpen { get; private set; }
    private readonly string _openableOpen = "Open";

    private void OnEnable()
    {
        inputManager.InteractEvent += ToggleObjectState;
    }

    private void OnDisable()
    {
        inputManager.InteractEvent -= ToggleObjectState;
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void ToggleObjectState()
    {
        if (IsMouseLookingAtObject() && playerController.GetObjectGrabbable() == null)
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

    private bool IsMouseLookingAtObject()
    {
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (Physics.Raycast(ray, out RaycastHit hit, raycastDistance, interactableLayerMask))
        {
            if (hit.transform != gameObject.transform) return false;

            return hit.collider.gameObject.GetComponent<WaterFaucetSystem>();
        }
        return false;
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
                        audioManager.PlaySound(waterFlowStopEvent);
                        waterParticles.Stop();
                        return false;
                    }

                    Debug.Log(waterBucket.GetWaterPercentage());
                    water.SetActive(true);
                    waterBucket.WaterPercentageHandler(fillWaterSpeed);
                }
            }
        }
        return true;
    }
}