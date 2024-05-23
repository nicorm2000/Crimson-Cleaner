using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class CleaningToolSelector : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private InputManager inputManager;
    [SerializeField] private GameObject[] tools;
    
    private int _currentToolIndex = 0;

     public event UnityAction<int> OnToolSwitched;

    public GameObject[] Tools => tools;
    public int ToolsLength => tools.Length;
    public int CurrentToolIndex => _currentToolIndex;

    private void OnEnable()
    {
        inputManager.SelectFirstToolEvent += SetMop;
        inputManager.SelectSecondToolEvent += SetSponge;
        inputManager.SelectThirdToolEvent += SetHands;
    }

    private void OnDisable()
    {
        inputManager.SelectFirstToolEvent -= SetMop;
        inputManager.SelectSecondToolEvent -= SetSponge;
        inputManager.SelectThirdToolEvent -= SetHands;
    }

    private void Start()
    {
        for (int i = 0; i < tools.Length; i++)
        {
            tools[i].SetActive(i == _currentToolIndex);
        }
    }

    private void Update()
    {
        if (inputManager.Scroll > 0f)
        {
            SwitchTool(_currentToolIndex - 1);
        }
        else if (inputManager.Scroll < 0f)
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

    private void SetMop()
    {
        SwitchTool(0);
    }

    private void SetSponge()
    {
        SwitchTool(1);
    }

    private void SetHands()
    {
        SwitchTool(2);
    }
}