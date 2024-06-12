using UnityEngine;
using UnityEngine.UI;

public class CreditsUIManager : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private GameObject creditsTab = null;
    [SerializeField] private Button backToLobbyButton = null;

    [Header("Audio Config")]
    [SerializeField] private AudioManager audioManager = null;
    [SerializeField] private string clickEvent = null;

    private void Awake()
    {
        backToLobbyButton.onClick.AddListener(() => { OpenTab(creditsTab, false); });
    }

    private void OpenTab(GameObject go, bool state)
    {
        go.SetActive(state);
        audioManager.PlaySound(clickEvent);
    }
}