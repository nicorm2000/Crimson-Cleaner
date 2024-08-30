using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TrashBinController : MonoBehaviour
{
    [SerializeField] private InputManager inputManager;
    [SerializeField] private GameObject bagPrefab;
    [SerializeField] private Transform newBagTransform;
    [SerializeField] private GameObject emptyBagGO;
    [SerializeField] private GameObject fullBagGO;
    [SerializeField] private Transform mainCamera;
    [SerializeField] int raycastDistance;
    [SerializeField] int maxItemsPerBag;
    [SerializeField] string trashTag;

    public int counter;
    private bool isBagDispached;

    private void OnEnable()
    {
        inputManager.InteractEvent += OnInteractEvent;
        inputManager.DispatchBagEvent += OnDispatchBag;
    }

    private void OnDisable()
    {
        inputManager.InteractEvent -= OnInteractEvent;
        inputManager.DispatchBagEvent -= OnDispatchBag;
    }

    private void Start()
    {
        counter = 0;
        isBagDispached = true;
    }

    private void OnInteractEvent()
    {
        if (isBagDispached)
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
                    isBagDispached = false;
                }
            }
        }
    }

    private void OnDispatchBag()
    {
        if (!isBagDispached)
        {
            isBagDispached = true;
            emptyBagGO.SetActive(true);
            fullBagGO.SetActive(false);

            Instantiate(bagPrefab, newBagTransform.position, newBagTransform.rotation);
        }
    }
}
