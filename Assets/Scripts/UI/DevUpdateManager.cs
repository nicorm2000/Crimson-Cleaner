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

    private void Awake()
    {
        devUpdateGO.SetActive(false);
        changeLogVersionText.text= Application.version;

        exitDevUpdate.onClick.AddListener(() => { TurnOffDevlog(); audioManager.PlaySound(buttonClickEvent); });
    }

    public void TurnOnDevlog()
    {
        devUpdateGO.SetActive(true);
    }

    public void TurnOffDevlog()
    {
        devUpdateGO.SetActive(false);
    }
}