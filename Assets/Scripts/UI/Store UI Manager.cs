using UnityEngine;
using UnityEngine.UI;

public class StoreUIManager : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private GameObject storeTab = null;
    [SerializeField] private Button backToLobbyButton = null;

    private void Awake()
    {
        storeTab.SetActive(false);

        backToLobbyButton.onClick.AddListener(() => { storeTab.SetActive(false); });
    }
}