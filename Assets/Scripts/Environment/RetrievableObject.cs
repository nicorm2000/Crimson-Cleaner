using System;
using System.Collections;
using UnityEngine;

public class RetrievableObject : MonoBehaviour, IRetrievable
{
    [SerializeField] private InputManager inputManager;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private PickUpDrop pickUpDrop;
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private string soundEvent;
    [SerializeField] private float raycastDistance;

    private bool isObjectPickedUp = false;
    private bool isHoldingRetrieve = false;
    private float holdTimer = 0f;
    private Coroutine currentRetrieveCoroutine;

    public bool IsObjectPickedUp => isObjectPickedUp;
    public Sprite InteractMessage => CleaningManager.Instance.GetRetrieveMessage();

    public event Action RetrieveStoppedEvent;
    public event Action ObjectRetrievedEvent;
    public event Action<GameObject> ObjectRetrievedEventGO;

    private void OnEnable()
    {
        inputManager.RetrieveEvent += HandleRetrieve;
    }

    private void OnDisable()
    {
        inputManager.RetrieveEvent -= HandleRetrieve;
    }

    private void Update()
    {
        if (isHoldingRetrieve)
        {
            Ray ray = new(cameraTransform.position, cameraTransform.forward);
            if (Physics.Raycast(ray, out RaycastHit hit, raycastDistance))
            {
                if (hit.collider.gameObject != gameObject)
                {
                    StopRetrieve();
                }
            }
            else
            {
                StopRetrieve();
            }
        }
    }


    private void HandleRetrieve(bool isRetrieving)
    {
        if (isRetrieving)
        {
            Ray ray = new(cameraTransform.transform.position, cameraTransform.transform.forward);
            if (Physics.Raycast(ray, out RaycastHit hit, raycastDistance))
            {
                if (hit.collider.gameObject == gameObject)
                {
                    if (currentRetrieveCoroutine != null)
                    {
                        StopCoroutine(currentRetrieveCoroutine);
                    }
                    playerController.SetIsRetrieveingObject(true);
                    
                    currentRetrieveCoroutine = StartCoroutine(HoldToRetrieve());
                }
            }
        }
        else
        {
            StopRetrieve();
        }
    }

    private void StopRetrieve()
    {
        RetrieveStoppedEvent?.Invoke();
        isHoldingRetrieve = false;
        if (currentRetrieveCoroutine != null)
        {
            StopCoroutine(currentRetrieveCoroutine);
            currentRetrieveCoroutine = null;
            playerController.SetIsRetrieveingObject(false);
        }
    }

    private IEnumerator HoldToRetrieve()
    {
        isHoldingRetrieve = true;
        holdTimer = 0f;

        while (isHoldingRetrieve)
        {
            holdTimer += Time.deltaTime;

            if (holdTimer >= pickUpDrop.GetHoldTime())
            {
                ToggleObjectState();
                currentRetrieveCoroutine = null;
                yield break;
            }

            yield return null;
        }

        holdTimer = 0f;
    }

    private void ToggleObjectState()
    {
        if (SanityManager.Instance.isRageActive) return;

        if (playerController.GetObjectGrabbable() == null)
        {
            Debug.Log("Retrieve Object: " + name);

            if (soundEvent != null && audioManager != null)
                audioManager.PlaySound(soundEvent);

            isObjectPickedUp = true;
            RetrieveStoppedEvent?.Invoke();
            ObjectRetrievedEvent?.Invoke();
            ObjectRetrievedEventGO?.Invoke(gameObject);
            playerController.SetIsRetrieveingObject(false);

            gameObject.SetActive(false);
            // Destroy(gameObject); // Example: Destroy the object after retrieval
        }
    }

    public void Interact(PlayerController playerController)
    {
        throw new NotImplementedException();
    }
}

