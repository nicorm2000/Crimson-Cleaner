using UnityEngine;
using UnityEngine.InputSystem;

public class Openable : MonoBehaviour
{
    [Header("Openable Config")]
    [SerializeField] private InputManager inputManager;
    [SerializeField] private LayerMask openableRaycastLayerMask = ~0;

    private Animator _openableAnimator;
    private bool _isOpen = false;

    private readonly string _openableOpen = "Open";

    private void OnEnable()
    {
        inputManager.OpenEvent += ToggleObjectState;
    }

    private void OnDisable()
    {
        inputManager.OpenEvent -= ToggleObjectState;
    }

    private void Start()
    {
        _openableAnimator = GetComponent<Animator>();
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
        if (Physics.Raycast(ray, out hit, 1000f, openableRaycastLayerMask))
        {
            Debug.DrawRay(ray.origin, ray.direction * hit.distance, Color.green, 2f);
            return hit.transform.parent == transform;
        }
        else
        {
            Debug.DrawRay(ray.origin, ray.direction * 1000, Color.red, 2f);
        }
        return false;
    }
}