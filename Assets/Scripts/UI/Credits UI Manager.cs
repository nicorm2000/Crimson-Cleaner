using UnityEngine;
using UnityEngine.UI;

public class CreditsUIManager : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private GameObject creditsTab = null;
    [SerializeField] private Button backToLobbyButton = null;
    [SerializeField] private RectTransform creditsText = null;
    [SerializeField] private float duration = 0;
    [SerializeField] private float startY = 0;
    [SerializeField] private float endY = 0;

    [Header("Audio Config")]
    [SerializeField] private AudioManager audioManager = null;
    [SerializeField] private string clickEvent = null;

    private float elapsedTime;

    private void Awake()
    {
        backToLobbyButton.onClick.AddListener(() => { OpenTab(creditsTab, false); });
    }

    private void OnEnable()
    {
        elapsedTime = 0f;
        creditsText.anchoredPosition = new Vector2(creditsText.anchoredPosition.x, startY);
    }

    private void Update()
    {
        if (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float progress = elapsedTime / duration;
            float newY = Mathf.Lerp(startY, endY, progress);
            creditsText.anchoredPosition = new Vector2(creditsText.anchoredPosition.x, newY);
        }
    }

    private void OpenTab(GameObject go, bool state)
    {
        go.SetActive(state);
        audioManager.PlaySound(clickEvent);
    }
}