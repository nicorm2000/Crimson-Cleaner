using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private InputManager inputManager;
    [SerializeField] private GameObject gameplayCanvasGO;
    [SerializeField] private GameObject pauseCanvasGO;

    private void OnEnable()
    {
        inputManager.PauseEvent += ToggleCanvases;
    }

    private void OnDisable()
    {
        inputManager.PauseEvent -= ToggleCanvases;

    }

    private void Start()
    {
        gameplayCanvasGO.SetActive(true);
        pauseCanvasGO.SetActive(false);
    }

    public void ToggleCanvases()
    {
        bool active = gameplayCanvasGO.activeSelf;
        gameplayCanvasGO.SetActive(!active);
        pauseCanvasGO.SetActive(active);
    }
}
