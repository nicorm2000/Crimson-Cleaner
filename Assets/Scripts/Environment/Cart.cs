using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Cart : MonoBehaviour
{
    [Header("Openable Config")]
    [SerializeField] private InputManager inputManager;
    [SerializeField] private Collider coll;
    [SerializeField] private Collider collDestruct;
    [SerializeField] private LayerMask openableRaycastLayerMask = ~0;
    [SerializeField] private float raycastDistance = 3f;

    private Animator _openableAnimator;
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

    private void Start()
    {
        _openableAnimator = GetComponentInParent<Animator>();
        _isOpen = false;
    }

    private void ToggleObjectState()
    {
        if (IsMouseLookingAtObject())
        {
            _isOpen = !_isOpen;
            _openableAnimator.SetBool(_openableOpen, _isOpen);
            Debug.Log((_isOpen ? "Open" : "Close") + " Object: " + name);
            
            Invoke(nameof(EmptyMethod), 0.25f);
        }
    }

    private bool IsMouseLookingAtObject()
    {
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (Physics.Raycast(ray, out RaycastHit hit, raycastDistance, openableRaycastLayerMask))
        {
            Debug.DrawRay(ray.origin, ray.direction * hit.distance, Color.green, 2f);
            return hit.transform == transform;
        }
        else
        {
            Debug.DrawRay(ray.origin, ray.direction * 1000, Color.red, 2f);
        }
        return false;
    }

    private void EmptyMethod()
    {
        ColliderState(!_isOpen, coll);
        //ColliderState(_isOpen, collDestruct);
    }

    private void ColliderState(bool state, Collider collider)
    {
        collider.enabled = state; 
    }
}
