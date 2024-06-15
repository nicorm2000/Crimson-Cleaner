using UnityEngine;
using UnityEngine.InputSystem;

public class Openable : MonoBehaviour, IOpenable
{
    [Header("Openable Config")]
    [SerializeField] private InputManager inputManager;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private float raycastDistance = 3f;
    [SerializeField] private LayerMask interactableLayerMask = ~0;
    [SerializeField] private Animator _openableAnimator;

    [Header("Audio Config")]
    [SerializeField] private AudioManager audioManager = null;
    [SerializeField] private string doorOpenCloseEvents = null;

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
         _isOpen = false;
    }

    private void ToggleObjectState()
    {
        if (IsMouseLookingAtObject() && playerController.GetObjectGrabbable() == null)
        {
            _isOpen = !_isOpen;
            _openableAnimator.SetBool(_openableOpen, _isOpen);
            Debug.Log((_isOpen ? "Open" : "Close") + " Object: " + name);
        }
    }

    private bool IsMouseLookingAtObject()
    {
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());

        if (Physics.Raycast(ray, out RaycastHit hit, raycastDistance, interactableLayerMask))
        {
            if (hit.transform != gameObject.transform)
            {
                return false;
            }
            audioManager.PlaySound(doorOpenCloseEvents);
            return hit.collider.gameObject.GetComponent<Openable>() && hit.transform == transform;
        }

        return false;
    }
}