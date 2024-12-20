using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private InputManager inputManager;
    [SerializeField] private GameObject gameplayCanvasGO;
    [SerializeField] private GameObject pauseCanvasGO;
    [SerializeField] private GameStateManager gameStateManager = null;
    [SerializeField] private PlayerSensitivitySettings sensitivitySettings;

    [Header("Game Pause")]
    [SerializeField] private Button pauseMenuButton = null;
    [SerializeField] private Button settingsButton = null;
    [SerializeField] private GameObject pauseMenuPanel = null;
    [SerializeField] private GameObject settingsMenuPanel = null;

    [Header("Resume")]
    [SerializeField] private Button resumeGameButton = null;

    [Header("Back To Lobby")]
    [SerializeField] private GameObject backToLobbyPanel = null;
    [SerializeField] private Button backToLobbyButton = null;
    [SerializeField] private Button yesBackToLobbyButton = null;
    [SerializeField] private Button noBackToLobbyButton = null;
    [SerializeField] private string lobbySceneName = null;
    [SerializeField] private Animator endGameAnimator = null;
    [SerializeField] private string endGameTriggername = null;

    [Header("Settings")]
    [SerializeField] private Button audioTabButton = null;
    [SerializeField] private Button controlsTabButton = null;

    [Header("Audio Tab")]
    [SerializeField] private GameObject audioTabPanel = null;
    [SerializeField] private Button musicStateButton = null;
    [SerializeField] private Slider musicVolumeSlider = null;
    [SerializeField] private Button sfxStateButton = null;
    [SerializeField] private Slider sfxVolumeSlider = null;
    [SerializeField] private TextMeshProUGUI sfxCrossText;

    [Header("Controls Tab")]
    [SerializeField] private GameObject controlsTabPanel = null;
    [SerializeField] private Slider mouseSensitivitySlider = null;
    
    [Header("Tutorial")]
    [SerializeField] private GameObject tutorialImage;
    [SerializeField] private GameObject tutorialControlHint;
    [SerializeField] private float tutorialHintDuration = 10f;

    [Header("Audio Config")]
    [SerializeField] private AudioManager audioManager = null;
    [SerializeField] private string clickEvent = null;

    [Header("Water Faucet System")]
    [SerializeField] private WaterFaucetSystem waterFaucetSystem = null;

    private const float alpha = 2.0f;

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
        yesBackToLobbyButton.onClick.AddListener(() => { audioManager.PlaySound(waterFaucetSystem.waterFlowStopEvent); audioManager.PlaySound(clickEvent); TriggerEndGame(); });
        noBackToLobbyButton.onClick.AddListener(() => { OpenTab(backToLobbyPanel, false); });

        //Settings
        audioTabButton.onClick.AddListener(() => { OpenTab(audioTabPanel, true); OpenTab(controlsTabPanel, false); });
        controlsTabButton.onClick.AddListener(() => { OpenTab(controlsTabPanel, true); OpenTab(audioTabPanel, false); });

        //Audio
        musicStateButton.onClick.AddListener(() => { });
        sfxStateButton.onClick.AddListener(() => {  audioManager.ToggleMute(); ToggleSFXCrossText(); });

        //Controls
        mouseSensitivitySlider.minValue = 0f;
        mouseSensitivitySlider.maxValue = 2f;

        mouseSensitivitySlider.value = SensitivityToSliderValue(sensitivitySettings.sensitivity);

        mouseSensitivitySlider.onValueChanged.AddListener(OnSensitivityChanged);

        //Tutorial
        tutorialImage.SetActive(false);
        ToggleSFXCrossText();
    }

    private void OnEnable()
    {
        inputManager.PauseEvent += ToggleCanvases;
        inputManager.DisplayTutorialEvent += TriggerTutorialUI;
    }

    private void OnDisable()
    {
        inputManager.PauseEvent -= ToggleCanvases;
        inputManager.DisplayTutorialEvent -= TriggerTutorialUI;
    }

    private void Start()
    {
        gameplayCanvasGO.SetActive(true);
        pauseCanvasGO.SetActive(false);
        StartCoroutine(WaitDisplayTutorialHint());
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

    private void OnSensitivityChanged(float value)
    {
        sensitivitySettings.sensitivity = ApplyExponentialScaling(value);
    }

    private float ApplyExponentialScaling(float sliderValue)
    {
        return Mathf.Exp(alpha * sliderValue) - 1;
    }

    private float SensitivityToSliderValue(float sensitivity)
    {
        return Mathf.Log(sensitivity + 1f) / alpha;
    }

    private void TriggerTutorialUI()
    {
        tutorialImage.SetActive(!tutorialImage.activeSelf);
    }

    private IEnumerator WaitDisplayTutorialHint()
    {
        tutorialControlHint.SetActive(true);
        yield return new WaitForSeconds(tutorialHintDuration);
        tutorialControlHint.SetActive(false);
    }

    public void TriggerEndGame()
    {
        ToggleCanvases();
        endGameAnimator.SetTrigger(endGameTriggername);

        AnimatorStateInfo stateInfo = endGameAnimator.GetCurrentAnimatorStateInfo(0);

        float animationDuration = stateInfo.length;
        float offset = 0.1f;

        Invoke("TriggerDeInit", animationDuration - offset);
    }

    private void TriggerDeInit()
    {
        gameStateManager.TransitionToState("DeInit");
    }

    private void ToggleSFXCrossText()
    {
        sfxCrossText.text = AudioManager.muteSFX ? "OFF" : "ON";
    }

    private void OnDestroy()
    {
        mouseSensitivitySlider.onValueChanged.RemoveListener(OnSensitivityChanged);
    }
}