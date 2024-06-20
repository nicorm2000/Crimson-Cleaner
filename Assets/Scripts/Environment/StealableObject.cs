using UnityEngine;
using UnityEngine.InputSystem;

public class StealableObject : MonoBehaviour, IRetrievable
{
    [Header("Stealable Config")]
    [SerializeField] private InputManager inputManager;
    [SerializeField] private PickUpDrop pickUpDrop;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private StealableManager stealableManager;
    [SerializeField] private float raycastDistance = 3f;
    [SerializeField] private LayerMask interactableLayerMask = ~0;
    [SerializeField] private Sprite pickUpMessage;
    [SerializeField] private float moneyAmount;

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
        if (pickUpDrop.GetObjectGrabbable() != null) return false;

        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (Physics.Raycast(ray, out RaycastHit hit, raycastDistance, interactableLayerMask))
        {
            if (hit.transform != gameObject.transform) return false;

            if (hit.collider.gameObject.GetComponent<StealableObject>())
            {
                stealableManager.PlayeMoneySFX();
                return true;
            }
        }
        return false;
    }

    private void RetrieveObject()
    {
        stealableManager.AddMoney(moneyAmount);
        Destroy(gameObject);
    }
}