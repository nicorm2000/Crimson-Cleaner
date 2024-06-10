using UnityEngine;
using UnityEngine.UI;

public class SettingsUIManager : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private GameObject settingsTab = null;
    [SerializeField] private Button audioTabButton = null;
    [SerializeField] private Button controlsTabButton = null;
    [SerializeField] private Button backToLobbyButton = null;

    [Header("Audio")]
    [SerializeField] private GameObject audioTab = null;
    [SerializeField] private Button audioBackToSettingsButton = null;
    [SerializeField] private Button musicStateButton = null;
    [SerializeField] private Slider musicVolumeSlider = null;
    [SerializeField] private Button sfxStateButton = null;
    [SerializeField] private Slider sfxVolumeSlider = null;

    [Header("Controls")]
    [SerializeField] private GameObject controlsTab = null;
    [SerializeField] private Button controlsBackToSettingsButton = null;
    [SerializeField] private Slider mouseSensitivityXSlider = null;
    [SerializeField] private Slider mouseSensitivityYSlider = null;

    private void Awake()
    {
        settingsTab.SetActive(false);
        audioTab.SetActive(false);
        controlsTab.SetActive(false);

        backToLobbyButton.onClick.AddListener(() => { settingsTab.SetActive(false); });
        audioTabButton.onClick.AddListener(() => { audioTab.SetActive(true); });
        controlsTabButton.onClick.AddListener(() => { controlsTab.SetActive(true); });

        audioBackToSettingsButton.onClick.AddListener(() => { audioTab.SetActive(false); });
        musicStateButton.onClick.AddListener(() => { });
        sfxStateButton.onClick.AddListener(() => { });

        controlsBackToSettingsButton.onClick.AddListener(() => { controlsTab.SetActive(false); });
    }
}