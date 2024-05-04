using UnityEngine;
using UnityEngine.UI;

public class ToolsUIManager : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private CleaningToolSelector toolSelector;
    [SerializeField] private Image toolImage;
    [SerializeField] private Sprite[] toolSprites;

    private void Start()
    {
        toolSelector.OnToolSwitched += UpdateToolImage;
        UpdateToolImage(toolSelector.CurrentToolIndex);
    }

    private void OnDestroy()
    {
        toolSelector.OnToolSwitched -= UpdateToolImage;
    }

    private void UpdateToolImage(int currentToolIndex)
    {
        if (currentToolIndex >= 0 && currentToolIndex < toolSprites.Length)
        {
            toolImage.sprite = toolSprites[currentToolIndex];
        }
        else
        {
            Debug.LogWarning("No corresponding sprite found for the current tool.");
        }
    }
}