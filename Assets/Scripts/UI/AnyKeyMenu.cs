using UnityEngine;
using System.Collections;

public class AnyKeyMenu : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private MySceneManager mySceneManager = null;
    [SerializeField] private string sceneName = null;
    [SerializeField] private Animator animator = null;
    [SerializeField] private float waitToLoby = 3f;

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
            StartCoroutine(MenuToLobby(waitToLoby));
        }
    }

    private IEnumerator MenuToLobby(float duration)
    {
        yield return new WaitForSeconds(duration);
        if (!AudioManager.muteSFX)
            audioManager.PlaySound(gameIntroStop);
        mySceneManager.LoadSceneByName("Lobby");
    }
}