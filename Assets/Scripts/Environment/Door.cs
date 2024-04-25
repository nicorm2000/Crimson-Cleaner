using UnityEngine;

public class Door : MonoBehaviour
{
    private Animator _doorAnimator;
    private bool _isOpen = false;

    private readonly string _doorOpen = "Open";

    private void Start()
    {
        _doorAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (!_isOpen && Input.GetKeyDown(KeyCode.O))
        {
            DoorOpen();
        }
        else if (_isOpen && Input.GetKeyDown(KeyCode.O))
        {
            DoorClose();
        }
    }

    private void DoorOpen()
    {
        Debug.Log("Open Door: " + name);
        _isOpen = true;
        _doorAnimator.SetBool(_doorOpen, _isOpen);
    }

    private void DoorClose()
    {
        Debug.Log("Close Door: " + name);
        _isOpen = false;
        _doorAnimator.SetBool(_doorOpen, _isOpen);
    }
}