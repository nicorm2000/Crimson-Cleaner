using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleaningManagerMenu : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private Camera gameCamera = null;
    [SerializeField] private InputManager inputManager = null;
    [SerializeField] private ToolAnimatorController toolAnimatorController = null;

    [Header("Interaction")]
    [SerializeField] private float interactionDistance;

    [Header("Audio Config")]
    [SerializeField] private AudioManager audioManager = null;

    [Header("Hands")]
    [SerializeField] private string dropEvent = null;
    [SerializeField] private string pickUpEvent = null;
    [SerializeField] private string throwEvent = null;
    [SerializeField] private string handSwapEvent = null;
    [SerializeField] private string handSelectEvent = null;

    [Header("UI Config")]
    [SerializeField] private Sprite pickUpMessage;
    [SerializeField] private Sprite dropMessage;
    [SerializeField] private Sprite throwMessage;
    [SerializeField] private Sprite rotateMessage;
    [SerializeField] private Sprite storeMessage;
    [SerializeField] private Sprite interactMessage;
    [SerializeField] private Sprite retrieveMessage;

    public static CleaningManagerMenu Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public Camera GetCamera() => gameCamera;
    public InputManager GetInputManager() => inputManager;
    public float GetInteractionDistance() => interactionDistance;
    public AudioManager GetAudioManager() => audioManager;
    public ToolAnimatorController GetToolAnimator() => toolAnimatorController;

    public string GetDropEvent() => dropEvent;
    public string GetPickUpEvent() => pickUpEvent;
    public string GetThrowEvent() => throwEvent;
    public string GetHandSwapEvent() => handSwapEvent;
    public string GetHandSelectEvent() => handSelectEvent;

    public Sprite GetPickUpMessage() => pickUpMessage;
    public Sprite GetDropMessage() => dropMessage;
    public Sprite GetThrowMessage() => throwMessage;
    public Sprite GetRotateMessage() => rotateMessage;
    public Sprite GetStoreMessage() => storeMessage;
    public Sprite GetInteractMessage() => interactMessage;
    public Sprite GetRetrieveMessage() => retrieveMessage;
}
