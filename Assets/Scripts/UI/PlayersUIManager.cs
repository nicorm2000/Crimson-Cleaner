using UnityEngine;
using UnityEngine.UI;

public class PlayersUIManager : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private CleaningToolSelector toolSelector;
    [SerializeField] private Image toolImage;
    [SerializeField] private Sprite[] toolSprites;
    [SerializeField] private GameObject cleaningList;

    private bool _cleaningListState = false;

    private void Start()
    {
        toolSelector.OnToolSwitched += UpdateToolImage;
        UpdateToolImage(toolSelector.CurrentToolIndex);
    }

    private void OnDestroy()
    {
        toolSelector.OnToolSwitched -= UpdateToolImage;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            _cleaningListState = !_cleaningListState;
            cleaningList.SetActive(_cleaningListState);
        }
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