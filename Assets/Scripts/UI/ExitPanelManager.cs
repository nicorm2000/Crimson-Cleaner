using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitPanelManager : MonoBehaviour
{
    [SerializeField] private InputManager inputManager;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private MainMenuUIManager mainMenuUIManager;
    [SerializeField] private GameObject PCCanvas;
    [SerializeField] private GameObject exitGamePanel;

    private void OnEnable()
    {
        inputManager.PauseEvent += OnPauseEvent;
    }

    private void OnDisable()
    {
        inputManager.PauseEvent -= OnPauseEvent;
    }

    public void OnPauseEvent()
    {
        if (!mainMenuUIManager.pCCanvasController.isPlayerOnPC && !mainMenuUIManager.pCCanvasController.isPlayerMoving)
        {
            mainMenuUIManager.StartSimpleGaussianBlurState();
            ToggleExitGameCanvas();
            TogglePlayerMovement();
            ToggleCursor();
        }
    }

    private void ToggleExitGameCanvas()
    {
        bool active = exitGamePanel.activeSelf;
        mainMenuUIManager.ToggleCanvas(exitGamePanel, !active);
    }

    private void TogglePlayerMovement()
    {
        bool isMovable = playerController.isMovable;
        bool isCameraMovable = playerController.isMovable;
        playerController.ToggleMovement(!isMovable);
        playerController.ToggleCameraMovement(!isCameraMovable);
    }

    private void ToggleCursor()
    {
        if (exitGamePanel.activeSelf)
            inputManager.ShowCursor();
        else
            inputManager.HideCursor();
    }

}
