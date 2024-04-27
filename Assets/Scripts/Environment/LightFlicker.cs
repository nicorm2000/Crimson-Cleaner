using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlicker : MonoBehaviour
{
    [Header("Flicker Set Up")]
    [SerializeField] private float minTime = 0;
    [SerializeField] private float maxTime = 0;

    private Light _lightGO;
    private float _timer;

    private void Start()
    {
        _lightGO ??= GetComponent<Light>();
        _timer = Random.Range(minTime, maxTime);
    }

    private void Update()
    {
        LightsFlickering();
    }

    private void LightsFlickering()
    {
        if (_timer > 0)
            _timer -= Time.deltaTime;

        if (_timer <= 0)
        {
            _lightGO.enabled = !_lightGO.enabled;
            _timer = Random.Range(minTime, maxTime);
        }
    }
}