using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private InputManager inputManager;
    [SerializeField] private GameObject gameplayCanvasGO;
    [SerializeField] private GameObject pauseCanvasGO;
    [SerializeField] private GameStateManager gameStateManager = null;

    [Header("Game Pause")]
    [SerializeField] private Button pauseMenuButton = null;
    [SerializeField] private Button settingsButton = null;
    [SerializeField] private GameObject pauseMenuPanel = null;
    [SerializeField] private GameObject settingsMenuPanel = null;

    [Header("Resume")]
    [SerializeField] private Button resumeGameButton = null;

    [Header("Back To Lobby")]
    [SerializeField] private MySceneManager mySceneManager = null;
    [SerializeField] private GameObject backToLobbyPanel = null;
    [SerializeField] private Button backToLobbyButton = null;
    [SerializeField] private Button yesBackToLobbyButton = null;
    [SerializeField] private Button noBackToLobbyButton = null;
    [SerializeField] private string lobbySceneName = null;

    [Header("Settings")]
    [SerializeField] private Button audioTabButton = null;
    [SerializeField] private Button controlsTabButton = null;

    [Header("Audio Tab")]
    [SerializeField] private GameObject audioTabPanel = null;
    [SerializeField] private Button musicStateButton = null;
    [SerializeField] private Slider musicVolumeSlider = null;
    [SerializeField] private Button sfxStateButton = null;
    [SerializeField] private Slider sfxVolumeSlider = null;

    [Header("Controls Tab")]
    [SerializeField] private GameObject controlsTabPanel = null;
    [SerializeField] private Slider mouseSensitivityXSlider = null;
    [SerializeField] private Slider mouseSensitivityYSlider = null;

    [Header("Audio Config")]
    [SerializeField] private AudioManager audioManager = null;
    [SerializeField] private string clickEvent = null;

    private void Awake()
    {
        //Game Pause
        pauseMenuPanel.SetActive(true);
        settingsMenuPanel.SetActive(false);

        pauseMenuButton.onClick.AddListener(() => { OpenTab(pauseMenuPanel, true); OpenTab(settingsMenuPanel, false); OpenTab(audioTabPanel, false); OpenTab(controlsTabPanel, false); });
        settingsButton.onClick.AddListener(() => { OpenTab(settingsMenuPanel, true); OpenTab(pauseMenuPanel, false); OpenTab(audioTabPanel, false); OpenTab(controlsTabPanel, false); });

        //Pause Menu
        backToLobbyPanel.SetActive(false);

        resumeGameButton.onClick.AddListener(() => { gameStateManager.TogglePause(); ToggleCanvases(); audioManager.PlaySound(clickEvent); });
        backToLobbyButton.onClick.AddListener(() => { OpenTab(backToLobbyPanel, true); });
        yesBackToLobbyButton.onClick.AddListener(() => { mySceneManager.LoadSceneByName(lobbySceneName); audioManager.PlaySound(clickEvent); });
        noBackToLobbyButton.onClick.AddListener(() => { OpenTab(backToLobbyPanel, false); });

        //Settings
        audioTabButton.onClick.AddListener(() => { OpenTab(audioTabPanel, true); OpenTab(controlsTabPanel, false); });
        controlsTabButton.onClick.AddListener(() => { OpenTab(controlsTabPanel, true); OpenTab(audioTabPanel, false); });

        //Audio
        musicStateButton.onClick.AddListener(() => { });
        sfxStateButton.onClick.AddListener(() => { });

        //Controls

    }

    private void OnEnable()
    {
        inputManager.PauseEvent += ToggleCanvases;
    }

    private void OnDisable()
    {
        inputManager.PauseEvent -= ToggleCanvases;
    }

    private void Start()
    {
        gameplayCanvasGO.SetActive(true);
        pauseCanvasGO.SetActive(false);
    }

    public void ToggleCanvases()
    {
        bool active = gameplayCanvasGO.activeSelf;
        gameplayCanvasGO.SetActive(!active);
        pauseCanvasGO.SetActive(active);
    }

    private void OpenTab(GameObject go, bool state)
    {
        go.SetActive(state);
        audioManager.PlaySound(clickEvent);
    }
}