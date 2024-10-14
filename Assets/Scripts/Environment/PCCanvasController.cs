using System.Collections;
using UnityEngine;

public class PCCanvasController : Interactable, IInteractable
{
    [SerializeField] private InputManager inputManager;
    [SerializeField] private MainMenuUIManager mainMenuUIManager;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private Transform mainCamera;
    [SerializeField] private Transform targetTransform;
    [SerializeField] private float lerpDuration = 1f;

    [SerializeField] private Sprite interactSprite;
    public Sprite InteractMessage => interactSprite;

    public bool isPlayerOnPC = false;
    public bool isPlayerMoving = false;

    private Vector3 previousCameraPosition;
    private Quaternion previousCameraRotation;

    private Coroutine goCoroutine;
    private Coroutine returnCoroutine;

    public void Interact(PlayerController playerController)
    {
        if (goCoroutine == null && returnCoroutine == null)
        {
            goCoroutine = StartCoroutine(PCStartUpCoroutine());
        }
    }

    protected override void PerformInteraction(PlayerController playerController)
    {
        Interact(playerController);
    }

    private IEnumerator PCStartUpCoroutine()
    {
        isPlayerMoving = true;
        playerController.ToggleMovement(false);
        playerController.ToggleCameraMovement(false);

        mainMenuUIManager.ToggleCanvas(mainMenuUIManager.mainCanvas, false);

        previousCameraPosition = mainCamera.transform.position;
        previousCameraRotation = mainCamera.transform.rotation;

        Vector3 initialPosition = mainCamera.transform.position;
        Quaternion initialRotation = mainCamera.transform.rotation;

        float elapsedTime = 0f;

        while (elapsedTime < lerpDuration)
        {
            float t = elapsedTime / lerpDuration;

            mainCamera.transform.position = Vector3.Lerp(initialPosition, targetTransform.position, t);
            mainCamera.transform.rotation = Quaternion.Lerp(initialRotation, targetTransform.rotation, t);

            elapsedTime += Time.deltaTime;
            yield return null; 
        }

        mainCamera.transform.position = targetTransform.position;
        mainCamera.transform.rotation = targetTransform.rotation;

        mainMenuUIManager.ToggleCanvas(mainMenuUIManager.pcCanvas, true);
        inputManager.ShowCursor();

        isPlayerMoving = false;
        isPlayerOnPC = true;
        goCoroutine = null;
    }

    public void ShutDownPC()
    {
        if (returnCoroutine == null && goCoroutine == null)
        {
            returnCoroutine = StartCoroutine(PCShutDownCoroutine());
        }
    }

    private IEnumerator PCShutDownCoroutine()
    {
        isPlayerMoving = true;

        Vector3 initialPosition = mainCamera.transform.position;
        Quaternion initialRotation = mainCamera.transform.rotation;

        float elapsedTime = 0f;

        while (elapsedTime < lerpDuration)
        {
            float t = elapsedTime / lerpDuration;

            mainCamera.transform.position = Vector3.Lerp(initialPosition, previousCameraPosition, t);
            mainCamera.transform.rotation = Quaternion.Lerp(initialRotation, previousCameraRotation, t);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        mainCamera.transform.position = previousCameraPosition;
        mainCamera.transform.rotation = previousCameraRotation;

        mainMenuUIManager.ToggleCanvas(mainMenuUIManager.pcCanvas, false);
        mainMenuUIManager.ToggleCanvas(mainMenuUIManager.mainCanvas, true);

        inputManager.HideCursor();

        playerController.ToggleMovement(true);
        playerController.ToggleCameraMovement(true);

        isPlayerMoving = false;
        isPlayerOnPC = false;
        returnCoroutine = null;
    }

}
