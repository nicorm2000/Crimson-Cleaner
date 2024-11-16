using System;
using UnityEditor.Rendering.LookDev;
using UnityEngine;
using UnityEngine.Events;

public class CleaningTool : MonoBehaviour
{
    [Header("Config")]
    public ToolAnimatorController toolAnimatorController;
    [SerializeField] private CleaningManager cleaningManager;
    [SerializeField] private GameObject[] tools;
    [SerializeField] private GameObject[] cleaningTools;
    public GameObject mopObject;
    public GameObject spongeObject;
    [SerializeField] private int[] dirtyPercentages;
    [SerializeField] private int dirtyIncrementAmount;
    [SerializeField] private Material mopCleanMaterial;
    [SerializeField] private Material spongeCleanMaterial;
    [SerializeField] private Material[] mopDirtyMaterial;
    [SerializeField] private Material[] spongeDirtyMaterial;
    [SerializeField] private string tabletName;

    private bool isTabletOpen = false;
    private int _previousToolIndex = 0;
    
    public int DirtyIncrement { get; private set; }
    private int _currentToolIndex = 0;

    public event UnityAction<int> OnToolSwitched;
    public event Action CleaningListEvent;
    public GameObject[] CleaningTools => cleaningTools;
    public GameObject[] Tools => tools;
    public int CleaningToolsLength => cleaningTools.Length;
    public int ToolsLength => tools.Length;
    public int CurrentToolIndex => _currentToolIndex;

    private int tabletIndex = 0;

    private void Awake()
    {
        DirtyIncrement = dirtyIncrementAmount;
    }

    private void OnEnable()
    {
        cleaningManager.GetInputManager().FirstToolEvent += OnSelectFirstTool;
        cleaningManager.GetInputManager().SecondToolEvent += OnSelectSecondTool;
        cleaningManager.GetInputManager().ThirdToolEvent += OnSelectThirdTool;
        cleaningManager.GetInputManager().ForthToolEvent += OnSelectForthTool;
        cleaningManager.GetInputManager().FifthToolEvent += OnSelectFifthTool;
    }
    private void OnDisable()
    {
        cleaningManager.GetInputManager().FirstToolEvent -= OnSelectFirstTool;
        cleaningManager.GetInputManager().SecondToolEvent -= OnSelectSecondTool;
        cleaningManager.GetInputManager().ThirdToolEvent -= OnSelectThirdTool;
        cleaningManager.GetInputManager().ForthToolEvent -= OnSelectForthTool;
        cleaningManager.GetInputManager().FifthToolEvent -= OnSelectFifthTool;
    }
    private void Start()
    {
        for (int i = 0; i < cleaningTools.Length; i++)
        {
            dirtyPercentages[i] = 0;
        }

        for (int i = 0; i < tools.Length; i++)
        {
            //tools[i].SetActive(false);
            if (Tools[i].name == tabletName)
            {
                tabletIndex = i;
            }
        }

    }

    public void SwitchTool(int newIndex)
    {
        newIndex = Mathf.Clamp(newIndex, 0, tools.Length - 1);

        if (newIndex == tabletIndex && isTabletOpen)
        {
            return;
        }

        //tools[_currentToolIndex].SetActive(false);        

        _previousToolIndex = _currentToolIndex;
        _currentToolIndex = newIndex;

        OnToolSwitched?.Invoke(_currentToolIndex);

        toolAnimatorController.HandleOffTool(_previousToolIndex);
        toolAnimatorController.HandleToolSwitched(_currentToolIndex);

        PlayToolSwapSound(_currentToolIndex);

        if (newIndex == tabletIndex)
        {
            tools[newIndex].SetActive(true);
            isTabletOpen = true;
            CleaningListEvent?.Invoke();
        }
        else
        {
            if (isTabletOpen)
            {
                isTabletOpen = false;
                CleaningListEvent?.Invoke();
            }
        }
    }

    private void PlayToolSwapSound(int toolIndex)
    {
        AudioManager audioManager = cleaningManager.GetAudioManager();

        if (toolIndex == 0) // Mop
        {
            if (cleaningManager.GetMopSwapEvent() != null)
                audioManager.PlaySound(cleaningManager.GetHandSwapEvent());
        }
        else if (toolIndex == 1) // Sponge
        {
            if (cleaningManager.GetSpongeSwapEvent() != null)
                audioManager.PlaySound(cleaningManager.GetMopSwapEvent());
        }
        else if (toolIndex == 2) // Hands
        {
            if (cleaningManager.GetHandSwapEvent() != null)
                audioManager.PlaySound(cleaningManager.GetSpongeSwapEvent());
        }
        else if (toolIndex == 3) // Bin
        {
            if (cleaningManager.GetTrashBinSwapEvent() != null)
                audioManager.PlaySound(cleaningManager.GetTrashBinSwapEvent());
        }
        else
        {
            Debug.LogWarning("No sound assigned for this tool.");
        }
    }

    public void IncrementDirtyPercentage(int toolIndex, int amount)
    {
        if (toolIndex >= 0 && toolIndex < dirtyPercentages.Length)
        {
            dirtyPercentages[toolIndex] = Mathf.Clamp(dirtyPercentages[toolIndex] + amount, 0, 100);
            Debug.Log($"Tool {toolIndex} dirty percentage: {dirtyPercentages[toolIndex]}%");

            if (dirtyPercentages[toolIndex] <= 100)
            {
                if (_currentToolIndex == cleaningManager.GetMop())
                {
                    if (dirtyPercentages[_currentToolIndex] >= 25 && dirtyPercentages[_currentToolIndex] < 50)
                    {
                        ChangeToolMaterial(mopObject, toolIndex, mopDirtyMaterial[0]);
                    }
                    else if (dirtyPercentages[_currentToolIndex] >= 50 && dirtyPercentages[_currentToolIndex] < 75)
                    {
                        ChangeToolMaterial(mopObject, toolIndex, mopDirtyMaterial[1]);
                    }
                    else if (dirtyPercentages[_currentToolIndex] >= 75 && dirtyPercentages[_currentToolIndex] < 100)
                    {
                        ChangeToolMaterial(mopObject, toolIndex, mopDirtyMaterial[2]);
                    }
                    else if (dirtyPercentages[_currentToolIndex] == 100)
                    {
                        ChangeToolMaterial(mopObject, toolIndex, mopDirtyMaterial[3]);
                        cleaningManager.GetMopDrippingDirtyParticles().Play();
                    }
                }
                else if (_currentToolIndex == cleaningManager.GetSponge())
                {
                    if (dirtyPercentages[_currentToolIndex] >= 25 && dirtyPercentages[_currentToolIndex] < 50)
                    {
                        ChangeToolMaterial(spongeObject, toolIndex, spongeDirtyMaterial[0]);
                    }
                    else if (dirtyPercentages[_currentToolIndex] >= 50 && dirtyPercentages[_currentToolIndex] < 75)
                    {
                        ChangeToolMaterial(spongeObject, toolIndex, spongeDirtyMaterial[1]);
                    }
                    else if (dirtyPercentages[_currentToolIndex] >= 75 && dirtyPercentages[_currentToolIndex] < 100)
                    {
                        ChangeToolMaterial(spongeObject, toolIndex, spongeDirtyMaterial[2]);
                    }
                    else if (dirtyPercentages[_currentToolIndex] == 100)
                    {
                        ChangeToolMaterial(spongeObject, toolIndex, spongeDirtyMaterial[3]);
                        cleaningManager.GetSpongeDrippingDirtyParticles().Play();
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

    public void ChangeToolMaterial(GameObject toolObject,int toolIndex, Material newMaterial)
    {
        if (toolObject)
            toolObject.GetComponent<Renderer>().material = newMaterial;
        else
            Debug.LogError($"Renderer component not found on tool {toolIndex}.");
    }

    public Material GetOriginalMaterial()
    {
        if (_currentToolIndex == cleaningManager.GetMop())
        {
            return mopCleanMaterial;
        }
        else if (_currentToolIndex == cleaningManager.GetSponge())
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

    private void OnSelectFirstTool()
    {
        if (CurrentToolIndex != 0) // Tablet now in first position
            SwitchTool(0);
    }
    private void OnSelectSecondTool()
    {
        if (CurrentToolIndex != 1)
            SwitchTool(1);
    }
    private void OnSelectThirdTool()
    {
        if (CurrentToolIndex != 2)
            SwitchTool(2);
    }
    private void OnSelectForthTool()
    {
        if (CurrentToolIndex != 3) // Hands now in first position
            SwitchTool(3);
    }
    private void OnSelectFifthTool()
    {
        if (CurrentToolIndex != 4) // Hands now in first position
            SwitchTool(4);
    }

    public int GetPreviousToolIndex() => _previousToolIndex;
}