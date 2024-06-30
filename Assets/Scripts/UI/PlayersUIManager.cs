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

    private Coroutine toolDissapearCoroutine;
    private Coroutine notebookWarningCoroutine;
    private Coroutine waterBucketWarningCoroutine;
    private Coroutine pickUpWarningCoroutine;
    private Coroutine wrongToolCleaningWarningCoroutine;
    private Coroutine wrongToolMopCleaningWarningCoroutine;
    private Coroutine wrongToolSpongeCleaningWarningCoroutine;
    private bool isTogglingNotebook = false;

    private void OnEnable()
    {
        backToLobbyPanel.SetActive(false);

        inputManager.CleaningListEvent += OnCleaningListEvent;
        gameStateManager.GameLost += TriggerLostUI;
        gameStateManager.GameWon += TriggerWinUI;
        cleaningManager.GetToolSelector().OnToolSwitched += HandleToolSwitched;
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
            cleanableObject.GetComponent<Clean>().WrongToolMop += () => OnWrongToolWarning(ref wrongToolMopCleaningWarningCoroutine, mopImageWarning, wrongToolErrorDuration);
            cleanableObject.GetComponent<Clean>().WrongToolSponge += () => OnWrongToolWarning(ref wrongToolSpongeCleaningWarningCoroutine, spongeImageWarning, wrongToolErrorDuration);
        }

        foreach (DisposableObject disposableObject in gameStateManager.DisposableObjects)
        {
            disposableObject.GetComponent<DisposableObject>().DisposedGO += UpdateDisposableList;
            disposableObject.GetComponent<DisposableObject>().Broken += UpdateDisposableListMultiple;
        }
    }

    private void OnDisable()
    {
        inputManager.CleaningListEvent -= OnCleaningListEvent;
        gameStateManager.GameLost -= TriggerLostUI;
        gameStateManager.GameWon -= TriggerWinUI;
        cleaningManager.GetToolSelector().OnToolSwitched -= HandleToolSwitched;
        waterBucket.WaterBucketUnavailable -= OnToggleWaterbucketUnavailiable;
        pickUpDrop.PickUpUnavailableEvent -= OnPickUpUnavailable;

        foreach (Clean cleanableObject in gameStateManager.CleanableObjects)
        {
            if (cleanableObject != null)
            {
                cleanableObject.GetComponent<Clean>().CleanedGO -= UpdateCleaningList;
                cleanableObject.GetComponent<Clean>().WrongTool -= () => OnWrongToolWarning(ref wrongToolCleaningWarningCoroutine, new[] { mopImageWarning, spongeImageWarning }, wrongToolErrorDuration);
                cleanableObject.GetComponent<Clean>().WrongToolMop -= () => OnWrongToolWarning(ref wrongToolMopCleaningWarningCoroutine, mopImageWarning, wrongToolErrorDuration);
                cleanableObject.GetComponent<Clean>().WrongToolSponge -= () => OnWrongToolWarning(ref wrongToolSpongeCleaningWarningCoroutine, spongeImageWarning, wrongToolErrorDuration);
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
    }

    private void Start()
    {
        cleaningManager.GetToolSelector().OnToolSwitched += UpdateToolImage;
        UpdateToolImage(cleaningManager.GetToolSelector().CurrentToolIndex);

        reticle.SetActive(true);
        toolHolder.SetActive(false);
        jobFinished.SetActive(false);
        jobUnfinished.SetActive(false);

        CreateCleaningList();
    }

    private void OnDestroy()
    {
        cleaningManager.GetToolSelector().OnToolSwitched -= UpdateToolImage;
    }

    private void HandleToolSwitched(int newToolIndex)
    {
        if (newToolIndex != 2 && notebook.activeSelf)
        {
            _cleaningListState = false;
            StartCoroutine(ToggleNotebookState());
            cleaningListAnimator.SetBool(notebookAnimatorOpenHash, _cleaningListState);
        }
    }

    private void OpenTab(GameObject go, bool state)
    {
        go.SetActive(state);
        audioManager.PlaySound(clickEvent);
    }

    private void OnCleaningListEvent()
    {
        if (cleaningManager.GetToolSelector().CurrentToolIndex == cleaningManager.GetToolSelector().ToolsLength - 1)
        {
            if (!isTogglingNotebook)
            {
                _cleaningListState = !_cleaningListState;

                if (!_cleaningListState) // If the list is closing
                {
                    audioManager.PlaySound(closeNotebookEvent);
                    StartCoroutine(ToggleNotebookState());
                }
                else
                {
                    audioManager.PlaySound(openNotebookEvent);
                    notebook.SetActive(true);
                }

                if (cleaningListAnimator)
                {
                    cleaningListAnimator.SetBool(notebookAnimatorOpenHash, _cleaningListState);
                }
            }
        }
        else
        {
            if (notebookWarningCoroutine != null)
            {
                StopCoroutine(notebookWarningCoroutine);
            }
            notebookWarningCoroutine = StartCoroutine(ShowWarning(handImageWarning, togglingNotebookErrorDuration));
        }
    }

    private IEnumerator ToggleNotebookState()
    {
        isTogglingNotebook = true;
        if (cleaningListAnimator)
        {
            notebook.SetActive(true);
            float closeDuration = cleaningListAnimator.GetCurrentAnimatorStateInfo(0).length;
            if (closeDuration == 0f)
            {
                closeDuration = cleaningListAnimator.GetCurrentAnimatorClipInfo(0)[0].clip.length;
            }
            yield return new WaitForSeconds(closeDuration);
            notebook.SetActive(false);
        }
        isTogglingNotebook = false;
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

    private void OnWrongToolWarning(ref Coroutine warningCoroutine, Image toolImageWarning, float warningDuration)
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

    private void UpdateToolImage(int currentToolIndex)
    {
        reticle.SetActive(false);
        toolHolder.SetActive(true);
        switch (cleaningManager.GetToolSelector().CurrentToolIndex)
        {
            case 0:
                mopImage.sprite = mopSpriteOn;
                spongeImage.sprite = spongeSpriteOff;
                handImage.sprite = handSpriteOff;
                break;
            case 1:
                mopImage.sprite = mopSpriteOff;
                spongeImage.sprite = spongeSpriteOn;
                handImage.sprite = handSpriteOff;
                break;
            case 2:
                mopImage.sprite = mopSpriteOff;
                spongeImage.sprite = spongeSpriteOff;
                handImage.sprite = handSpriteOn;
                break;
            default:
                mopImage.sprite = mopSpriteOff;
                spongeImage.sprite = spongeSpriteOff;
                handImage.sprite = handSpriteOff;
                break;
        }

        if (toolDissapearCoroutine != null)
        {
            StopCoroutine(toolDissapearCoroutine);
        }
        toolDissapearCoroutine = StartCoroutine(WaitToolDisappear());
    }

    private IEnumerator WaitToolDisappear()
    {
        yield return new WaitForSeconds(toolHolderLifetime);
        toolHolder.SetActive(false);
        reticle.SetActive(true);
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
