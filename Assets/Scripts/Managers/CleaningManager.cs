using UnityEngine;

public class CleaningManager : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private Camera gameCamera = null;
    [SerializeField] private Animator playerAnimator = null;
    [SerializeField] private InputManager inputManager = null;

    public float[] CleaningPercentages { get; private set; }

    private void Start()
    {
        CleaningPercentages = new float[4];

        CleaningPercentages[0] = 1.0f;
        CleaningPercentages[1] = 0.66f;
        CleaningPercentages[2] = 0.33f;
        CleaningPercentages[3] = 0.0f;
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

    public InputManager GetInputManager()
    {
        return inputManager;
    }
}