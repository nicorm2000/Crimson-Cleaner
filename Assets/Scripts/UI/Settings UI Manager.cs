using UnityEngine;
using UnityEngine.UI;

public class SettingsUIManager : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private GameObject settingsTab = null;
    [SerializeField] private Button audioTabButton = null;
    [SerializeField] private Button controlsTabButton = null;
    [SerializeField] private Button backToLobbyButton = null;
    [SerializeField] private PlayerSensitivitySettings sensitivitySettings;

    [Header("Audio Tab")]
    [SerializeField] private GameObject audioTab = null;
    [SerializeField] private Button audioBackToSettingsButton = null;
    [SerializeField] private Button musicStateButton = null;
    [SerializeField] private Slider musicVolumeSlider = null;
    [SerializeField] private Button sfxStateButton = null;
    [SerializeField] private Slider sfxVolumeSlider = null;

    [Header("Controls Tab")]
    [SerializeField] private GameObject controlsTab = null;
    [SerializeField] private Button controlsBackToSettingsButton = null;
    [SerializeField] private Slider mouseSensitivitySlider = null;

    [Header("Audio Config")]
    [SerializeField] private AudioManager audioManager = null;
    [SerializeField] private string clickEvent = null;

    private const float alpha = 2.0f;

    private void Awake()
    {
        backToLobbyButton.onClick.AddListener(() => { OpenTab(settingsTab, false); });
        audioTabButton.onClick.AddListener(() => { OpenTab(audioTab, true); });
        controlsTabButton.onClick.AddListener(() => { OpenTab(controlsTab, true); });

        audioBackToSettingsButton.onClick.AddListener(() => { OpenTab(audioTab, false); });
        //musicStateButton.onClick.AddListener(() => { audioManager.PlaySound(clickEvent); });
        sfxStateButton.onClick.AddListener(() => { audioManager.PlaySound(clickEvent); audioManager.ToggleMute(); });

        controlsBackToSettingsButton.onClick.AddListener(() => { OpenTab(controlsTab, false); });

        //musicStateButton.onClick.AddListener(() => { });
        sfxStateButton.onClick.AddListener(() => { });

        mouseSensitivitySlider.minValue = 0f;
        mouseSensitivitySlider.maxValue = 2f;

        mouseSensitivitySlider.value = SensitivityToSliderValue(sensitivitySettings.sensitivity);

        mouseSensitivitySlider.onValueChanged.AddListener(OnSensitivityChanged);
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

    private void OnDestroy()
    {
        mouseSensitivitySlider.onValueChanged.RemoveListener(OnSensitivityChanged);
    }
}