using System.Collections;
using UnityEngine;

public class TrashBinController : MonoBehaviour
{
    [Header("Scripts Config")]
    [SerializeField] private InputManager inputManager;
    [SerializeField] private CleaningManager cleaningManager;
    [Header("Trash Bin Config")]
    [SerializeField] private GameObject emptyBagGO;
    [SerializeField] private GameObject fullBagGO;
    [SerializeField] private Transform mainCamera;
    [SerializeField] int raycastDistance;
    [SerializeField] int maxItemsPerBag;
    [SerializeField] string trashTag;
    [Header("New bag Config")]
    [SerializeField] private GameObject bagPrefab;
    [SerializeField] private Transform newBagTransform;
    [Header("Animations")]
    [SerializeField] private float binCleanAnimDuration;
    [SerializeField] private float binRemoveAnimDuration;

    [Header("Audio Config")]
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private string pickUpTrashEvent;
    [SerializeField] private string removeTrashEvent;
    [SerializeField] private string selectTrashBinEvent;
    [SerializeField] private string swapTrashBinEvent;

    private int counter;
    private bool isBagDispatched;

    private void OnEnable()
    {
        inputManager.StoreObjectEvent += OnPickUp;
        //inputManager.DispatchBagEvent += OnDispatchBag;
    }

    private void OnDisable()
    {
        inputManager.StoreObjectEvent -= OnPickUp;
        //inputManager.DispatchBagEvent -= OnDispatchBag;
    }

    private void Start()
    {
        counter = 0;
        isBagDispatched = true;
    }

    private void OnPickUp()
    {
        if (cleaningManager.GetToolSelector().CurrentToolIndex == cleaningManager.GetBin() && isBagDispatched)
            CheckForInteraction();
    }

    private void CheckForInteraction()
    {
        RaycastHit hit;

        Debug.DrawRay(mainCamera.transform.position, transform.forward, Color.red,1f);

        if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out hit, raycastDistance))
        {
            if (hit.transform.gameObject.CompareTag(trashTag))
            {
                hit.transform.gameObject.SetActive(false);
                counter++;

                if (counter == maxItemsPerBag)
                {
                    emptyBagGO.SetActive(false);
                    fullBagGO.SetActive(true);

                    // Implementar audio - bag full
                    //if (audioManager != null && pickUpTrashEvent != null)
                    //    audioManager.PlaySound(pickUpTrashEvent);

                    if (audioManager != null && removeTrashEvent != null)
                        audioManager.PlaySound(removeTrashEvent);

                    counter = 0;
                    isBagDispatched = false;

                    StartCoroutine(RemoveAnimationCoroutine());
                }
                else
                {
                    // Implementar audio - trash picked up
                    if (audioManager != null && pickUpTrashEvent != null)
                        audioManager.PlaySound(pickUpTrashEvent);


                    //if (cleaningManager.GetPickUpTrashEvent() != null)
                    //    cleaningManager.GetAudioManager().PlaySound(cleaningManager.GetPickUpTrashEvent());
                }

                cleaningManager.GetToolSelector().toolAnimatorController.TriggerParticularAction(cleaningManager.GetToolSelector().toolAnimatorController.GetBinCleanName());
                cleaningManager.GetToolSelector().toolAnimatorController.TriggerActionCoroutine(binCleanAnimDuration);
            }
        }
    }

    private void OnDispatchBag()
    {
        if (!isBagDispatched)
        {
            isBagDispatched = true;
            emptyBagGO.SetActive(true);
            fullBagGO.SetActive(false);


            // Implementar audio - new empty bag
            //if (cleaningManager.GetAddNewTrashBagEvent() != null)
            //    cleaningManager.GetAudioManager().PlaySound(cleaningManager.GetAddNewTrashBagEvent());


            cleaningManager.GetToolSelector().toolAnimatorController.TriggerParticularAction(cleaningManager.GetToolSelector().toolAnimatorController.GetBinRemoveName());
            cleaningManager.GetToolSelector().toolAnimatorController.TriggerActionCoroutine(binRemoveAnimDuration);
            StartCoroutine(InstantiateBagCoroutin());
        }
    }

    private IEnumerator RemoveAnimationCoroutine()
    {
        yield return new WaitForSeconds(1f);
        OnDispatchBag();
    }

    private IEnumerator InstantiateBagCoroutin()
    {
        yield return new WaitForSeconds(0.5f);
        Instantiate(bagPrefab, newBagTransform.position, newBagTransform.rotation);
    }
}
