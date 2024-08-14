using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ToolsWheelController : MonoBehaviour
{
    [Header("Config")]
    public static int toolID;

    private Animator _animator;
    private bool _toolsWheelSelected = false;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            _toolsWheelSelected = !_toolsWheelSelected;
            Cursor.visible = _toolsWheelSelected;

            if (_toolsWheelSelected)
                Cursor.lockState = CursorLockMode.None;
            else
                Cursor.lockState = CursorLockMode.Confined;
        }

        if (_toolsWheelSelected)
        {
            _animator.SetBool("OpenToolWheel", true);
        }
        else
        {
            _animator.SetBool("OpenToolWheel", false);
        }

        switch(toolID)
        {
            case 0:
                Debug.Log("No Selection");
                break;
            case 1:
                Debug.Log("Mop");
                break;
            case 2:
                Debug.Log("Sponge");
                break;
            case 3:
                Debug.Log("Trash Bin");
                break;
            case 4:
                Debug.Log("Hands");
                break;
        }
    }
}