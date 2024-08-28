using UnityEngine;
using UnityEngine.UI;

public class ToolsWheelController : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private InputManager inputManager;
    [SerializeField] private GameStateManager gameStateManager;
    [SerializeField] private CleaningTool cleaningTool;
    [SerializeField] private CleaningManager cleaningManager;
    [SerializeField] private Image reticle;
    
    public int previousToolID;
    public int currentToolID;

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
        previousToolID = currentToolID;
        currentToolID = -1;

        reticle.gameObject.SetActive(false);

        if (cleaningManager.GetToolSelector().CurrentToolIndex == cleaningManager.GetTablet())
            gameStateManager.TransitionToState("Tablet");
        else
            gameStateManager.TransitionToState("ToolWheel");
        
        _animator.SetBool("OpenToolWheel", true);
    }

    private void OnToolWheelEnd()
    {
        if (currentToolID == -1)
            currentToolID = previousToolID;

        if (cleaningManager.GetToolSelector().CurrentToolIndex == cleaningManager.GetTablet())
            gameStateManager.TransitionToState("Tablet");
        else
        {
            gameStateManager.TransitionToState("GamePlay");
            reticle.gameObject.SetActive(true);
        }

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
        if (currentToolID >= 0 && currentToolID < cleaningTool.ToolsLength)
        {
            cleaningTool.SwitchTool(currentToolID);
        }
        else
        {
            Debug.LogWarning($"ToolID {currentToolID} is out of range.");
        }
    }
}