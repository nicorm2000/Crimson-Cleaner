using UnityEngine;

public class LobbyWakeUpManager : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    [SerializeField] private ExitPanelManager exitPanelManager;
    [SerializeField] private MainMenuUIManager mainMenuUIManager;
    [SerializeField] private Camera mainCamera;

    private void Awake()
    {
        mainCamera.enabled = false;   
    }

    public void EnableExitPanel()
    {
        if (!exitPanelManager.canToggleExit)
            exitPanelManager.EnablePanel();
    }

    public void EnableReticle()
    {
        if (!mainMenuUIManager.reticle.activeSelf)
            mainMenuUIManager.EnableReticle();
    }

    public void DisablePlayerMovement()
    {
        playerController.ToggleMovement(false);
        playerController.ToggleCameraMovement(false);
    }

    public void EnablePlayerMovement()
    {
        if (!playerController.isMovable)
            playerController.ToggleMovement(true);
        if (!playerController.isCameraMovable)
            playerController.ToggleCameraMovement(true);
    }

    public void EnableMainCamera()
    {
        mainCamera.enabled = true;
    }
}
