using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private InputManager inputManager;
    [SerializeField] private GameObject gameplayCanvasGO;
    [SerializeField] private GameObject pauseCanvasGO;
    [SerializeField] private GameStateManager gameStateManager = null;

    [Header("Resume Game")]
    [SerializeField] private Button resumeGameButton = null;

    [Header("Back To Lobby")]
    [SerializeField] private MySceneManager mySceneManager = null;
    [SerializeField] private GameObject backToLobbyPanel = null;
    [SerializeField] private Button backToLobbyButton = null;
    [SerializeField] private Button yesBackToLobbyButton = null;
    [SerializeField] private Button noBackToLobbyButton = null;
    [SerializeField] private string lobbySceneName = null;

    private void Awake()
    {
        resumeGameButton.onClick.AddListener(() => { gameStateManager.TogglePause(); ToggleCanvases(); });
        backToLobbyButton.onClick.AddListener(() => { backToLobbyPanel.SetActive(true); });
        yesBackToLobbyButton.onClick.AddListener(() => { mySceneManager.LoadSceneByName(lobbySceneName); });
        noBackToLobbyButton.onClick.AddListener(() => { backToLobbyPanel.SetActive(false); });
        backToLobbyPanel.SetActive(false);
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
}
