using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;
using UnityEngine.UI;

public class PlayersUIManager : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private InputManager inputManager;
    [SerializeField] private CleaningManager cleaningManager;
    [SerializeField] private WaterBucket waterBucket;
    [SerializeField] private PickUpDrop pickUpDrop;
    [SerializeField] private GameStateManager gameStateManager;
    [SerializeField] private Openable[] openables;
    [SerializeField] private OpenableNoAnimator[] openablesNoAnimator;
    [SerializeField] private GameObject tablet;
    [SerializeField] private string notebookAnimatorOpenHash;
    [SerializeField] private Animator cleaningListAnimator;
    [SerializeField] private GameObject cleanableListText;
    [SerializeField] private GameObject disposableListText;
    [SerializeField] private float togglingNotebookErrorDuration;
    [SerializeField] private float togglingWaterBucketErrorDuration;
    [SerializeField] private float pickUpErrorDuration;
    [SerializeField] private float wrongToolErrorDuration;
    [SerializeField] private float dirtyToolErrorDuration;
    [SerializeField] private float missingKeyErrorDuration;

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

    [Header("Keys UI")]
    [SerializeField] private Image missingKeyImageWarning;

    [Header("Retrievable Objects")]
    [SerializeField] private TextMeshProUGUI[] documentsTexts;
    [SerializeField] private TextMeshProUGUI[] clothesTexts;
    [SerializeField] private TextMeshProUGUI[] miscellaneousTexts;
    [SerializeField] private TextMeshProUGUI[] weaponsTexts;

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

    [Header("Post Process Config")]
    [SerializeField] private Volume playerVolume = null;

    private bool _cleaningListState = false;

    private Coroutine notebookWarningCoroutine;
    private Coroutine waterBucketWarningCoroutine;
    private Coroutine pickUpWarningCoroutine;
    private Coroutine wrongToolCleaningWarningCoroutine;
    private Coroutine wrongToolMopCleaningWarningCoroutine;
    private Coroutine wrongToolSpongeCleaningWarningCoroutine;
    private Coroutine toolDirtyWarningCoroutine;
    private Coroutine missingKeyWarningCoroutine;
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
        yesBackToLobbyButton.onClick.AddListener(() => { gameStateManager.uiManager.TriggerEndGame(); });
        noBackToLobbyButton.onClick.AddListener(() => { OpenTab(backToLobbyPanel, false); });


        foreach (Clean cleanableObject in gameStateManager.CleanableObjects)
        {
            cleanableObject.GetComponent<Clean>().WrongTool += () => OnWrongToolWarning(ref wrongToolCleaningWarningCoroutine, new[] { mopImageWarning, spongeImageWarning }, wrongToolErrorDuration);
            cleanableObject.GetComponent<Clean>().WrongToolMop += () => OnToolWarning(ref wrongToolMopCleaningWarningCoroutine, mopImageWarning, wrongToolErrorDuration);
            cleanableObject.GetComponent<Clean>().WrongToolSponge += () => OnToolWarning(ref wrongToolSpongeCleaningWarningCoroutine, spongeImageWarning, wrongToolErrorDuration);
        }

        foreach (var tool in cleaningToolReceivers)
        {
            tool.ToolDirty += () => OnToolWarning(ref toolDirtyWarningCoroutine, bucketImageWarning, dirtyToolErrorDuration);
        }

        foreach (RetrievableObject retrievableObject in gameStateManager.Documents)
        {
            if (retrievableObject != null)
            {
                retrievableObject.GetComponent<RetrievableObject>().ObjectRetrievedEvent += UpdateRetrievableTexts;
            }
        }

        foreach (RetrievableObject retrievableObject in gameStateManager.Clothes)
        {
            if (retrievableObject != null)
            {
                retrievableObject.GetComponent<RetrievableObject>().ObjectRetrievedEvent += UpdateRetrievableTexts;
            }
        }

        foreach (RetrievableObject retrievableObject in gameStateManager.Miscellaneous)
        {
            if (retrievableObject != null)
            {
                retrievableObject.GetComponent<RetrievableObject>().ObjectRetrievedEvent += UpdateRetrievableTexts;
            }
        }

        foreach (RetrievableObject retrievableObject in gameStateManager.Weapons)
        {
            if (retrievableObject != null)
            {
                retrievableObject.GetComponent<RetrievableObject>().ObjectRetrievedEvent += UpdateRetrievableTexts;
            }
        }

        foreach (var openable in openables)
        {
            openable.ungrabbedKey += () => OnMissingKeyWarning(ref missingKeyWarningCoroutine, missingKeyImageWarning, missingKeyErrorDuration);
        }
        
        foreach (var openable in openablesNoAnimator)
        {
            openable.ungrabbedKey += () => OnMissingKeyWarning(ref missingKeyWarningCoroutine, missingKeyImageWarning, missingKeyErrorDuration);
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
                cleanableObject.GetComponent<Clean>().WrongTool -= () => OnWrongToolWarning(ref wrongToolCleaningWarningCoroutine, new[] { mopImageWarning, spongeImageWarning }, wrongToolErrorDuration);
                cleanableObject.GetComponent<Clean>().WrongToolMop -= () => OnToolWarning(ref wrongToolMopCleaningWarningCoroutine, mopImageWarning, wrongToolErrorDuration);
                cleanableObject.GetComponent<Clean>().WrongToolSponge -= () => OnToolWarning(ref wrongToolSpongeCleaningWarningCoroutine, spongeImageWarning, wrongToolErrorDuration);
            }
        }

        foreach (var tool in cleaningToolReceivers)
        {
            tool.ToolDirty -= () => OnToolWarning(ref toolDirtyWarningCoroutine, bucketImageWarning, dirtyToolErrorDuration);
        }

        foreach (RetrievableObject retrievableObject in gameStateManager.Documents)
        {
            if (retrievableObject != null)
            {
                retrievableObject.GetComponent<RetrievableObject>().ObjectRetrievedEvent -= UpdateRetrievableTexts;
            }
        }

        foreach (RetrievableObject retrievableObject in gameStateManager.Clothes)
        {
            if (retrievableObject != null)
            {
                retrievableObject.GetComponent<RetrievableObject>().ObjectRetrievedEvent -= UpdateRetrievableTexts;
            }
        }

        foreach (RetrievableObject retrievableObject in gameStateManager.Miscellaneous)
        {
            if (retrievableObject != null)
            {
                retrievableObject.GetComponent<RetrievableObject>().ObjectRetrievedEvent -= UpdateRetrievableTexts;
            }
        }

        foreach (RetrievableObject retrievableObject in gameStateManager.Weapons)
        {
            if (retrievableObject != null)
            {
                retrievableObject.GetComponent<RetrievableObject>().ObjectRetrievedEvent -= UpdateRetrievableTexts;
            }
        }

        foreach (var openable in openables)
        {
            openable.ungrabbedKey -= () => OnMissingKeyWarning(ref missingKeyWarningCoroutine, missingKeyImageWarning, missingKeyErrorDuration);
        }
        
        foreach (var openable in openablesNoAnimator)
        {
            openable.ungrabbedKey -= () => OnMissingKeyWarning(ref missingKeyWarningCoroutine, missingKeyImageWarning, missingKeyErrorDuration);
        }
    }

    private void Start()
    {
        reticle.SetActive(true);
        toolHolder.SetActive(false);
        jobFinished.SetActive(false);
        jobUnfinished.SetActive(false);

        //CreateCleaningList();
        UpdateRetrievableTexts();
    }

    private void OpenTab(GameObject go, bool state)
    {
        go.SetActive(state);
        cleaningManager.GetAudioManager().PlaySound(cleaningManager.GetClickTabletEvent());
    }

    private void OnCleaningListEvent()
    {
        if (isTogglingNotebook) return;

        _cleaningListState = !_cleaningListState;
        isTabletOpen = _cleaningListState;

        if (!_cleaningListState)
        {
            cleaningManager.GetAudioManager().PlaySound(cleaningManager.GetCloseTabletEvent());
            //if (cleaningListAnimator)
            //{
            //    cleaningListAnimator.SetBool(notebookAnimatorOpenHash, _cleaningListState);
            //}
        }
        else
        {
            gameStateManager.TransitionToState("Tablet");
            reticle.SetActive(false);
            if (playerVolume.profile.TryGet(out Exposure exposure))
                exposure.active = false;
            cleaningManager.GetAudioManager().PlaySound(cleaningManager.GetOpenTabletEvent());
            ToggleTabletState(true);

            //if (cleaningListAnimator)
            //{
            //    cleaningListAnimator.SetBool(notebookAnimatorOpenHash, _cleaningListState);
            //}

        }
    }

    private void ToggleTabletState(bool active)
    {
        isTabletOpen = active;
        isTogglingNotebook = active;
    }

    private void OnToolSwitched(int newIndex)
    {
        if (cleaningManager.GetToolSelector().Tools[newIndex] != tablet)
        {
            gameStateManager.TransitionToState("GamePlayState");
            reticle.SetActive(true);
            if (playerVolume.profile.TryGet(out Exposure exposure))
                exposure.active = true;
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

    private void OnMissingKeyWarning(ref Coroutine warningCoroutine, Image missingKeyWarning, float warningDuration)
    {
        if (warningCoroutine != null)
        {
            StopCoroutine(warningCoroutine);
        }
        warningCoroutine = StartCoroutine(ShowWarning(missingKeyWarning, warningDuration));
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
        gameStateManager.uiManager.TriggerEndGame();
    }

    private void TriggerWinUI()
    {
        jobFinished.SetActive(true);
        gameStateManager.uiManager.TriggerEndGame();
    }

    private void UpdateRetrievableTexts()
    {
        // UPDATE WITH RETRIEVABLE PROPS -----------------------------------------------

        //UpdateRetrievableTexts(documentsTexts, gameStateManager.Documents);
        //UpdateRetrievableTexts(clothesTexts, gameStateManager.Clothes);
        //UpdateRetrievableTexts(miscellaneousTexts, gameStateManager.Miscellaneous);
        UpdateRetrievableTexts(weaponsTexts, gameStateManager.Weapons);
    }

    private void UpdateRetrievableTexts(TextMeshProUGUI[] texts, List<RetrievableObject> retrievableObjects)
    {
        for (int i = 0; i < texts.Length; i++)
        {
            texts[i].text = retrievableObjects[i].name + (retrievableObjects[i].IsObjectPickedUp ? " picked up" : " not picked up");
        }
    }
}
