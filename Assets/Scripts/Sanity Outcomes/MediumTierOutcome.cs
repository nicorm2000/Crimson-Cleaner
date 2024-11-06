using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MediumTierOutcome : MonoBehaviour
{
    public UnityEvent TriggerOutcome;
    [SerializeField] private VolumeController volumeController;
    [SerializeField] private GameObject visualObject;
    public float duration;
    public bool isActiveNow = false;

    public void ToggleVisualObjectState(bool active)
    {
        if (visualObject)
            visualObject.SetActive(active);
    }

    public void ToggleVolumeController(bool active)
    {
        if (active)
        {
            volumeController.gameObject.SetActive(active);
            volumeController.GetVolume().gameObject.SetActive(active);
            volumeController.StartVolumeVFX();
        }
        else
        {
            volumeController.EndVolumeVFX();
        }
    }

    public void StartVolumeControllerCoroutine()
    {
        StartCoroutine(ToggleVolumeControllerCoroutine());
    }
    private IEnumerator ToggleVolumeControllerCoroutine()
    {
        yield return new WaitForSeconds(0.5f);
        volumeController.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }

}
