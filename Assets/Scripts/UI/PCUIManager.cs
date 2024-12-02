using IE.RichFX;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;
using UnityEngine.UI;

[System.Serializable]
public class Job
{
    public string jobName;
    public Sprite jobImage;
    public string jobAddress;
    public string jobInformation;
    public int jobPayment;
    public int jobCompletionTime;
    public GameObject jobButton;
}

public class PCUIManager : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private Job[] jobAvailables = null;
    [SerializeField] private Image jobImage = null;
    [SerializeField] private TextMeshProUGUI jobNameText = null;
    [SerializeField] private TextMeshProUGUI jobAddressText = null;
    [SerializeField] private TextMeshProUGUI jobInformationText = null;
    [SerializeField] private TextMeshProUGUI jobCompletionTimeText = null;
    [SerializeField] private TextMeshProUGUI jobPaymentText = null;

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

    [Header("New Exit Game")]
    [SerializeField] private GameObject newExitPanel = null;
    [SerializeField] private Button newYesExitButton = null;
    [SerializeField] private Button newNoExitButton = null;
    [SerializeField] private Volume sceneVolume = null;
    [SerializeField] private float duration = 0.5f;
    [SerializeField] private float startValueON = 0.3f;
    [SerializeField] private float startValueOFF = 0.0f;
    [SerializeField] private float endValueON = 0.3f;
    [SerializeField] private float endValueOFF = 0.0f;

    [Header("Audio Config")]
    [SerializeField] private AudioManager audioManager = null;
    [SerializeField] private string clickEvent = null;

    private int currentIndex = 0;

    private void Awake()
    {
        exitPanel.SetActive(false);
        settingsPanel.SetActive(false);
        creditsPanel.SetActive(false);
        storePanel.SetActive(false);
        deepWebPanel.SetActive(false);

        leftButton.onClick.AddListener(() => { NavigateLeft(); audioManager.PlaySound(clickEvent); });
        rightButton.onClick.AddListener(() => { NavigateRight(); audioManager.PlaySound(clickEvent); });

        deepWebButton.onClick.AddListener(() => { OpenTab(deepWebPanel, true); });
        storeButton.onClick.AddListener(() => { OpenTab(storePanel, true); });        
        creditsButton.onClick.AddListener(() => { OpenTab(creditsPanel, true); });        
        settingsButton.onClick.AddListener(() => { OpenTab(settingsPanel, true); });
        //exitButton.onClick.AddListener(() => { OpenTab(exitPanel, true); });

        //yesExitButton.onClick.AddListener(() => { mySceneManager.Exit(); audioManager.PlaySound(clickEvent); });
        //noExitButton.onClick.AddListener(() => { OpenTab(exitPanel, false); });

        //REMEMBER TO DO THE VOLUME UPDATE WITH THE ESC KEY
        //newYesExitButton.onClick.AddListener(() => { mySceneManager.Exit(); audioManager.PlaySound(clickEvent); });
        //newNoExitButton.onClick.AddListener(() => { SimpleGaussianBlurState(duration, startValueOFF, endValueON); OpenTab(newExitPanel, false); });

        //Cursor.visible = true;
        //Cursor.lockState = CursorLockMode.Confined;
    }

    private void Start()
    {
        UpdateJob();
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

    private void SimpleGaussianBlurState(float duration, float startValue, float endValue)
    {
        if (sceneVolume.profile.TryGet(out SimpleGaussianBlur simpleGaussianBlur))
            StartCoroutine(UpdateSimpleGaussianBlur(simpleGaussianBlur, duration, startValue, endValue));
    }

    private IEnumerator UpdateSimpleGaussianBlur(SimpleGaussianBlur simpleGaussianBlur, float duration, float startValue, float endValue)
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float newIntensity = Mathf.Lerp(startValue, endValue, elapsedTime / duration);
            simpleGaussianBlur.intensity.value = newIntensity;
            yield return null;
        }

        simpleGaussianBlur.intensity.value = endValue;
    }

    private void UpdateJob()
    {
        if (jobAvailables.Length == 0) return;

        jobNameText.text = jobAvailables[currentIndex].jobName;
        jobImage.sprite = jobAvailables[currentIndex].jobImage;
        jobAddressText.text = jobAvailables[currentIndex].jobAddress;
        jobInformationText.text = jobAvailables[currentIndex].jobInformation;
        jobCompletionTimeText.text = jobAvailables[currentIndex].jobCompletionTime.ToString() + " minutes";
        jobPaymentText.text = "$" + jobAvailables[currentIndex].jobPayment.ToString("N2");

        for (int i = 0; i < jobAvailables.Length; i++)
        {
            jobAvailables[i].jobButton.SetActive(i == currentIndex);
        }
    }

    private void OpenTab(GameObject go, bool state)
    {
        go.SetActive(state);
        audioManager.PlaySound(clickEvent);
    }
}