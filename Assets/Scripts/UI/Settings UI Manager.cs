using UnityEngine;
using UnityEngine.UI;

public class SettingsUIManager : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private GameObject settingsTab = null;
    [SerializeField] private Button audioTabButton = null;
    [SerializeField] private Button controlsTabButton = null;
    [SerializeField] private Button backToLobbyButton = null;

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
    [SerializeField] private Slider mouseSensitivityXSlider = null;
    [SerializeField] private Slider mouseSensitivityYSlider = null;

    [Header("Audio Config")]
    [SerializeField] private AudioManager audioManager = null;
    [SerializeField] private string clickEvent = null;

    private void Awake()
    {
        backToLobbyButton.onClick.AddListener(() => { OpenTab(settingsTab, false); });
        audioTabButton.onClick.AddListener(() => { OpenTab(audioTab, true); });
        controlsTabButton.onClick.AddListener(() => { OpenTab(controlsTab, true); });

        audioBackToSettingsButton.onClick.AddListener(() => { OpenTab(audioTab, false); });
        musicStateButton.onClick.AddListener(() => { audioManager.PlaySound(clickEvent); });
        sfxStateButton.onClick.AddListener(() => { audioManager.PlaySound(clickEvent); });

        controlsBackToSettingsButton.onClick.AddListener(() => { OpenTab(controlsTab, false); });
    }

    private void OpenTab(GameObject go, bool state)
    {
        go.SetActive(state);
        audioManager.PlaySound(clickEvent);
    }
}