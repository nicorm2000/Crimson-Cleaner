using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayersUIManager : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private InputManager inputManager;
    [SerializeField] private CleaningManager cleaningManager;
    [SerializeField] private WaterBucket waterBucket;
    [SerializeField] private PickUpDrop pickUpDrop;
    [SerializeField] private GameStateManager gameStateManager;
    [SerializeField] private GameObject notebook;
    [SerializeField] private string notebookAnimatorOpenHash;
    [SerializeField] private Animator cleaningListAnimator;
    [SerializeField] private GameObject cleanableListText;
    [SerializeField] private GameObject disposableListText;
    [SerializeField] private float togglingNotebookErrorDuration;
    [SerializeField] private float togglingWaterBucketErrorDuration;
    [SerializeField] private float pickUpErrorDuration;
    [SerializeField] private float wrongToolErrorDuration;
    [SerializeField] private float dirtyToolErrorDuration;

    [Header("Tools")]
    [SerializeField] private CleaningToolReceiver[] cleaningToolReceivers;

    [Header("Tools UI")]
    [SerializeField] private GameObject reticle;
    [SerializeField] private GameObject toolHolder;
    [SerializeField] private float toolHolderLifetime;
    [SerializeField] private Image mopImage;
    [SerializeField] private Image spongeImage;
    [SerializeField] private Image handImage;
    [SerializeField] private Sprite mopSpriteOn;
    [SerializeField] private Sprite mopSpriteOff;
    [SerializeField] private Sprite spongeSpriteOn;
    [SerializeField] private Sprite spongeSpriteOff;
    [SerializeField] private Sprite handSpriteOn;
    [SerializeField] private Sprite handSpriteOff;
    [SerializeField] private Image mopImageWarning;
    [SerializeField] private Image spongeImageWarning;
    [SerializeField] private Image handImageWarning;
    [SerializeField] private Image bucketImageWarning;

    [Header("Back To Lobby")]
    [SerializeField] private GameObject backToLobbyPanel = null;
    [SerializeField] private Button backToLobbyFinishedButton = null;
    [SerializeField] private Button backToLobbyUnfinishedButton = null;
    [SerializeField] private Button yesBackToLobbyButton = null;
    [SerializeField] private Button noBackToLobbyButton = null;
    [SerializeField] private string lobbySceneName = null;

    [Header("Job State")]
    [SerializeField] GameObject jobFinished;
    [SerializeField] GameObject jobUnfinished;

    [Header("Audio Config")]
    [SerializeField] private AudioManager audioManager = null;
    [SerializeField] private string openNotebookEvent = null;
    [SerializeField] private string closeNotebookEvent = null;
    [SerializeField] private string clickEvent = null;

    private bool _cleaningListState = false;
    private List<GameObject> cleaningTextElements = new();
    private List<GameObject> disposalTextElements = new();

    private Coroutine notebookWarningCoroutine;
    private Coroutine waterBucketWarningCoroutine;
    private Coroutine pickUpWarningCoroutine;
    private Coroutine wrongToolCleaningWarningCoroutine;
    private Coroutine wrongToolMopCleaningWarningCoroutine;
    private Coroutine wrongToolSpongeCleaningWarningCoroutine;
    private Coroutine toolDirtyWarningCoroutine;
    private bool isTogglingNotebook = false;
    private bool isTabletOpen = false;

    private void OnEnable()
    {
        backToLobbyPanel.SetActive(false);

        gameStateManager.GameLost += TriggerLostUI;
        gameStateManager.GameWon += TriggerWinUI;
        cleaningManager.GetToolSelector().OnToolSwitched += OnToolSwitched;
        cleaningManager.GetToolSelector().CleaningListEvent += OnCleaningListEvent;
        waterBucket.WaterBucketUnavailable += OnToggleWaterbucketUnavailiable;
        pickUpDrop.PickUpUnavailableEvent += OnPickUpUnavailable;

        backToLobbyFinishedButton.onClick.AddListener(() => { OpenTab(backToLobbyPanel, true); });
        backToLobbyUnfinishedButton.onClick.AddListener(() => { OpenTab(backToLobbyPanel, true); });
        yesBackToLobbyButton.onClick.AddListener(() => { gameStateManager.TransitionToState("DeInit"); MySceneManager.Instance.LoadSceneByName(lobbySceneName); });
        noBackToLobbyButton.onClick.AddListener(() => { OpenTab(backToLobbyPanel, false); });


        foreach (Clean cleanableObject in gameStateManager.CleanableObjects)
        {
            cleanableObject.GetComponent<Clean>().CleanedGO += UpdateCleaningList;
            cleanableObject.GetComponent<Clean>().WrongTool += () => OnWrongToolWarning(ref wrongToolCleaningWarningCoroutine, new[] { mopImageWarning, spongeImageWarning }, wrongToolErrorDuration);
            cleanableObject.GetComponent<Clean>().WrongToolMop += () => OnToolWarning(ref wrongToolMopCleaningWarningCoroutine, mopImageWarning, wrongToolErrorDuration);
            cleanableObject.GetComponent<Clean>().WrongToolSponge += () => OnToolWarning(ref wrongToolSpongeCleaningWarningCoroutine, spongeImageWarning, wrongToolErrorDuration);
        }

        foreach (DisposableObject disposableObject in gameStateManager.DisposableObjects)
        {
            disposableObject.GetComponent<DisposableObject>().DisposedGO += UpdateDisposableList;
            disposableObject.GetComponent<DisposableObject>().Broken += UpdateDisposableListMultiple;
        }

        foreach (var tool in cleaningToolReceivers)
        {
            tool.ToolDirty += () => OnToolWarning(ref toolDirtyWarningCoroutine, bucketImageWarning, dirtyToolErrorDuration);
        }
    }

    private void OnDisable()
    {
        gameStateManager.GameLost -= TriggerLostUI;
        gameStateManager.GameWon -= TriggerWinUI;
        cleaningManager.GetToolSelector().OnToolSwitched -= OnToolSwitched;
        cleaningManager.GetToolSelector().CleaningListEvent -= OnCleaningListEvent;
        waterBucket.WaterBucketUnavailable -= OnToggleWaterbucketUnavailiable;
        pickUpDrop.PickUpUnavailableEvent -= OnPickUpUnavailable;

        foreach (Clean cleanableObject in gameStateManager.CleanableObjects)
        {
            if (cleanableObject != null)
            {
                cleanableObject.GetComponent<Clean>().CleanedGO -= UpdateCleaningList;
                cleanableObject.GetComponent<Clean>().WrongTool -= () => OnWrongToolWarning(ref wrongToolCleaningWarningCoroutine, new[] { mopImageWarning, spongeImageWarning }, wrongToolErrorDuration);
                cleanableObject.GetComponent<Clean>().WrongToolMop -= () => OnToolWarning(ref wrongToolMopCleaningWarningCoroutine, mopImageWarning, wrongToolErrorDuration);
                cleanableObject.GetComponent<Clean>().WrongToolSponge -= () => OnToolWarning(ref wrongToolSpongeCleaningWarningCoroutine, spongeImageWarning, wrongToolErrorDuration);
            }
        }

        foreach (DisposableObject disposableObject in gameStateManager.DisposableObjects)
        {
            if (disposableObject != null)
            {
                disposableObject.GetComponent<DisposableObject>().DisposedGO -= UpdateDisposableList;
                disposableObject.GetComponent<DisposableObject>().Broken -= UpdateDisposableListMultiple;
            }
        }

        foreach (var tool in cleaningToolReceivers)
        {
            tool.ToolDirty -= () => OnToolWarning(ref toolDirtyWarningCoroutine, bucketImageWarning, dirtyToolErrorDuration);
        }
    }

    private void Start()
    {
        reticle.SetActive(true);
        toolHolder.SetActive(false);
        jobFinished.SetActive(false);
        jobUnfinished.SetActive(false);

        CreateCleaningList();
    }

    private void OpenTab(GameObject go, bool state)
    {
        go.SetActive(state);
        audioManager.PlaySound(clickEvent);
    }

    private void OnCleaningListEvent()
    {
        if (isTogglingNotebook) return;

        _cleaningListState = !_cleaningListState;
        isTabletOpen = _cleaningListState;

        if (!_cleaningListState)
        {
            audioManager.PlaySound(closeNotebookEvent);
        }
        else
        {
            audioManager.PlaySound(openNotebookEvent);
            ToggleTabletState(true);

            if (cleaningListAnimator)
            {
                cleaningListAnimator.SetBool(notebookAnimatorOpenHash, _cleaningListState);
            }
        }
    }

    private void ToggleTabletState(bool active)
    {
        isTabletOpen = active;
        isTogglingNotebook = active;
    }

    private void OnToolSwitched(int newIndex)
    {
        if (cleaningManager.GetToolSelector().Tools[newIndex] != notebook)
        {
            if (isTabletOpen)
            {
                ToggleTabletState(false);
            }
        }
    }

    private void OnToggleWaterbucketUnavailiable()
    {
        if (waterBucketWarningCoroutine != null)
        {
            StopCoroutine(waterBucketWarningCoroutine);
        }
        waterBucketWarningCoroutine = StartCoroutine(ShowWarning(new[] { mopImageWarning, spongeImageWarning }, togglingWaterBucketErrorDuration));
    }
    
    private void OnPickUpUnavailable()
    {
        if (pickUpWarningCoroutine != null)
        {
            StopCoroutine(pickUpWarningCoroutine);
        }
        pickUpWarningCoroutine = StartCoroutine(ShowWarning(handImageWarning, pickUpErrorDuration));
    }

    private void OnToolWarning(ref Coroutine warningCoroutine, Image toolImageWarning, float warningDuration)
    {
        if (warningCoroutine != null)
        {
            StopCoroutine(warningCoroutine);
        }
        warningCoroutine = StartCoroutine(ShowWarning(toolImageWarning, warningDuration));
    }
    
    private void OnWrongToolWarning(ref Coroutine warningCoroutine, Image[] toolImageWarning, float warningDuration)
    {
        if (warningCoroutine != null)
        {
            StopCoroutine(warningCoroutine);
        }
        warningCoroutine = StartCoroutine(ShowWarning(toolImageWarning, warningDuration));
    }

    private IEnumerator ShowWarning(Image warningImage, float duration)
    {
        warningImage.gameObject.SetActive(true);
        yield return new WaitForSeconds(duration);
        warningImage.gameObject.SetActive(false);
    }

    private IEnumerator ShowWarning(Image[] warningImage, float duration)
    {
        foreach (Image warningImageItem in warningImage) warningImageItem.gameObject.SetActive(true);
        yield return new WaitForSeconds(duration);
        foreach (Image warningImageItem in warningImage) warningImageItem.gameObject.SetActive(false);
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
        cleaningTextElements.Clear();
        disposalTextElements.Clear();

        foreach (var cleanableObject in gameStateManager.CleanableObjects)
        {
            cleaningTextElements.Add(cleanableObject.gameObject);
        }

        foreach (var disposableObject in gameStateManager.DisposableObjects)
        {
            disposalTextElements.Add(disposableObject.gameObject);
        }

        UpdateCleanableListText();
        UpdateDisposableListText();
    }

    private void UpdateCleanableListText()
    {
        cleanableListText.GetComponent<TextMeshPro>().text = "Cleaning List " + "\n";
        foreach (var cleanableObject in cleaningTextElements)
        {
            cleanableListText.GetComponent<TextMeshPro>().text += cleanableObject.name + "\n";
        }
    }

    private void UpdateDisposableListText()
    {
        disposableListText.GetComponent<TextMeshPro>().text = "Disposable List " + "\n";
        foreach (var disposableObject in disposalTextElements)
        {
            disposableListText.GetComponent<TextMeshPro>().text += disposableObject.name + "\n";
        }
    }

    private void UpdateCleaningList(GameObject objectInList)
    {
        cleaningTextElements.Remove(objectInList);
        UpdateCleanableListText();
    }

    private void UpdateDisposableList(GameObject objectInList)
    {
        disposalTextElements.Remove(objectInList);
        UpdateDisposableListText();
    }

    private void UpdateDisposableListMultiple(GameObject objectInList, List<GameObject> objectsToAdd)
    {
        disposalTextElements.Remove(objectInList);

        foreach (var disposableObject in objectsToAdd)
        {
            disposalTextElements.Add(disposableObject);
            disposableObject.GetComponent<DisposableObject>().DisposedGO += UpdateDisposableList;
        }

        UpdateDisposableListText();
    }
}
