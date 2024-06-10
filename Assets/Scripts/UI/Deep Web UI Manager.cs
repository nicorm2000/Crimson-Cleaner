using UnityEngine;
using UnityEngine.UI;

public class DeepWebUIManager : MonoBehaviour
{

    [Header("Config")]
    [SerializeField] private MySceneManager mySceneManager = null;
    [SerializeField] private GameObject deepWebTab = null;
    [SerializeField] private Button backToLobbyButton = null;
    [SerializeField] private Button level1Button = null;
    [SerializeField] private string level1Name = null;
    [SerializeField] private Button level2Button = null;
    [SerializeField] private string level2Name = null;
    [SerializeField] private Button level3Button = null;
    [SerializeField] private string level3Name = null;

    //[Header("Jobs")]
    //[SerializeField] private Jobs[] jobAvailables = null;
    //[SerializeField] private Image jobImage = null;
    //[SerializeField] private TextMeshProUGUI jobNameText = null;
    //[SerializeField] private TextMeshProUGUI jobDescriptionText = null;

    //private int currentIndex = 0;

    private void Awake()
    {
        deepWebTab.SetActive(false);

        level1Button.onClick.AddListener(() => { mySceneManager.LoadSceneByName(level1Name); });
        level2Button.onClick.AddListener(() => { mySceneManager.LoadSceneByName(level2Name); });
        level3Button.onClick.AddListener(() => { mySceneManager.LoadSceneByName(level3Name); });

        backToLobbyButton.onClick.AddListener(() => { deepWebTab.SetActive(false); });
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
}