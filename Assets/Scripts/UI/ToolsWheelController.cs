using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ToolsWheelController : MonoBehaviour
{
    [Header("Config")]
    public int toolID;
    public InputManager inputManager;
    public GameStateManager gameStateManager;
    public CleaningTool cleaningTool;

    private Animator _animator;

    private void OnEnable()
    {
        inputManager.ToggleToolWheelStartEvent += OnToolWheelStart;
        inputManager.ToggleToolWheelEndEvent += OnToolWheelEnd;
    }

    private void OnDisable()
    {
        inputManager.ToggleToolWheelStartEvent -= OnToolWheelStart;
        inputManager.ToggleToolWheelEndEvent -= OnToolWheelEnd;
    }

    private void Start()
    {
        _animator = GetComponent<Animator>();
        AssignToolIDs();
    }

    private void OnToolWheelStart()
    {
        gameStateManager.TransitionToState("ToolWheel");
        _animator.SetBool("OpenToolWheel", true);
    }

    private void OnToolWheelEnd()
    {
        gameStateManager.TransitionToState("GamePlay");
        _animator.SetBool("OpenToolWheel", false);
        SelectTool();
    }

    private void AssignToolIDs()
    {
        ToolsWheel[] toolWheels = GetComponentsInChildren<ToolsWheel>();
        for (int i = 0; i < toolWheels.Length; i++)
        {
            if (i < cleaningTool.ToolsLength)
            {
                toolWheels[i].ID = i;
                toolWheels[i].itemName = cleaningTool.Tools[i].name;
            }
        }
    }

    private void SelectTool()
    {
        if (toolID >= 0 && toolID < cleaningTool.ToolsLength)
        {
            cleaningTool.SwitchTool(toolID);
        }
        else
        {
            Debug.LogWarning($"ToolID {toolID} is out of range.");
        }
    }
}