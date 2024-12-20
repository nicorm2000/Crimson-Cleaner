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

    public bool canToggleExit = false;

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
        if (mainMenuUIManager.pCCanvasController.isPlayerOnPC || mainMenuUIManager.pCCanvasController.isPlayerMoving || !canToggleExit) return;


        mainMenuUIManager.StartSimpleGaussianBlurState();
        //ToggleSimpleGaussianBlur();
        ToggleExitGameCanvas();
        TogglePlayerMovement();
        ToggleCursor();
    }

    public void DisablePanel()
    {
        canToggleExit = false;
    }

    public void EnablePanel()
    {
        canToggleExit = true;
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

    private void ToggleSimpleGaussianBlur()
    {
        if (exitGamePanel.activeSelf)
            mainMenuUIManager.StopSimpleGaussianBlurState();
        else
            mainMenuUIManager.StartSimpleGaussianBlurState();
    }

}
