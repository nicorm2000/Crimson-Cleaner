using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class CleaningToolSelector : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private GameObject[] tools;
    
    private int _currentToolIndex = 0;

     public event UnityAction<int> OnToolSwitched;

    public GameObject[] Tools => tools;
    public int ToolsLength => tools.Length;
    public int CurrentToolIndex => _currentToolIndex;

    private void Start()
    {
        for (int i = 0; i < tools.Length; i++)
        {
            tools[i].SetActive(i == _currentToolIndex);
        }
    }

    private void Update()
    {
        if (Keyboard.current.digit1Key.wasPressedThisFrame)
        {
            Debug.Log("Mop");
            SwitchTool(0);
        }
        else if (Keyboard.current.digit2Key.wasPressedThisFrame)
        {
            Debug.Log("Sponge");
            SwitchTool(1);
        }
        else if (Keyboard.current.digit3Key.wasPressedThisFrame)
        {
            Debug.Log("Hands");
            SwitchTool(2);
        }

        float scrollInput = Mouse.current.scroll.ReadValue().y;
        if (scrollInput > 0f)
        {
            SwitchTool(_currentToolIndex - 1);
        }
        else if (scrollInput < 0f)
        {
            SwitchTool(_currentToolIndex + 1);
        }
    }

    private void SwitchTool(int newIndex)
    {
        newIndex = Mathf.Clamp(newIndex, 0, tools.Length - 1);

        tools[_currentToolIndex].SetActive(false);

        tools[newIndex].SetActive(true);

        _currentToolIndex = newIndex;

        OnToolSwitched?.Invoke(_currentToolIndex);
    }
}