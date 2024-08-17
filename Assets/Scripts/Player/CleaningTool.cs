using UnityEngine;
using UnityEngine.Events;

public class CleaningTool : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private GameObject[] tools;
    [SerializeField] private int[] dirtyPercentages;
    [SerializeField] private int dirtyIncrementAmount;
    [SerializeField] private Material mopCleanMaterial;
    [SerializeField] private Material spongeCleanMaterial;
    [SerializeField] private Material[] mopDirtyMaterial;
    [SerializeField] private Material[] spongeDirtyMaterial;

    [Header("Audio Config")]
    [SerializeField] private AudioManager audioManager = null;
    [SerializeField] private string weaponSwapEvent = null;

    private bool isEventPlaying = false;

    public int DirtyIncrement { get; private set; }
    private int _currentToolIndex = 0;

    public event UnityAction<int> OnToolSwitched;
    public GameObject[] Tools => tools;
    public int ToolsLength => tools.Length;
    public int CurrentToolIndex => _currentToolIndex;

    private void Awake()
    {
        DirtyIncrement = dirtyIncrementAmount;
    }

    private void Start()
    {
        for (int i = 0; i < tools.Length; i++)
        {
            tools[i].SetActive(i == _currentToolIndex);
            dirtyPercentages[i] = 0;
        }
    }

    public void SwitchTool(int newIndex)
    {
        audioManager.PlaySound(weaponSwapEvent);
        newIndex = Mathf.Clamp(newIndex, 0, tools.Length - 1);
        tools[_currentToolIndex].SetActive(false);
        tools[newIndex].SetActive(true);
        _currentToolIndex = newIndex;
        OnToolSwitched?.Invoke(_currentToolIndex);
    }

    public void IncrementDirtyPercentage(int toolIndex, int amount)
    {
        if (toolIndex >= 0 && toolIndex < dirtyPercentages.Length)
        {
            dirtyPercentages[toolIndex] = Mathf.Clamp(dirtyPercentages[toolIndex] + amount, 0, 100);
            Debug.Log($"Tool {toolIndex} dirty percentage: {dirtyPercentages[toolIndex]}%");

            if (dirtyPercentages[toolIndex] <= 100)
            {
                if (_currentToolIndex == 0)
                {
                    if (dirtyPercentages[_currentToolIndex] >= 25 && dirtyPercentages[_currentToolIndex] < 50)
                    {
                        ChangeToolMaterial(toolIndex, mopDirtyMaterial[0]);
                    }
                    else if (dirtyPercentages[_currentToolIndex] >= 50 && dirtyPercentages[_currentToolIndex] < 75)
                    {
                        ChangeToolMaterial(toolIndex, mopDirtyMaterial[1]);
                    }
                    else if (dirtyPercentages[_currentToolIndex] >= 75 && dirtyPercentages[_currentToolIndex] < 100)
                    {
                        ChangeToolMaterial(toolIndex, mopDirtyMaterial[2]);
                    }
                    else if (dirtyPercentages[_currentToolIndex] == 100)
                    {
                        ChangeToolMaterial(toolIndex, mopDirtyMaterial[3]);
                    }
                }
                else if (_currentToolIndex == 1)
                {
                    if (dirtyPercentages[_currentToolIndex] >= 25 && dirtyPercentages[_currentToolIndex] < 50)
                    {
                        ChangeToolMaterial(toolIndex, spongeDirtyMaterial[0]);
                    }
                    else if (dirtyPercentages[_currentToolIndex] >= 50 && dirtyPercentages[_currentToolIndex] < 75)
                    {
                        ChangeToolMaterial(toolIndex, spongeDirtyMaterial[1]);
                    }
                    else if (dirtyPercentages[_currentToolIndex] >= 75 && dirtyPercentages[_currentToolIndex] < 100)
                    {
                        ChangeToolMaterial(toolIndex, spongeDirtyMaterial[2]);
                    }
                    else if (dirtyPercentages[_currentToolIndex] == 100)
                    {
                        ChangeToolMaterial(toolIndex, spongeDirtyMaterial[3]);
                    }
                }
                else
                {
                    Debug.Log(name + " - No tool to swap back material.");
                }
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
        for (int i = 0; i < tools[_currentToolIndex].transform.childCount; i++)
        {
            Renderer toolRenderer = tools[_currentToolIndex].transform.GetChild(i).GetComponentInChildren<Renderer>();
            if (toolRenderer != null)
            {
                toolRenderer.material = newMaterial;
            }
            else
            {
                Debug.LogError($"Renderer component not found on tool {toolIndex}.");
            }
        }
    }

    public Material GetOriginalMaterial()
    {
        if (_currentToolIndex == 0)
        {
            return mopCleanMaterial;
        }
        else if (_currentToolIndex == 1)
        {
            return spongeCleanMaterial;
        }
        else
        {
            Debug.Log(name + " - No tool to swap back material.");
            return null;
        }
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