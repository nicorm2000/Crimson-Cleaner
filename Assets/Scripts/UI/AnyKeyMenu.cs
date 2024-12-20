using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class AnyKeyMenu : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private MySceneManager mySceneManager = null;
    [SerializeField] private string sceneName = null;
    [SerializeField] private Animator animator = null;
    [SerializeField] private float waitToLobby = 2f;

    [Header("Warning Manager Config")]
    [SerializeField] private WarningManager warningManager = null;
    [SerializeField] private TextMeshProUGUI gameTitle = null;
    [SerializeField] private TextMeshProUGUI pressAnyKey = null;
    [SerializeField] private float endBlackValue = 0f;
    [SerializeField] private float colorLerpDuration = 2f;
    [SerializeField] private float fadeDuration = 1f;

    [Header("Audio")]
    [SerializeField] private AudioManager audioManager = null;
    [SerializeField] private string gameIntroStop;
    [SerializeField] private string warningToLobby;
    
    private bool isReady = false;

    private void Update()
    {
        if (mySceneManager == null) 
            return;

        if (Input.anyKeyDown && !isReady)
        {
            isReady = true;
            //animator.SetBool("GoToLobby", true);
            StartCoroutine(MenuToLobby(waitToLobby));
        }
    }

    private IEnumerator MenuToLobby(float duration)
    {
        if (!AudioManager.muteSFX)
            audioManager.PlaySound(warningToLobby);

        if (warningManager.pressAnyKeyCoroutine != null)
        {
            warningManager.StopCoroutine(warningManager.pressAnyKeyCoroutine);
            warningManager.pressAnyKeyCoroutine = null;
        }
        StartCoroutine(warningManager.FadeAlphaToZero(gameTitle, fadeDuration));
        StartCoroutine(warningManager.FadeAlphaToZero(pressAnyKey, fadeDuration));
        StartCoroutine(warningManager.LerpColor(endBlackValue, colorLerpDuration));
        
        yield return new WaitForSeconds(duration);
        
        if (!AudioManager.muteSFX)
            audioManager.PlaySound(gameIntroStop);
            
        mySceneManager.LoadSceneByName("Lobby");
    }
}