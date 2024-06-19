using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DevUpdateManager : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private GameObject devUpdateGO = null;
    [SerializeField] private Button exitDevUpdate = null;
    [SerializeField] private TextMeshProUGUI changeLogVersionText = null;
    [SerializeField] private TextMeshProUGUI changeLogText = null;

    [Header("Audio Config")]
    [SerializeField] private AudioManager audioManager = null;
    [SerializeField] private string buttonClickEvent = null;

    private const string FirstTimeKey = "FirstTimeTurnOnDevlog";

    private void Awake()
    {
        TurnOffDevlog();
        changeLogVersionText.text = Application.version;

        exitDevUpdate.onClick.AddListener(() => { TurnOffDevlog(); audioManager.PlaySound(buttonClickEvent); });
    }

    public void TurnOnDevlog()
    {
        if (PlayerPrefs.GetInt(FirstTimeKey, 0) == 0)
        {
            PlayerPrefs.SetInt(FirstTimeKey, 1);
            PlayerPrefs.Save();

            Debug.Log("TurnOnDevlog called for the first time.");
            devUpdateGO.SetActive(true);
        }
    }

    public void TurnOffDevlog()
    {
        devUpdateGO.SetActive(false);
    }
}