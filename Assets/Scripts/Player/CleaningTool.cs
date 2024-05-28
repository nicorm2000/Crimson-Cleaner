using UnityEngine;
using UnityEngine.Events;

public class CleaningTool : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private InputManager inputManager;
    [SerializeField] private GameObject[] tools;
    [SerializeField] private int[] dirtyPercentages;
    [SerializeField] private int dirtyIncrementAmount;
    [SerializeField] private Material dirtyMaterial;
    [SerializeField] private Material originalMaterial;

    private Renderer _renderer;
    public int DirtyIncrement {get; private set;}
    private int _currentToolIndex = 0;

    public event UnityAction<int> OnToolSwitched;
    public GameObject[] Tools => tools;
    public int ToolsLength => tools.Length;
    public int CurrentToolIndex => _currentToolIndex;

    private void Awake()
    {
        DirtyIncrement = dirtyIncrementAmount;
    }

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
            dirtyPercentages[i] = 0;
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

    public void IncrementDirtyPercentage(int toolIndex, int amount)
    {
        if (toolIndex >= 0 && toolIndex < dirtyPercentages.Length)
        {
            dirtyPercentages[toolIndex] = Mathf.Clamp(dirtyPercentages[toolIndex] + amount, 0, 100);
            Debug.Log($"Tool {toolIndex} dirty percentage: {dirtyPercentages[toolIndex]}%");

            if (dirtyPercentages[toolIndex] >= 100)
            {
                ChangeToolMaterial(toolIndex, dirtyMaterial);
            }
        }
    }

    public int GetDirtyPercentage(int toolIndex)
    {
        if (toolIndex >= 0 && toolIndex < dirtyPercentages.Length)
        {
            return dirtyPercentages[toolIndex];
        }
        return 0;
    }

    public void ChangeToolMaterial(int toolIndex, Material newMaterial)
    {
        Renderer toolRenderer = tools[toolIndex].GetComponentInChildren<Renderer>();
        if (toolRenderer != null)
        {
            toolRenderer.material = newMaterial;
        }
        else
        {
            Debug.LogError($"Renderer component not found on tool {toolIndex}.");
        }
    }

    public Material GetOriginalMaterial()
    {
        return originalMaterial;
    }

    public void ResetDirtyPercentage(int toolIndex)
    {
        if (toolIndex >= 0 && toolIndex < dirtyPercentages.Length)
        {
            dirtyPercentages[toolIndex] = 0;
            Debug.Log($"Tool {toolIndex} cleaned. Dirty percentage reset to {dirtyPercentages[toolIndex]}%.");
        }
    }
}