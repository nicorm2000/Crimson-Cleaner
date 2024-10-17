using UnityEngine;

public class LobbyLevelManager : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private DeepWebUIManager deepWebUIManager;
    [SerializeField] private string vanTrigger;
    [SerializeField] private GameObject van;

    private Animator vanAnimator;

    private void Awake()
    {
        vanAnimator = van.GetComponent<Animator>();
    }

    private void OnEnable()
    {
        deepWebUIManager.LevelAccepted += OnLevelAccepted;
    }

    private void OnDisable()
    {
        deepWebUIManager.LevelAccepted -= OnLevelAccepted;
    }

    private void OnLevelAccepted()
    {
        vanAnimator.SetTrigger(vanTrigger);
    }
}
