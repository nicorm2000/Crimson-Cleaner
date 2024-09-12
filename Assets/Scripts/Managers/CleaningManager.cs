using UnityEngine;

[System.Serializable]
public class CleanableObjectState
{
    public string objectName;
    public GameObject cleanObject;
    public GameObject dirtyObject;
    public float cleanliness;

    public CleanableObjectState(string objectName,GameObject cleanObject, GameObject dirtyObject, float cleanliness)
    {
        this.objectName = objectName;
        this.cleanObject = cleanObject;
        this.dirtyObject = dirtyObject;
        this.cleanliness = cleanliness;
    }
}

public enum Tools
{
    Mop = 0,
    Sponge,
    Hands,
    Tablet,
    Bin
}

public class CleaningManager : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private Camera gameCamera = null;
    [SerializeField] private InputManager inputManager = null;
    [SerializeField] private CleaningTool cleaningTool = null;
    [SerializeField] private CleaningToolReceiver mopToolReceiver;
    [SerializeField] private CleaningToolReceiver spongeToolReceiver;
    
    [Header("Animators")]
    [SerializeField] private Animator playerAnimator = null;
    [SerializeField] private Animator mopAnimator = null;
    [SerializeField] private Animator spongeAnimator = null;
    [SerializeField] private Animator handsAnimator = null;
    
    [Header("Interaction")]
    [SerializeField] private LayerMask mopLayerMask;
    [SerializeField] private LayerMask spongeLayerMask;
    [SerializeField] private float interactionDistance;

    [Header("Particle Systems")]
    [Header("Mop")]
    [SerializeField] private ParticleSystem mopCleaningParticles;
    [SerializeField] private ParticleSystem mopCleaningDirtyParticles;
    [SerializeField] private ParticleSystem mopDrippingParticles;
    [SerializeField] private ParticleSystem mopDrippingDirtyParticles;
    [Header("Sponge")]
    [SerializeField] private ParticleSystem spongeCleaningParticles;
    [SerializeField] private ParticleSystem spongeCleaningDirtyParticles;
    [SerializeField] private ParticleSystem spongeDrippingParticles;
    [SerializeField] private ParticleSystem spongeDrippingDirtyParticles;
    [Header("Trash Bin")]
    [SerializeField] private ParticleSystem trashBinCleaningParticles;

    [Header("Audio Config")]
    [SerializeField] private AudioManager audioManager = null;
    [Header("Clean General")]
    [SerializeField] private string cleanedEvent = null;
    [Header("Mop")]
    [SerializeField] private string mopEvent = null;
    [SerializeField] private string mopDirtyEvent = null;
    [SerializeField] private string mopWooshEvent = null;
    [SerializeField] private string mopSwapEvent = null;
    [Header("Sponge")]
    [SerializeField] private string spongeEvent = null;
    [SerializeField] private string spongeDirtyEvent = null;
    [SerializeField] private string spongeWooshEvent = null;
    [SerializeField] private string spongeSwapEvent = null;
    [Header("Hands")]
    [SerializeField] private string dropEvent = null;
    [SerializeField] private string pickUpEvent = null;
    [SerializeField] private string throwEvent = null;
    [SerializeField] private string handSwapEvent = null;
    [Header("Trash Bin")]
    [SerializeField] private string pickUpTrashEvent = null;
    [SerializeField] private string addNewTrashBagEvent = null;
    [SerializeField] private string zipBagEvent = null;
    [SerializeField] private string trashBinSwapEvent = null;
    [Header("Tool Wheel")]
    [SerializeField] private string openTWEvent = null;
    [SerializeField] private string closeTWEvent = null;
    [SerializeField] private string hoverEvent = null;

    [Header("UI Config")]
    [SerializeField] private Sprite pickUpMessage;
    [SerializeField] private Sprite dropMessage;
    [SerializeField] private Sprite throwMessage;
    [SerializeField] private Sprite rotateMessage;
    [SerializeField] private Sprite storeMessage;
    [SerializeField] private Sprite interactMessage;

    public static CleaningManager Instance { get; private set; }

    public int DirtyMaxValue { get; private set; }
    public int DirtyIncrementAmount { get; private set; }

    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        DirtyMaxValue = 100;
        DirtyIncrementAmount = cleaningTool.DirtyIncrement;
    }

    public Camera GetCamera() => gameCamera;
    public Animator GetPlayerAnimator() => playerAnimator;
    public Animator GetMopAnimator() => mopAnimator;
    public Animator GetSpongeAnimator() => spongeAnimator;
    public Animator GetHandsAnimator() => handsAnimator;
    public InputManager GetInputManager() => inputManager;
    public CleaningTool GetToolSelector() => cleaningTool;
    public int GetMop() => (int)Tools.Mop;
    public int GetSponge() => (int)Tools.Sponge;
    public int GetHands() => (int)Tools.Hands;
    public int GetTablet() => (int)Tools.Tablet;
    public int GetBin() => (int)Tools.Bin;
    public int GetDirtyMaxValue() => DirtyMaxValue;
    public int GetDirtyIncrementAmount() => cleaningTool.DirtyIncrement;
    public LayerMask GetMopLayerMask() => mopLayerMask;
    public LayerMask GetSpongeLayerMask() => spongeLayerMask;
    public float GetInteractionDistance() => interactionDistance;
    public string GetCleanedEvent() => cleanedEvent;
    public AudioManager GetAudioManager() => audioManager;
    public string GetMopEvent() => mopEvent;
    public string GetMopDirtyEvent() => mopDirtyEvent;
    public string GetMopWooshEvent() => mopWooshEvent;
    public string GetSpongeEvent() => spongeEvent;
    public string GetSpongeDirtyEvent() => spongeDirtyEvent;
    public string GetSpongeWooshEvent() => spongeWooshEvent;
    public string GetDropEvent() => dropEvent;
    public string GetPickUpEvent() => pickUpEvent;
    public string GetThrowEvent() => throwEvent;
    public string GetPickUpTrashEvent() => pickUpTrashEvent;
    public string GetAddNewTrashBagEvent() => addNewTrashBagEvent;
    public string GetZipBagEvent() => zipBagEvent;
    public string GetTrashBinSwapEvent() => trashBinSwapEvent;
    public string GetOpenTWEvent() => openTWEvent;
    public string GetCloseTWEvent() => closeTWEvent;
    public string GetHoverEvent() => hoverEvent;
    public ParticleSystem GetMopCleaningParticles() => mopCleaningParticles;
    public ParticleSystem GetMopCleaningDirtyParticles() => mopCleaningDirtyParticles;
    public ParticleSystem GetMopDrippingParticles() => mopDrippingParticles;
    public ParticleSystem GetMopDrippingDirtyParticles() => mopDrippingDirtyParticles;
    public ParticleSystem GetSpongeCleaningParticles() => spongeCleaningParticles;
    public ParticleSystem GetSpongeCleaningDirtyParticles() => spongeCleaningDirtyParticles;
    public ParticleSystem GetSpongeDrippingParticles() => spongeDrippingParticles;
    public ParticleSystem GetSpongeDrippingDirtyParticles() => spongeDrippingDirtyParticles;
    public CleaningToolReceiver GetMopToolReceiver() => mopToolReceiver;
    public CleaningToolReceiver GetSpongeToolReceiver() => spongeToolReceiver;
    public Sprite GetPickUpMessage() => pickUpMessage;
    public Sprite GetDropMessage() => dropMessage;
    public Sprite GetThrowMessage() => throwMessage;
    public Sprite GetRotateMessage() => rotateMessage;
    public Sprite GetStoreMessage() => storeMessage;
    public Sprite GetInteractMessage() => interactMessage;
}