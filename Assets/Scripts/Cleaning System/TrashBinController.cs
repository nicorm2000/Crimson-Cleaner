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

    private int counter;
    private bool isBagDispatched;

    private void OnEnable()
    {
        inputManager.StoreObjectEvent += OnPickUp;
        inputManager.DispatchBagEvent += OnDispatchBag;
    }

    private void OnDisable()
    {
        inputManager.StoreObjectEvent -= OnPickUp;
        inputManager.DispatchBagEvent -= OnDispatchBag;
    }

    private void Start()
    {
        counter = 0;
        isBagDispatched = true;
    }

    private void OnPickUp()
    {
        if (isBagDispatched)
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

                    counter = 0;
                    isBagDispatched = false;
                }
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

            Instantiate(bagPrefab, newBagTransform.position, newBagTransform.rotation);
        }
    }
}
