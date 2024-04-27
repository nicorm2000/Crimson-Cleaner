using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LightSwitch : MonoBehaviour
{
    [Header("Switch Config")]
    [SerializeField] private InputManager inputManager;
    [SerializeField] private GameObject lightOn = null;
    [SerializeField] private GameObject lightOff = null;
    [SerializeField] private GameObject[] lightsObject = null;

    private bool _lightsAreOn;

    private void OnEnable()
    {
        inputManager.ToggleLightsEvent += ToggleLights;
    }

    private void OnDisable()
    {
        inputManager.ToggleLightsEvent -= ToggleLights;
    }

    private void Start()
    {
        lightOn.SetActive(false);
        lightOff.SetActive(true);
        _lightsAreOn = lightOn.activeSelf;
    }

    private void ToggleLights()
    {
        _lightsAreOn = !_lightsAreOn;
        SetLightsState(_lightsAreOn);
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