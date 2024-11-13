using UnityEngine;
using System.Collections;

public class AnyKeyMenu : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private MySceneManager mySceneManager = null;
    [SerializeField] private string sceneName = null;
    [SerializeField] private Animator animator = null;
    [SerializeField] private float waitToLobby = 2f;

    [Header("Warning Manager Config")]
    [SerializeField] private WarningManager warningManager = null;
    [SerializeField] private float endBlackValue = 0f;
    [SerializeField] private float colorLerpDuration = 2f;
    [SerializeField] private float fadeDuration = 1f;

    [Header("Audio")]
    [SerializeField] private AudioManager audioManager = null;
    [SerializeField] private string gameIntroStop;

    private void Update()
    {
        if (mySceneManager == null) 
            return;

        if (Input.anyKeyDown)
        {
            animator.SetBool("GoToLobby", true);
            StartCoroutine(MenuToLobby(waitToLobby));
        }
    }

    private IEnumerator MenuToLobby(float duration)
    {
        StopCoroutine(warningManager.pressAnyKeyCoroutine);
        StartCoroutine(warningManager.FadeAlphaToZero(fadeDuration));
        StartCoroutine(warningManager.LerpColor(endBlackValue, colorLerpDuration));
        yield return new WaitForSeconds(duration);
        if (!AudioManager.muteSFX)
            audioManager.PlaySound(gameIntroStop);
        mySceneManager.LoadSceneByName("Lobby");
    }
}