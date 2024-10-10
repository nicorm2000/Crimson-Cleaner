using UnityEngine;
using UnityEngine.UI;

public class RetrieveImageController : MonoBehaviour
{
    public InputManager inputManager;
    public PlayerController playerController;
    public Image retrieveImage;
    public PickUpDrop pickUpDrop;

    public RetrievableObject[] retrievableObjects;

    private float retrieveTimer = 0f;

    private void Start()
    {
        retrieveImage.fillAmount = 0f;
        retrieveImage.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        inputManager.RetrieveEvent += HandleRetrieve;

        foreach (var obj in retrievableObjects)
        {
            obj.RetrieveStoppedEvent += HideRetrieveImage;
        }
    }

    private void OnDisable()
    {
        inputManager.RetrieveEvent -= HandleRetrieve;

        foreach (var obj in retrievableObjects)
        {
            obj.RetrieveStoppedEvent -= HideRetrieveImage;
        }
    }

    private void HandleRetrieve(bool isRetrieving)
    {
        if (isRetrieving && pickUpDrop.GetObjectGrabbable() == null)
        {
            Invoke("ShowRetrieveImage", 0.1f);
        }
        else
        {
            HideRetrieveImage();
        }
    }

    private void ShowRetrieveImage()
    {
        if (playerController.IsRetrievingObject)
        {
            retrieveImage.gameObject.SetActive(true);
        }
    }

    private void HideRetrieveImage()
    {
        retrieveImage.fillAmount = 0f;
        retrieveImage.gameObject.SetActive(false);
        retrieveTimer = 0f;
    }

    private void Update()
    {
        if (retrieveImage.gameObject.activeSelf)
        {
            retrieveTimer += Time.deltaTime;
            retrieveImage.fillAmount = Mathf.Clamp01(retrieveTimer / pickUpDrop.GetHoldTime());

            retrieveImage.color = retrieveImage.fillAmount >= 1 ? Color.yellow : Color.white;
        }
    }
}
