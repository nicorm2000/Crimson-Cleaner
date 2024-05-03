using UnityEngine;

public class CleaningManager : MonoBehaviourSingleton<CleaningManager>
{
    [Header("Config")]
    [SerializeField] private Camera gameCamera = null;
    [SerializeField] private Animator playerAnimator = null;
    [SerializeField] private InputManager inputManager = null;

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