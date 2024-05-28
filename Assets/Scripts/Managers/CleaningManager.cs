using UnityEngine;

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

    public float[] CleaningPercentages { get; private set; }
    public int DirtyMaxValue { get; private set; }
    public int DirtyIncrementAmount { get; private set; }

    private void Start()
    {
        CleaningPercentages = new float[4];

        CleaningPercentages[0] = 1.0f;
        CleaningPercentages[1] = 0.66f;
        CleaningPercentages[2] = 0.33f;
        CleaningPercentages[3] = 0.0f;

        DirtyMaxValue = 100;
        DirtyIncrementAmount = cleaningTool.DirtyIncrement;
    }

    private void OnDestroy()
    {
        CleaningPercentages = null;
    }

    public Camera GetCamera() 
    { 
        return gameCamera; 
    }

    public Animator GetPlayerAnimator()
    {
        return playerAnimator;
    }

    public Animator GetMopAnimator()
    {
        return mopAnimator;
    }

    public Animator GetSpongeAnimator()
    {
        return spongeAnimator;
    }

    public Animator GetHandsAnimator()
    {
        return handsAnimator;
    }

    public InputManager GetInputManager()
    {
        return inputManager;
    }

    public CleaningTool GetToolSelector()
    {
        return cleaningTool;
    }

    public int GetDirtyMaxValue()
    {
        return DirtyMaxValue;
    }

    public int GetDirtyIncrementAmount()
    {
        return cleaningTool.DirtyIncrement;
    }
}