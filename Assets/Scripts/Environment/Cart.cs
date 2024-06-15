using UnityEngine;
using UnityEngine.InputSystem;

public class Cart : MonoBehaviour, IOpenable
{
    [Header("Cart Config")]
    [SerializeField] private InputManager inputManager;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private LayerMask interactableLayerMask = ~0;
    [SerializeField] private float raycastDistance = 3f;

    [Header("Audio Config")]
    [SerializeField] private AudioManager audioManager = null;
    [SerializeField] private string openEvent = null;
    [SerializeField] private string closeEvent = null;

    private Animator _openableAnimator;
    public bool _isOpen { get; private set; }

    public string OpenCloseMessage => _isOpen ? "Press F to close" : "Press F to open";

    [SerializeField] private Sprite interactMessage;
    public Sprite InteractMessage => interactMessage;

    private readonly string _openableOpen = "Open";

    private void OnEnable()
    {
        inputManager.InteractEvent += ToggleObjectState;
    }

    private void OnDisable()
    {
        inputManager.InteractEvent -= ToggleObjectState;
    }

    private void Start()
    {
        _openableAnimator = GetComponentInParent<Animator>();
        _isOpen = false;
    }

    private void ToggleObjectState()
    {
        if (IsMouseLookingAtObject() && playerController.GetObjectGrabbable() == null)
        {
            _isOpen = !_isOpen;
            if (_isOpen)
            {
                audioManager.PlaySound(openEvent);
            }
            else
            {
                audioManager.PlaySound(closeEvent);
            }
            _openableAnimator.SetBool(_openableOpen, _isOpen);
        }
    }

    private bool IsMouseLookingAtObject()
    {
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (Physics.Raycast(ray, out RaycastHit hit, raycastDistance, interactableLayerMask))
        {
            if (hit.transform != gameObject.transform) return false;

            return hit.collider.gameObject.GetComponent<Cart>() && hit.transform == transform;
        }
        return false;
    }
}
