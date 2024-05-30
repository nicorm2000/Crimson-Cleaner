using UnityEngine;
using UnityEngine.InputSystem;

public class Openable : MonoBehaviour, IOpenable
{
    [Header("Openable Config")]
    [SerializeField] private InputManager inputManager;
    [SerializeField] private float raycastDistance = 3f;
    [SerializeField] private LayerMask interactableLayerMask = ~0;
    [SerializeField] private Animator _openableAnimator;

    public bool _isOpen { get; private set; }

    public string OpenCloseMessage => _isOpen ? "Press F to close" : "Press F to open";

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
        if (IsMouseLookingAtObject())
        {
            _isOpen = !_isOpen;
            _openableAnimator.SetBool(_openableOpen, _isOpen);
            Debug.Log((_isOpen ? "Open" : "Close") + " Object: " + name);
        }
    }

    private bool IsMouseLookingAtObject()
    {
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
            RaycastHit hit;
        if (Physics.Raycast(ray, out hit, raycastDistance, interactableLayerMask))
        {
            Debug.DrawRay(ray.origin, ray.direction * hit.distance, Color.green, 2f);
            return hit.collider.gameObject.GetComponent<Openable>() && hit.transform == transform;
        }
        else
        {
            Debug.DrawRay(ray.origin, ray.direction * 1000, Color.red, 2f);
        }
        return false;
    }
}