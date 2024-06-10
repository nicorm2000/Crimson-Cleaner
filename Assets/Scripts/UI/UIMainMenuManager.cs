using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Job
{
    public string jobName;
    public Sprite jobImage;
    public string jobDescription;
    public int jobPayment;
    public int jobCompletionTimeEasy;
    public int jobCompletionTimeMedium;
    public int jobCompletionTimeHard;
    public GameObject jobButton;
}

public class UIMainMenuManager : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private TextMeshProUGUI gameTime = null;
    [SerializeField] private Job[] jobAvailables = null;
    [SerializeField] private Image jobImage = null;
    [SerializeField] private TextMeshProUGUI jobNameText = null;
    [SerializeField] private TextMeshProUGUI jobDescriptionText = null;

    [Header("Navigation Buttons")]
    [SerializeField] private Button leftButton = null;
    [SerializeField] private Button rightButton = null;

    [Header("Settings")]
    [SerializeField] private GameObject settingsPanel = null;
    [SerializeField] private Button settingsButton = null;

    [Header("Credits")]
    [SerializeField] private GameObject creditsPanel = null;
    [SerializeField] private Button creditsButton = null;

    [Header("Store")]
    [SerializeField] private GameObject storePanel = null;
    [SerializeField] private Button storeButton = null;

    [Header("Deep Web")]
    [SerializeField] private GameObject deepWebPanel = null;
    [SerializeField] private Button deepWebButton = null;

    [Header("Exit Game")]
    [SerializeField] private MySceneManager mySceneManager = null;
    [SerializeField] private GameObject exitPanel = null;
    [SerializeField] private Button exitButton = null;
    [SerializeField] private Button yesExitButton = null;
    [SerializeField] private Button noExitButton = null;

    private int currentIndex = 0;

    private void Awake()
    {
        exitPanel.SetActive(false);
        settingsPanel.SetActive(false);
        creditsPanel.SetActive(false);
        storePanel.SetActive(false);
        deepWebPanel.SetActive(false);

        leftButton.onClick.AddListener(NavigateLeft);
        rightButton.onClick.AddListener(NavigateRight);

        deepWebButton.onClick.AddListener(() => { deepWebPanel.SetActive(true); });
        
        storeButton.onClick.AddListener(() => { storePanel.SetActive(true); });
        
        creditsButton.onClick.AddListener(() => { creditsPanel.SetActive(true); });
        
        settingsButton.onClick.AddListener(() => { settingsPanel.SetActive(true); });
        
        exitButton.onClick.AddListener(() => { exitPanel.SetActive(true); });
        yesExitButton.onClick.AddListener(() => { mySceneManager.Exit(); });
        noExitButton.onClick.AddListener(() => { exitPanel.SetActive(false); });
    }

    private void Start()
    {
        UpdateJob();
    }

    private void Update()
    {
        LogCurrentTime();
    }

    private void LogCurrentTime()
    {
        DateTime now = DateTime.Now;
        string formattedTime = now.ToString("hh:mm tt");
        gameTime.text = formattedTime;
    }

    private void NavigateRight()
    {
        currentIndex = (currentIndex + 1) % jobAvailables.Length;
        UpdateJob();
    }

    private void NavigateLeft()
    {
        currentIndex = (currentIndex - 1 + jobAvailables.Length) % jobAvailables.Length;
        UpdateJob();
    }

    private void UpdateJob()
    {
        if (jobAvailables.Length == 0) return;

        jobNameText.text = jobAvailables[currentIndex].jobName;
        jobImage.sprite = jobAvailables[currentIndex].jobImage;
        jobDescriptionText.text = jobAvailables[currentIndex].jobDescription;

        for (int i = 0; i < jobAvailables.Length; i++)
        {
            jobAvailables[i].jobButton.SetActive(i == currentIndex);
        }
    }
}