using UnityEngine;

public class LightSwitch : Interactable, IToggable
{
    [Header("Switch Config")]
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private CleaningManager cleaningManager;
    [SerializeField] private GameObject lightOn = null;
    [SerializeField] private GameObject lightOff = null;
    [SerializeField] private GameObject[] lightsObject = null;
    [SerializeField] private bool lightsAreOn = true;

    [Header("UI Config")]
    [SerializeField] private Sprite toggleOnOffMessage;

    public Sprite InteractMessage => toggleOnOffMessage;
    public void Interact(PlayerController playerController)
    {
        ToggleLights();
    }

    protected override void PerformInteraction(PlayerController playerController)
    {
        Interact(playerController);
    }

    private void Start()
    {
        SetLightsState(lightsAreOn);
        lightOn.SetActive(lightsAreOn);
        lightOff.SetActive(!lightsAreOn);
    }

    private void ToggleLights()
    {
        audioManager.PlaySound(soundEvent);
        lightsAreOn = !lightsAreOn;
        SetLightsState(lightsAreOn);
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