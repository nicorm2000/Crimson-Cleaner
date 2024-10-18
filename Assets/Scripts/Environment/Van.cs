using System.Collections;
using UnityEngine;

public class Van : Interactable, IInteractable
{
    [Header("Config")]
    //[SerializeField] private MySceneManager mySceneManager;
    //[SerializeField] private string level1Name;
    [SerializeField] private PlayerControllerMenu playerController;
    [SerializeField] private Animator changeSceneAnimator;
    [SerializeField] private string changeSceneTrigger;
    [SerializeField] private Animator endGameAnimator;
    [SerializeField] private string endGameTrigger;
    //[SerializeField] private float changeSceneAnimationDuration = 2f;
    [SerializeField] private float startEngineDelay = 1f;
    [SerializeField] private float stopEngineDelay = 2f;
    [SerializeField] private float garageDoorDelay = 1f;

    [Header("Audio")]
    [SerializeField] private string engineStartEvent = null;
    [SerializeField] private string engineStopEvent = null;
    [SerializeField] private string garageDoorEvent = null;
    
    [Header("Keys Config")]
    [SerializeField] private KeysManager keysManager;
    [SerializeField] private Key[] requiredKeys;
    public event System.Action ungrabbedKey;

    [SerializeField] private Sprite interactSprite;
    public Sprite InteractMessage => interactSprite;

    public bool isSceneTransitioned = false;
    public event System.Action SceneTransitioned;

    public void Interact(PlayerController playerController)
    {
        if (keysManager != null)
        {
            if (requiredKeys.Length > 0)
            {
                if (CheckGrabbedKeys())
                {
                    StartCoroutine(TriggerSceneTransition());
                    return;
                }
                else
                {
                    ungrabbedKey?.Invoke();
                    return;
                }
            }
            else
            {
                StartCoroutine(TriggerSceneTransition());
            }
        }
        else
        {
            StartCoroutine(TriggerSceneTransition());
        }
    }

    protected override void PerformInteraction(PlayerController playerController)
    {
        Interact(playerController);
    }
    
    private IEnumerator TriggerSceneTransition()
    {
        isSceneTransitioned = true;
        SceneTransitioned?.Invoke();
        playerController.ToggleMovement(false);
        playerController.ToggleCameraMovement(false);

        yield return new WaitForSeconds(startEngineDelay);

        TriggerEventSound(engineStartEvent);

        yield return new WaitForSeconds(garageDoorDelay);

        TriggerEventSound(garageDoorEvent); 
        TriggerFadeOut();

        // Add engine stop loop if necessary
    }

    private bool CheckGrabbedKeys()
    {
        int keysCounter = 0;

        foreach (var key in keysManager.GetKeysList())
        {
            for (int i = 0; i < requiredKeys.Length; i++)
            {
                if (key == requiredKeys[i])
                {
                    if (key.isKeyPickedUp)
                    {
                        keysCounter++;
                    }
                    else
                    {
                        Debug.Log("Missing Key: " + key.name);
                    }
                }
            }
        }

        if (keysCounter == requiredKeys.Length)
        {
            return true;
        }

        return false;
    }

    public void TriggerEventSound(string eventName)
    {
        if (audioManager != null && eventName != null)
            audioManager.PlaySound(eventName);
    }

    public void TriggerFadeOut()
    {
        endGameAnimator.SetTrigger(endGameTrigger);
    }
}
