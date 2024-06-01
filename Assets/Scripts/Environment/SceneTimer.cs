using TMPro;
using UnityEngine;

public class SceneTimer : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private float totalTimeInSeconds;
    [SerializeField] private TextMeshProUGUI countdownText;
    
    private GameStateManager gameStateManager;
    private float _currentTimeInSeconds;

    private void Start()
    {
        _currentTimeInSeconds = totalTimeInSeconds;

        UpdateTimerDisplay();

        gameStateManager = FindObjectOfType<GameStateManager>();
    }

    private void UpdateTimerDisplay()
    {
        int minutes = Mathf.FloorToInt(_currentTimeInSeconds / 60);
        int seconds = Mathf.FloorToInt(_currentTimeInSeconds % 60);
        countdownText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void StartTimer()
    {
        InvokeRepeating(nameof(UpdateTimer), 1f, 1f);
    }

    private void UpdateTimer()
    {
        _currentTimeInSeconds -= 1f;
        UpdateTimerDisplay();

        if (_currentTimeInSeconds <= 0f)
        {
            CancelInvoke(nameof(UpdateTimer));
            gameStateManager.OnTimerFinished();
        }
    }

    public void ShowCursor()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}