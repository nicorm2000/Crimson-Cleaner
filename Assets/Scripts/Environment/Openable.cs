using UnityEngine;

public class Openable : MonoBehaviour
{
    [Header("Openable Config")]
    [SerializeField] private KeyCode activationKey = KeyCode.None;

    private Animator _openableAnimator;
    private bool _isOpen = false;

    private readonly string _openableOpen = "Open";

    private void Start()
    {
        _openableAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (!_isOpen && Input.GetKeyDown(activationKey))
        {
            ObjectOpen();
        }
        else if (_isOpen && Input.GetKeyDown(activationKey))
        {
            ObjectClose();
        }
    }

    private void ObjectOpen()
    {
        Debug.Log("Open Object: " + name);
        _isOpen = true;
        _openableAnimator.SetBool(_openableOpen, _isOpen);
    }

    private void ObjectClose()
    {
        Debug.Log("Close Object: " + name);
        _isOpen = false;
        _openableAnimator.SetBool(_openableOpen, _isOpen);
    }
}