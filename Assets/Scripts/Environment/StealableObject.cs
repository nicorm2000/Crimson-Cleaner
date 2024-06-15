using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class StealableObject : MonoBehaviour, IRetrievable
{
    [Header("Openable Config")]
    [SerializeField] private InputManager inputManager;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private float raycastDistance = 3f;
    [SerializeField] private LayerMask interactableLayerMask = ~0;
    [SerializeField] private Sprite pickUpMessage;
    [SerializeField] private float moneyAmount;

    [Header("UI Config")]
    [SerializeField] private TextMeshProUGUI moneyText;
    [SerializeField] private float moneyPopUpDuration;

    [Header("Audio Config")]
    [SerializeField] private AudioManager audioManager = null;
    [SerializeField] private string moneyEvent = null;

    public static Coroutine currentPopUpCoroutine = null;
    public static float totalMoney = 0;
    public static bool coroutineRunning = false;

    public Sprite PickUpMessage => pickUpMessage;

    private void OnEnable()
    {
        inputManager.InteractEvent += OnRetrieveObject;
    }

    private void OnDisable()
    {
        inputManager.InteractEvent -= OnRetrieveObject;
    }

    private void OnRetrieveObject()
    {
        if (IsMouseLookingAtObject() && playerController.GetObjectGrabbable() == null)
        {
            RetrieveObject();
        }
    }

    private bool IsMouseLookingAtObject()
    {
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (Physics.Raycast(ray, out RaycastHit hit, raycastDistance, interactableLayerMask))
        {
            if (hit.transform != gameObject.transform)
            {
                return false;
            }

            audioManager.PlaySound(moneyEvent);
            return hit.collider.gameObject.GetComponent<StealableObject>() && hit.transform == transform;
        }
        return false;
    }

    private void RetrieveObject()
    {
        if (coroutineRunning)
        {
            totalMoney += moneyAmount;
        }
        else
        {
            totalMoney += moneyAmount;
            currentPopUpCoroutine = StartCoroutine(ShowMoneyPopUp());
        }

        gameObject.GetComponent<Collider>().enabled = false;
        gameObject.GetComponent<Renderer>().enabled = false;
        gameObject.GetComponentInChildren<Renderer>().enabled = false;
    }

    private IEnumerator ShowMoneyPopUp()
    {
        coroutineRunning = true;
        float elapsedTime = 0f;

        while (elapsedTime < moneyPopUpDuration)
        {
            moneyText.text = "$" + totalMoney.ToString();
            moneyText.gameObject.SetActive(true);

            elapsedTime += Time.deltaTime;
            yield return null;

            if (totalMoney > float.Parse(moneyText.text.Substring(1)))
            {
                elapsedTime = 0f;
            }
        }

        moneyText.gameObject.SetActive(false);
        totalMoney = 0;
        coroutineRunning = false;
    }
}