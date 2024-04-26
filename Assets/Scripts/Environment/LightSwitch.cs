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
    private bool _lightsAreOff;

    private InputAction _toggleAction;

    private void Start()
    {
        _lightsAreOn = false;
        _lightsAreOff = true;
        lightOn.SetActive(false);
        lightOff.SetActive(true);

        _toggleAction = inputManager.inputMaster.Player.ToggleLights;
        _toggleAction.performed += ctx => ToggleLights();
        _toggleAction.Enable();
    }

    private void OnDisable()
    {
        _toggleAction.Disable();
    }

    private void ToggleLights()
    {
        _lightsAreOn = !_lightsAreOn;
        _lightsAreOff = !_lightsAreOff;

        IterateThroughLights(_lightsAreOn);
        lightOn.SetActive(_lightsAreOn);
        lightOff.SetActive(_lightsAreOff);
    }

    private void IterateThroughLights(bool value)
    {
        for (int i = 0; i < lightsObject.Length; i++)
        {
            lightsObject[i].SetActive(value);
        }
    }
}