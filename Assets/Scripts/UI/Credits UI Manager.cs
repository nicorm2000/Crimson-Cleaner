using UnityEngine;
using UnityEngine.UI;

public class CreditsUIManager : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private GameObject creditsTab = null;
    [SerializeField] private Button backToLobbyButton = null;

    private void Awake()
    {
        creditsTab.SetActive(false);

        backToLobbyButton.onClick.AddListener(() => { creditsTab.SetActive(false); });
    }
}