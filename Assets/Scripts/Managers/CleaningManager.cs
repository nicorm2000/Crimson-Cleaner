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

public class CleaningManager : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private Camera gameCamera = null;
    [SerializeField] private Animator playerAnimator = null;
    [SerializeField] private Animator mopAnimator = null;
    [SerializeField] private Animator spongeAnimator = null;
    [SerializeField] private Animator handsAnimator = null;
    [SerializeField] private InputManager inputManager = null;
    [SerializeField] private CleaningTool cleaningTool = null;
    [SerializeField] private LayerMask mopLayerMask;
    [SerializeField] private LayerMask spongeLayerMask;
    [SerializeField] private float interactionDistance;
    [SerializeField] private ParticleSystem mopCleaningParticles;
    [SerializeField] private ParticleSystem sponeCleaningParticles;

    [Header("Audio Config")]
    [SerializeField] private AudioManager audioManager = null;
    [SerializeField] private string mopEvent = null;
    [SerializeField] private string mopWooshEvent = null;
    [SerializeField] private string spongeEvent = null;
    [SerializeField] private string spongeWooshEvent = null;
    [SerializeField] private string cleanedEvent = null;

    public int DirtyMaxValue { get; private set; }
    public int DirtyIncrementAmount { get; private set; }

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
    public int GetDirtyMaxValue() => DirtyMaxValue;
    public int GetDirtyIncrementAmount() => cleaningTool.DirtyIncrement;
    public LayerMask GetMopLayerMask() => mopLayerMask;
    public LayerMask GetSpongeLayerMask() => spongeLayerMask;
    public float GetInteractionDistance() => interactionDistance;
    public string GetMopEvent() => mopEvent;
    public string GetMopWooshEvent() => mopWooshEvent;
    public string GetSpongeEvent() => spongeEvent;
    public string GetSpongeWooshEvent() => spongeWooshEvent;
    public string GetCleanedEvent() => cleanedEvent;
    public ParticleSystem GetMopParticles() => mopCleaningParticles;
    public ParticleSystem GetSpongeParticles() => sponeCleaningParticles;
}