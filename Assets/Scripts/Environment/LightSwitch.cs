using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : MonoBehaviour
{
    [Header("Switch Config")]
    [SerializeField] private GameObject lightOn = null;
    [SerializeField] private GameObject lightOff = null;
    //[SerializeField] private GameObject lightsText = null;
    [SerializeField] private GameObject[] lightsObject = null;

    private bool _lightsAreOn;
    private bool _lightsAreOff;
    private bool _inReach;

    private void Start()
    {
        _inReach = false;
        _lightsAreOn = false;
        _lightsAreOff = true;
        lightOn.SetActive(false);
        lightOff.SetActive(true);

        IterateThroughLights(false);
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.tag == "Player")
    //    {
    //        _inReach = true;
    //        //lightsText.SetActive(true);
    //    }
    //}
    //
    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.gameObject.tag == "Player")
    //    {
    //        _inReach = false;
    //        //lightsText.SetActive(false);
    //    }
    //}

    private void Update()
    {
        if (_lightsAreOn && Input.GetKeyDown(KeyCode.E))
        {
            IterateThroughLights(false);
            lightOn.SetActive(false);
            lightOff.SetActive(true);
            _lightsAreOff = true;
            _lightsAreOn = false;
        }
        else if (_lightsAreOff && Input.GetKeyDown(KeyCode.E))
        {
            IterateThroughLights(true);
            lightOn.SetActive(true);
            lightOff.SetActive(false);
            _lightsAreOff = false;
            _lightsAreOn = true;
        }
    }

    private void IterateThroughLights(bool value)
    {
        for (int i = 0; i < lightsObject.Length; i++)
        {
            lightsObject[i].SetActive(value);
        }
    }
}