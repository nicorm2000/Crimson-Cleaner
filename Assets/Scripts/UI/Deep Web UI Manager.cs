using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DeepWebUIManager : MonoBehaviour
{

    [Header("Config")]
    [SerializeField] private PCCanvasController pCCanvasController;
    [SerializeField] private GameObject key;
    [SerializeField] private float grabKeyAnimationDuration = 2f;
    [SerializeField] private float grabKeySoundEventDelay = 0.5f;
    [SerializeField] private MySceneManager mySceneManager;
    [SerializeField] private GameObject deepWebTab = null;
    [SerializeField] private GameObject mainMenuCanvas = null;
    [SerializeField] private Button backToLobbyButton = null;
    [SerializeField] private Button level1Button = null;

    [Header("Audio Config")]
    [SerializeField] private AudioManager audioManager = null;
    [SerializeField] private string clickEvent = null;
    [SerializeField] private string vanCloseEvent = null;
    [SerializeField] private string vanHonkEvent = null;
    [SerializeField] private string retreiveKeyEvent = null;

    private bool isLevelSelected = false;

    public event System.Action LevelAccepted;

    private void Awake()
    {
        level1Button.onClick.AddListener(() => AcceptLevel());
        backToLobbyButton.onClick.AddListener(() => { OpenTab(deepWebTab, false); });
    }

    private void AcceptLevel()
    {
        if (isLevelSelected) return;
            StartCoroutine(GrabKeyAnimation());
    }

    private IEnumerator GrabKeyAnimation()
    {
        isLevelSelected = true;
        audioManager.PlaySound(clickEvent);
        float elapsedTime = 0f;
        bool vanClosed = false;
        bool vanHonkPlayed = false;
        bool retrieveKeyPlayed = false;

        while (elapsedTime < grabKeyAnimationDuration)
        {
            elapsedTime += Time.deltaTime;

            if (!vanClosed && elapsedTime >= grabKeyAnimationDuration / 4)
            {
                vanClosed = true;

                if (audioManager != null && vanCloseEvent != null)
                    audioManager.PlaySound(vanCloseEvent);
            }

            if (!vanHonkPlayed && elapsedTime >= grabKeyAnimationDuration / 2)
            {
                vanHonkPlayed = true;

                if (audioManager != null && vanHonkEvent != null)
                    audioManager.PlaySound(vanHonkEvent);
            }

            if (!retrieveKeyPlayed && elapsedTime >= grabKeyAnimationDuration / 2 + grabKeySoundEventDelay)
            {
                retrieveKeyPlayed = true;
                if (audioManager != null && retreiveKeyEvent != null)
                {
                    audioManager.PlaySound(retreiveKeyEvent);

                    // REMEBER TO MOVE THIS TO HAND COLLISION
                    key.GetComponent<Key>().isKeyPickedUp = true;
                    key.SetActive(false);
                }
            }

            yield return null;
        }

        if (pCCanvasController.isPlayerOnPC)
            pCCanvasController.ShutDownPC();

        mainMenuCanvas.SetActive(false);
        LevelAccepted?.Invoke();

    }

    private void OpenTab(GameObject go, bool state)
    {
        go.SetActive(state);
        audioManager.PlaySound(clickEvent);
    }
}