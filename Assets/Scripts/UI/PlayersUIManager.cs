using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Rendering.FilterWindow;

public class PlayersUIManager : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private InputManager inputManager;
    [SerializeField] private CleaningManager cleaningManager;
    [SerializeField] private GameStateManager gameStateManager;
    [SerializeField] private Image toolImage;
    [SerializeField] private Sprite[] toolSprites;
    [SerializeField] private GameObject cleaningList;
    [SerializeField] private GameObject displayControls;
    [SerializeField] private GameObject textElementPrefab;
    [SerializeField] private Transform cleanableListParent;
    [SerializeField] private Transform disposableListParent;
    public GameObject jobFinished;
    public GameObject jobUnfinished;


    private bool _cleaningListState = false;
    private bool _displayControlsState = false;
    private List<GameObject> cleaningTextElements = new List<GameObject>();
    private List<GameObject> disposalTextElements = new List<GameObject>();

    private void OnEnable()
    {
        inputManager.CleaningListEvent += CleaningListState;
        inputManager.DisplayControlsEvent += DisplayControlsState;
        gameStateManager.GameLost += TriggerLostUI;
        gameStateManager.GameWon += TriggerWinUI;

        foreach (var cleanableObject  in gameStateManager.CleanableObjects)
        {
            cleanableObject.GetComponent<Clean>().CleanedGO += UpdateCleaningList;
        }

        foreach (var disposableObject in gameStateManager.DisposableObjects)
        {
            disposableObject.GetComponent<DisposableObject>().DisposedGO += UpdateDisposableList;
        }
    }

    private void OnDisable()
    {
        inputManager.CleaningListEvent -= CleaningListState;
        inputManager.DisplayControlsEvent -= DisplayControlsState;
        gameStateManager.GameLost -= TriggerLostUI;
        gameStateManager.GameWon -= TriggerWinUI;

        foreach (var cleanableObject in gameStateManager.CleanableObjects)
        {
            cleanableObject.GetComponent<Clean>().CleanedGO -= UpdateCleaningList;
        }

        foreach (var disposableObject in gameStateManager.DisposableObjects)
        {
            disposableObject.GetComponent<DisposableObject>().DisposedGO -= UpdateDisposableList;
        }
    }

    private void Start()
    {
        cleaningManager.GetToolSelector().OnToolSwitched += UpdateToolImage;
        UpdateToolImage(cleaningManager.GetToolSelector().CurrentToolIndex);

        jobFinished.SetActive(false);
        jobUnfinished.SetActive(false);

        CreateCleaningList();
    }

    private void OnDestroy()
    {
        cleaningManager.GetToolSelector().OnToolSwitched -= UpdateToolImage;
    }

    private void CleaningListState()
    {
        _cleaningListState = !_cleaningListState;
        cleaningList.SetActive(_cleaningListState);
    }

    private void DisplayControlsState()
    {
        _displayControlsState = !_displayControlsState;
        displayControls.SetActive(_displayControlsState);
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

    private void TriggerLostUI()
    {
        jobUnfinished.SetActive(true);
    }
    private void TriggerWinUI()
    {
        jobFinished.SetActive(true);
    }

    public void CreateCleaningList()
    {
        foreach (var element in cleaningTextElements)
        {
            Destroy(element);
        }

        cleaningTextElements.Clear();

        foreach (var cleanableObject in gameStateManager.CleanableObjects)
        {
            GameObject textElement = Instantiate(textElementPrefab, cleanableListParent);
            TextMeshProUGUI tmp = textElement.GetComponent<TextMeshProUGUI>();
            if (tmp != null)
            {
                tmp.text = cleanableObject.name;

                textElement.name = cleanableObject.name;
            }
            cleaningTextElements.Add(textElement);
        }

        foreach (var element in disposalTextElements)
        {
            Destroy(element);
        }

        disposalTextElements.Clear();

        foreach (var disposableObject in gameStateManager.DisposableObjects)
        {
            GameObject textElement = Instantiate(textElementPrefab, disposableListParent);
            TextMeshProUGUI tmp = textElement.GetComponent<TextMeshProUGUI>();
            if (tmp != null)
            {
                tmp.text = disposableObject.name;

                textElement.name = disposableObject.name;
            }
            disposalTextElements.Add(textElement);
        }
    }

    private void UpdateCleaningList(GameObject objectInList)
    {
        for (int i = 0; i < cleaningTextElements.Count; i++)
        {
            if (cleaningTextElements[i].name == objectInList.name)
            {
                Destroy(cleaningTextElements[i]);
                cleaningTextElements.RemoveAt(i);
                break;
            }
        }
    }

    private void UpdateDisposableList(GameObject objectInList)
    {
        for (int i = 0; i < disposalTextElements.Count; i++)
        {
            if (disposalTextElements[i].name == objectInList.name)
            {
                Destroy(disposalTextElements[i]);
                disposalTextElements.RemoveAt(i);
                break;
            }
        }
    }
}