using UnityEngine;
using UnityEngine.UI;

public class DeepWebUIManager : MonoBehaviour
{

    [Header("Config")]
    [SerializeField] private GameObject deepWebTab = null;
    [SerializeField] private GameObject mainMenuCanvas = null;
    [SerializeField] private Button backToLobbyButton = null;
    [SerializeField] private Button level1Button = null;
    [SerializeField] private string level1Name = null;

    [Header("Audio Config")]
    [SerializeField] private AudioManager audioManager = null;
    [SerializeField] private string clickEvent = null;

    //[Header("Jobs")]
    //[SerializeField] private Jobs[] jobAvailables = null;
    //[SerializeField] private Image jobImage = null;
    //[SerializeField] private TextMeshProUGUI jobNameText = null;
    //[SerializeField] private TextMeshProUGUI jobDescriptionText = null;

    //private int currentIndex = 0;

    private void Awake()
    {
        level1Button.onClick.AddListener(() => { MySceneManager.Instance.LoadSceneByNameAsync(level1Name); audioManager.PlaySound(clickEvent);mainMenuCanvas.SetActive(false); });
        backToLobbyButton.onClick.AddListener(() => { OpenTab(deepWebTab, false); });
    }

    //private void Start()
    //{
    //    UpdateJob();
    //}
    //
    //private void NavigateRight()
    //{
    //    currentIndex = (currentIndex + 1) % jobAvailables.Length;
    //    UpdateJob();
    //}
    //
    //private void NavigateLeft()
    //{
    //    currentIndex = (currentIndex - 1 + jobAvailables.Length) % jobAvailables.Length;
    //    UpdateJob();
    //}
    //
    //private void UpdateJob()
    //{
    //    if (jobAvailables.Length == 0) return;
    //
    //    jobNameText.text = jobAvailables[currentIndex].jobName;
    //    jobImage.sprite = jobAvailables[currentIndex].jobImage;
    //    jobDescriptionText.text = jobAvailables[currentIndex].jobDescription;
    //
    //    for (int i = 0; i < jobAvailables.Length; i++)
    //    {
    //        jobAvailables[i].jobButton.SetActive(i == currentIndex);
    //    }
    //}

    private void OpenTab(GameObject go, bool state)
    {
        go.SetActive(state);
        audioManager.PlaySound(clickEvent);
    }
}