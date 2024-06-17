using UnityEngine;
using UnityEngine.InputSystem;

public class LightSwitch : MonoBehaviour, IToggable
{
    [Header("Switch Config")]
    [SerializeField] private InputManager inputManager;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private CleaningManager cleaningManager;
    [SerializeField] private GameObject lightOn = null;
    [SerializeField] private GameObject lightOff = null;
    [SerializeField] private GameObject[] lightsObject = null;

    [Header("UI Config")]
    [SerializeField] private Sprite toggleOnOffMessage;

    [Header("Audio Config")]
    [SerializeField] private AudioManager audioManager = null;
    [SerializeField] private string lampToolEvent = null;

    public Sprite ToggleOnOffMessage => toggleOnOffMessage;

    private bool _lightsAreOn = true;

    private void OnEnable()
    {
        inputManager.InteractEvent += ToggleLights;
    }

    private void OnDisable()
    {
        inputManager.InteractEvent -= ToggleLights;
    }

    private void Start()
    {
        lightOn.SetActive(true);
        lightOff.SetActive(false);
        _lightsAreOn = lightOn.activeSelf;
    }

    private void ToggleLights()
    {
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());

        if (Physics.Raycast(ray, out RaycastHit hit, cleaningManager.GetInteractionDistance(), layerMask))
        {
            if (hit.transform != gameObject.transform) return;

            audioManager.PlaySound(lampToolEvent);
            _lightsAreOn = !_lightsAreOn;
            SetLightsState(_lightsAreOn);
        }
    }

    private void SetLightsState(bool lightsOn)
    {
        foreach (var lightObj in lightsObject)
        {
            lightObj.SetActive(lightsOn);
        }

        lightOn.SetActive(lightsOn);
        lightOff.SetActive(!lightsOn);
    }
}